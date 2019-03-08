using System;
using System.Reflection;
using System.Collections.Generic;

namespace Controllers
{
    [MVC.ControllerMeta(null, typeof(Views.MainView))]
    public class MainController : MVC.Controller
    {
        public MainController()
        {
        }

        protected override bool DispatchInput(object sender, MVC.UserInputArgs args)
        {
            switch (args.Command)
            {
                case "show":
                    if (args.Arguments.Count != 1)
                        break;
                    
                    switch (args.Arguments[0].ToLower())
                    {
                        case "cars":
                            NextViewType = typeof(Views.CarsView);
                            return false;
                    }
                    break;
            }

            return base.DispatchInput(sender, args);
        }
    }
}