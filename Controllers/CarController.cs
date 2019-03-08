using System;

using Models;
using Views;

namespace Controllers
{
    [MVC.ControllerMeta(typeof(Models.CarModel), typeof(Views.CarView))]
    public class CarController : MVC.Controller
    {
        public CarController()
        {

        }

        protected override bool DispatchInput(object sender, MVC.UserInputArgs args)
        {
            switch (args.Command)
            {
                case "back":
                    NextViewType = typeof(CarsView);
                    return false;

                case "ch-prod":
                    if (args.Arguments.Count != 1)
                        break;

                    ChangeProducer(args.Arguments[0]);
                    return true;

                case "ch-model":
                    if (args.Arguments.Count != 1)
                        break;

                    ChangeModelName(args.Arguments[0]);
                    return true;
            }

            return base.DispatchInput(sender, args);
        }

        public void ChangeProducer(string prodName)
        {
            ((CarModel)GetModel()).SetProperty("ProducerName", prodName);
        }

        public void ChangeModelName(string modelName)
        {
            ((CarModel)GetModel()).SetProperty("ModelName", modelName);
        }
    }
}