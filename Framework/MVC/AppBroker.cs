using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace MVC
{
    public class AppBroker
    {
        public AppBroker(System.Type entryControllerType)
        {
            DaoToModelBindings = new List<DaoToModelBinding>();

            _entryControllerType = entryControllerType;
            _controllers = new List<Controller>();
            _models = new List<Model>();

            ViewManager.Instance.OnViewChangedHandler += OnViewChanged;
        }

        public int Run()
        {
            CreateModels();
            CreateControllers();

            if (_activeController == null)
                _activeController = FindControllerOfType(_entryControllerType);

            System.Console.WriteLine("Entry controller:" + _activeController);
            _activeController.ShowView();
            return 0;
        }

        protected void OnViewChanged(object sender, ViewChangedEventArgs args)
        {
            _activeController = FindController((c) => 
            {
                var attribs = System.Attribute.GetCustomAttributes(c.GetType());
                var metaAttrib = (ControllerMeta)attribs
                    .Where(atr => atr.GetType() == typeof(ControllerMeta))
                    .ToList()
                    .FirstOrDefault();

                if (metaAttrib == null)
                    return false;

                return metaAttrib.ViewType == args.NextViewType;
            });
        }

        protected Controller _activeController;
        public Controller ActiveController => _activeController;

        protected System.Type _entryControllerType;

        protected List<Controller> _controllers;

        public Controller FindControllerOfType(System.Type type)
        {
            return _controllers.Find(v => v.GetType() == type);
        }

        public Controller FindController(System.Predicate<Controller> func)
        {
            return _controllers.Find(func);
        }

        protected List<Model> _models;

        protected List<Type> CollectAllControllerTypes()
        {
            List<Type> viewTypes = new List<Type>();

            var baseType = typeof(Controller);
            var assembly = Assembly.GetAssembly(baseType);
            
            return assembly
                    .GetTypes()
                    .Where(t => t != baseType && baseType.IsAssignableFrom(t))
                    .ToList();
        }

        protected List<Type> CollectAllModelTypes()
        {
            List<Type> modelTypes = new List<Type>();

            var baseType = typeof(Model);
            var assembly = Assembly.GetAssembly(baseType);
            
            
            return assembly
                    .GetTypes()
                    .Where(t => t != baseType && baseType.IsAssignableFrom(t))
                    .ToList();
        }

        protected void CreateControllers()
        {
            var controllerTypes = CollectAllControllerTypes();

            foreach (var controllerType in controllerTypes)
            {
                var controller = (Controller)Activator.CreateInstance(controllerType);
                _controllers.Add(controller);

                var attribs = System.Attribute.GetCustomAttributes(controller.GetType());
                var metaAttrib = (ControllerMeta)attribs
                    .Where(atr => atr.GetType() == typeof(ControllerMeta))
                    .ToList()
                    .FirstOrDefault();

                controller.SetViewType(metaAttrib.ViewType);
                controller.SetModel(GetModelOfType(metaAttrib.ModelType));
                controller.Init();
            } 
        }

        protected void CreateModels()
        {
            var modelTypes = CollectAllModelTypes();

            foreach (var modelType in modelTypes)
            {
                if (!IsModelType(modelType))
                    continue;

                Model model;
                if (IsFetchableType(modelType))
                {
                    // #TODO: Throw exception when binding is not found.
                    model = (Model)Activator.CreateInstance
                    (
                        modelType, 
                        DaoToModelBindings
                            .Where(b => b.DestModelType == modelType)
                            .Select(b => b.DAO)
                            .ToList()
                            .FirstOrDefault()   
                    );
                }
                else 
                {
                    model = (Model)Activator.CreateInstance(modelType);
                }

                _models.Add(model);
            }
        }

        private bool IsFetchableType(System.Type modelType)
        {
            var fetchableType = typeof(IFetchableModel);
            return fetchableType.IsAssignableFrom(modelType);
        }

        private bool IsModelType(System.Type modelType)
        {
            var baseModelType = typeof(Model);
            return baseModelType.IsAssignableFrom(modelType);
        }

        protected Model GetModelOfType(System.Type modelType)
        {
            return _models.Where(mod => mod.GetType() == modelType).ToList().FirstOrDefault();
        }

        public List<DaoToModelBinding> DaoToModelBindings { get; }
    }
}