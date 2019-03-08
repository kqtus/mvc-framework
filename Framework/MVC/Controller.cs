using System;
using System.Collections.Generic;

namespace MVC
{
    public class Controller 
    {
        public virtual void Init()
        {
            TryFetchModelData();
            BindViewCommunication();
        }

        public virtual bool ShowView(System.Type viewType, IContext context = null)
        {
            return ViewManager.Instance.ShowView(viewType, context);
        }

        protected virtual bool TryFetchModelData() 
        {
            if (!(GetModel() is IFetchableModel))
                return false;

            IFetchableModel fetchable = (IFetchableModel)GetModel();
            if (fetchable == null)
                return false;

            return fetchable.Fetch();
        }

        public bool ShowView(IContext context = null)
        {
            return ShowView(_viewType, context);
        }

        private void HideViewAndShowNext(object sender, EventArgs args)
        {
            ViewManager.Instance.HideView(_viewType);
            if (NextViewType != null)
            {
                ShowView(NextViewType, InvocationContext);
                InvocationContext = null;
            }  
        } 

        protected virtual bool DispatchInput(object sender, UserInputArgs args)
        {
            if (args.Input == "exit")
            {
                NextViewType = null;
                return false;
            }
            return true;
        }

        private bool BindViewCommunication()
        {
            var view = GetView();
            if (view == null)
                return false;

            view.OnHideHandler += HideViewAndShowNext;
            view.OnUserInputHandler += DispatchInput;

            return true;
        }

        protected System.Type _viewType;

        public View GetView() => ViewManager.Instance.FindView(_viewType);

        public System.Type GetViewType() => _viewType;

        public void SetViewType(System.Type viewType)
        {
            _viewType = viewType;
        }

        public Model GetModel() => (Model)(GetView()?.AccessModel);

        public void SetModel(Model model)
        {
            var view = GetView();
            if (view != null)
            {
                view.AccessModel = model;
            }
        } 

        protected System.Type NextViewType { get; set; }
        protected IContext InvocationContext { get; set; }
    }
}