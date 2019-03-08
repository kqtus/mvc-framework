using System;
using Core;
using DAL;

namespace Models.Domain
{
    public class DAOFactory
    {
        public string DatabasePath { get; set; }

        public DAOFactory(string dbPath)
        {
            DatabasePath = dbPath;
        }

        public bool Init()
        {
            DataAccessObject.InitConnection(DatabasePath);
            return true;
        }

        protected DataAccessObject _cars;

        protected DataAccessObject InitCars()
        {
            if (_cars == null)
                _cars = new CarsDAO(new DaoDescriptor() { TableName = CarsTableName });
            
            return _cars;
        }

        public DataAccessObject Cars => _cars == null ? InitCars() : _cars;

        public string CarsTableName => "Cars";


    }
}