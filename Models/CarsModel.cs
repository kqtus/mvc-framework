using System;
using System.Collections.Generic;

namespace Models
{
    public class CarsModel 
        : MVC.Model
        , MVC.IFetchableModel
    {
        [MVC.ModelProperty]
        protected List<CarModel> Cars;

        protected DAL.CarsDAO _dao;

        public CarsModel(Core.DataAccessObject dao)
        {
            _dao = (DAL.CarsDAO)dao;
        }

        public bool Fetch()
        {
            if (_dao == null)
                return false;

            var cars = _dao.GetAllCars();
            SetProperty("Cars", cars);

            return true;
        }

        public bool Save()
        {
            if (_dao == null)
                return false;
            
            return _dao.SaveCars(Cars);
        }

        public void AddCar(CarModel car)
        {
            Cars.Add(car);
            SetProperty("Cars", Cars);
        }

        public void RemoveCar(int index)
        {
            Cars.RemoveAt(index);
            SetProperty("Cars", Cars);
        }

        public bool RenameCarProducer(int index, string newProdName)
        {
            if (index >= Cars.Count || index < 0)
                return false;

            Cars[index].SetProperty("ProducerName", newProdName);
            SetProperty("Cars", Cars);
            System.Console.WriteLine(Cars[index].GetProperty("Cars"));
            return true;
        }
    }
}