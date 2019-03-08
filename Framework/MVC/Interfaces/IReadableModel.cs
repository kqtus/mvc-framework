using System;

namespace MVC
{
    public class PropertyUpdateEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
    }

    public delegate void PropertyUpdateHandler(object sender, PropertyUpdateEventArgs args);

    public interface IReadableModel
    {
        bool GetProperty<T>(string propertyName, ref T val);
        T GetProperty<T>(string propertyName, T retOnError = default(T));
        object GetProperty(string propertyName);
        
        event PropertyUpdateHandler OnPropertyUpdateHandler;
    }
}