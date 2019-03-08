using System;

using Models;

namespace Views
{
    public class CarView : MVC.View
    {
        public class Context : MVC.IContext
        {
            public CarModel Model { get; set; }
        }

        protected override void Render()
        {
            var model = (CarModel)AccessModel;
            if (model == null)
                return;
            
            System.Console.WriteLine(String.Format
            (
                "{0, 15} | {1, 10} | {2, 10}",
                "ProducerName",
                "ModelName",
                "BodyType"
            ));

            System.Console.WriteLine(String.Format
            (
                "{0, 15} | {1, 10} | {2, 10}",
                (string)model.GetProperty("ProducerName"),
                (string)model.GetProperty("ModelName"),
                (string)model.GetProperty("BodyType")
            ));

            base.Render();
        }

        public override void OnShow(MVC.IContext context)
        {
            System.Console.WriteLine("CarView: OnShow");

            if (context != null)
                AccessModel = ((CarView.Context)context).Model;
        }

        public override void OnUpdate()
        {
            System.Console.WriteLine("CarView: OnUpdate");
        }

        public override void OnHide()
        {
            System.Console.WriteLine("CarView: OnHide");
        }
    }
}