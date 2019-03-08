using System;
using System.Collections.Generic;

namespace Views
{
    public class MainView : MVC.View
    {
        protected override void Render()
        {
            base.Render();
        }

        public override void OnShow(MVC.IContext context)
        {
            System.Console.WriteLine("MainView: OnShow");
        }

        public override void OnUpdate()
        {
            System.Console.WriteLine("MainView: OnUpdate");
        }

        public override void OnHide()
        {
            System.Console.WriteLine("MainView: OnHide");
        }
    }
}