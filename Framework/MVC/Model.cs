using System;
using System.Reflection;

namespace MVC 
{
    public class Model 
        : IReadableModel
        , IWriteableModel
    {
        public bool GetProperty<T>(string propertyName, ref T val)
        {
            foreach (var field in Fields)
            {
                if (field.Name != propertyName || !IsModelProperty(field))
                    continue;

                val = (T)field.GetValue((object)this);
                return true;
            }

            return false;
        }

        public T GetProperty<T>(string propertyName, T retOnError = default(T))
        {
            T val = retOnError;
            bool result = GetProperty(propertyName, ref val);

            if (result == false)
                return retOnError;

            return val;
        }

        public object GetProperty(string propertyName)
        {
            return GetProperty<object>(propertyName, null);
        }

        public bool SetProperty<T>(string propertyName, T value)
        {
            foreach (var field in Fields)
            {
                if (field.Name != propertyName || !IsModelProperty(field))
                    continue;

                field.SetValue(this, (object)value);
                OnPropertySet(propertyName);
                return true;
            }

            return false;
        }

        private bool IsModelProperty(FieldInfo fieldInfo)
        {
            var attribs = System.Attribute.GetCustomAttributes(fieldInfo);
            foreach (var attrib in attribs)
                if (attrib.GetType() == typeof(ModelProperty))
                    return true;

            return false;
        }

        private FieldInfo[] Fields => this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        protected virtual void OnPropertySet(string propertyName)
        {
            if (OnPropertyUpdateHandler != null)
            {
                OnPropertyUpdateHandler(this, new PropertyUpdateEventArgs() { PropertyName = propertyName });
            }  
        }

        public event PropertyUpdateHandler OnPropertyUpdateHandler;
    }
}