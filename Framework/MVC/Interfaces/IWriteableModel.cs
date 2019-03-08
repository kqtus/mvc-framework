using System;

namespace MVC
{
    public interface IWriteableModel
    {
        bool SetProperty<T>(string propertyName, T value);
    }
}