using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Core
{
    public struct DaoDescriptor
    {
        public string DbPath { get; set; }
        public string TableName { get; set; }
    }

    public class DataAccessObject
    {
        private DaoDescriptor _daoDescriptor;

        private static SQLiteConnection _dbConnection;

        public DataAccessObject(DaoDescriptor daoDescriptor)
        {
            _daoDescriptor = daoDescriptor;
        }

        public static void InitConnection(string dbPath)
        {
            _dbConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", dbPath));
            _dbConnection.Open();
        }

        public static void CloseConnection()
        {
            _dbConnection.Close();
        }

        protected int ExecNoRet(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, _dbConnection);
            return command.ExecuteNonQuery();
        }

        protected List<NameValueCollection> Exec(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, _dbConnection);
            
            var ret = new List<NameValueCollection>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ret.Add(reader.GetValues());
                }
            }
            
            return ret;
        }
    }
}