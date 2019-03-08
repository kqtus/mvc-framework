using System;
using System.Data.SQLite;
using System.Collections.Generic;
using Core;

using Models;

namespace DAL
{
    public class CarsDAO : DataAccessObject
    {
        public CarsDAO(DaoDescriptor descriptor)
            : base(descriptor)
        {
        }

        public List<CarModel> GetAllCars()
        {
            var query = "SELECT * FROM Cars";
            var carsData = Exec(query);

            List<CarModel> cars = new List<CarModel>();
            foreach (var carData in carsData)
            {
                CarModel model = new CarModel
                (
                    Int32.Parse(carData["Id"]),
                    carData["ProducerName"], 
                    carData["ModelName"],
                    carData["BodyType"]
                );
                cars.Add(model);
            }

            return cars;
        }

        public bool AddCar(CarModel model)
        {
            var query = String.Format
            (
                "INSERT INTO Cars(ProducerName, ModelName, BodyType) VALUES ('{0}', '{1}', '{2}')", 
                (string)model.GetProperty("ProducerName"),
                (string)model.GetProperty("ModelName"),
                (string)model.GetProperty("BodyType")
            );

            return ExecNoRet(query) > 0;
        }

        public bool SaveCars(List<CarModel> carModels)
        {
            var query = "DELETE FROM Cars";
            ExecNoRet(query);

            foreach (var carModel in carModels)
                AddCar(carModel);

            return true;
        }
    }
}