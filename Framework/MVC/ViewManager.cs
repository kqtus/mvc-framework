using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace MVC
{
    public class ViewChangedEventArgs : EventArgs
    {
        public System.Type PrevViewType { get; set; }
        public System.Type NextViewType { get; set; }
    }

    public delegate void ViewChangedHandler(object sender, ViewChangedEventArgs args);    
    
    public class ViewManager
    {
        public event ViewChangedHandler OnViewChangedHandler;

        public ViewManager()
        {
            _registeredViews = new List<View>();
            CreateViews();
        }

        public bool ShowView(System.Type viewType, IContext context = null)
        {
            ViewChangedEventArgs viewChangedEventArgs = new ViewChangedEventArgs();
            
            viewChangedEventArgs.PrevViewType = _lastShownViewType;
            viewChangedEventArgs.NextViewType = viewType;

            var view = FindView(viewType);
            if (OnViewChangedHandler != null && view != null)
                OnViewChangedHandler(this, viewChangedEventArgs);

            _lastShownViewType = viewType;

            view?.Show(context);
            return view != null;
        }

        public bool UpdateView(System.Type viewType)
        {
            var view = FindView(viewType);

            view?.Update();
            return view != null;
        }

        public bool HideView(System.Type viewType)
        {
            var view = FindView(viewType);

            view?.Hide();
            return view != null;
        }

        public View FindView(System.Type type)
        {
            return _registeredViews.Find(v => v.GetType() == type);
        }

        protected List<Type> CollectAllViewTypes()
        {
            List<Type> viewTypes = new List<Type>();

            var assembly = Assembly.GetAssembly(typeof(View));
            var baseViewType = typeof(View);
            
            return assembly
                    .GetTypes()
                    .Where(t => t != baseViewType && baseViewType.IsAssignableFrom(t))
                    .ToList();
        }

        protected void CreateViews()
        {
            var viewTypes = CollectAllViewTypes();

            foreach (var viewType in viewTypes)
            {
                _registeredViews.Add((View)Activator.CreateInstance(viewType));
            }
        }

        private List<View> _registeredViews;
        
        private static System.Type _lastShownViewType;

        private static ViewManager _instance;
        public static ViewManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ViewManager();
                
                return _instance;
            }
        }
    }
}