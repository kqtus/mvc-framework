using System;

namespace MVC
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class ModelProperty : System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ControllerMeta : System.Attribute
    {
        public System.Type ModelType { get; set; }
        public System.Type ViewType { get; set; }

        public ControllerMeta(System.Type modelType, System.Type viewType)
        {
            ModelType = modelType;
            ViewType = viewType;
        }
    }
}