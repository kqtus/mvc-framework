using System;

using MVC;

using Models;
using Views;
using Controllers;

namespace lab0
{
    class Program
    {
        static void Main(string[] args)
        {
            Models.Domain.DAOFactory daoFactory = new Models.Domain.DAOFactory("db.sqlite");
            daoFactory.Init();

            MVC.AppBroker broker = new MVC.AppBroker(typeof(MainController));
            broker.DaoToModelBindings.Add(new DaoToModelBinding(daoFactory.Cars, typeof(CarsModel)));
            broker.Run();
        }
    }
}
