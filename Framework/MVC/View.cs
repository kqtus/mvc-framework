using System;
using System.Collections.Generic;

namespace MVC 
{
    public class UserInputArgs : EventArgs
    {
        public string Input { get; set; }

        public string Command { get; set; }

        public List<string> Arguments { get; set; }

        public UserInputArgs(string input)
        {
            Input = input;
            var tokens = new List<string>(input.Split(' '));

            if (tokens.Count > 0)
            {
                Command = tokens[0];
                tokens.RemoveAt(0);
                Arguments = tokens;
            }
        }
    }

    public delegate void HidingHandler(object sender, EventArgs args);
    public delegate bool UserInputHandler(object sender, UserInputArgs args);

    public abstract class View 
    {
        public event HidingHandler OnHideHandler;
        public event UserInputHandler OnUserInputHandler;

        public void Show(IContext context)
        {
            Clear();
            OnShow(context);
            Render();

            while (DispatchInput(Console.ReadLine()) == true)
            {
                Update();
            }

            if (OnHideHandler != null)
            {
                OnHideHandler(this, new EventArgs());
            }    
        }

        /*
            Clears view.
        */
        private void Clear()
        {
            System.Console.Clear();
        }

        /*
            Renders content of view.
        */
        protected virtual void Render()
        {
            System.Console.Write(">");
        }

        /*
            Dispatches user input.
            Returns true if view needs to be updated.
            Returns false if view needs to be hidden.
        */
        protected bool DispatchInput(string input)
        {
            if (OnUserInputHandler == null)
                return true;
            
            return OnUserInputHandler(this, new UserInputArgs(input));
        }

        public virtual void OnShow(IContext context) { }

        /*
            Updates view.
        */
        public void Update()
        {
            Clear();
            OnUpdate();
            Render();
        }

        public virtual void OnUpdate() { }

        public void Hide()
        { 
            Clear();
            OnHide();
        }

        public virtual void OnHide() { }

        public virtual void OnModelUpdate(object sender, PropertyUpdateEventArgs args) { }

        private IReadableModel _model;
        public IReadableModel AccessModel 
        {
            get => _model;
            set
            {
                if (_model != null)
                {
                    _model.OnPropertyUpdateHandler -= OnModelUpdate;
                }

                _model = value;
                if (_model != null)
                    _model.OnPropertyUpdateHandler += OnModelUpdate;
                
                OnUpdate();
            }
        }

        protected void Bind<T>(string modelProperty, ref T destProperty)
        {

        }

        protected void Unbind<T>(ref T destProperty)
        {

        }

        private Dictionary<string, object> _propertyBindings;
    }
}