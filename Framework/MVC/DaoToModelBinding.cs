using System;

namespace MVC
{
    public class DaoToModelBinding
    {
        public Core.DataAccessObject DAO { get; set; }
        public System.Type DestModelType { get; set; }

        public DaoToModelBinding(Core.DataAccessObject dataAccessObject, System.Type destModelType)
        {
            DAO = dataAccessObject;
            DestModelType = destModelType;
        }
    }
}