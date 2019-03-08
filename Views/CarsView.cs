using System;
using System.Collections.Generic;

using Models;

namespace Views 
{
    public class CarsView : MVC.View
    {   
        protected override void Render()
        {
            var model = (CarsModel)AccessModel;
            if (model == null)
                return;

            System.Console.WriteLine(String.Format
            (
                "{0, 15} | {1, 10} | {2, 10}",
                "ProducerName",
                "ModelName",
                "BodyType"
            ));

            foreach (var car in (List<CarModel>)model.GetProperty("Cars"))
            {
                System.Console.WriteLine(String.Format
                (
                    "{0, 15} | {1, 10} | {2, 10}",
                    (string)car.GetProperty("ProducerName"),
                    (string)car.GetProperty("ModelName"),
                    (string)car.GetProperty("BodyType")
                ));
            }

            base.Render();
        }

        public override void OnShow(MVC.IContext context)
        {
            System.Console.WriteLine("CarsView: OnShow");
        }

        public override void OnUpdate()
        {
            System.Console.WriteLine("CarsView: OnUpdate");
        }

        public override void OnHide()
        {
            System.Console.WriteLine("CarsView: OnHide");
        }

        public override void OnModelUpdate(object sender, MVC.PropertyUpdateEventArgs args)
        {
            System.Console.WriteLine("CarsView: OnModelUpdate");
        }
    }
}