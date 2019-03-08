using System;
using System.Collections.Generic;

using Models;
using Views;

namespace Controllers 
{
    [MVC.ControllerMeta(typeof(CarsModel), typeof(CarsView))]
    public class CarsController : MVC.Controller
    {
        public CarsController()
        {
        }

        protected override bool DispatchInput(object sender, MVC.UserInputArgs args)
        {
            switch (args.Command)
            {
                case "save":
                    SaveModel();
                    return true;
                case "add":
                    if (args.Arguments.Count != 3)
                        break;

                    AddCar(args.Arguments[0], args.Arguments[1], args.Arguments[2]);
                    return true;
                case "del":
                    if (args.Arguments.Count != 1)
                        break;

                    DeleteCar(Int32.Parse(args.Arguments[0]));
                    return true;
                case "back":
                    NextViewType = typeof(MainView);
                    return false;
                case "edit":
                    if (args.Arguments.Count != 1)
                        break;

                    NextViewType = typeof(CarView);
                    InvocationContext = new CarView.Context() { Model = GetCar(Int32.Parse(args.Arguments[0])) };
                    return false;
            }

            return base.DispatchInput(sender, args);
        }

        public void RenameCarProducer(int index, string newProdName)
        {
            ((CarsModel)GetModel()).RenameCarProducer(index, newProdName);
        }

        public void SaveModel()
        {
            ((CarsModel)GetModel()).Save();
        }

        public void AddCar(string producerName, string modelName, string bodyType)
        {
            var car = new CarModel(0, producerName, modelName, bodyType);
            ((CarsModel)GetModel()).AddCar(car);
        }

        public void DeleteCar(int index)
        {
            ((CarsModel)GetModel()).RemoveCar(index);
        }

        public CarModel GetCar(int index)
        {
            return GetModel().GetProperty<List<CarModel>>("Cars")?[index];
        }
    }
}