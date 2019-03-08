using System;

namespace Models 
{
    public class CarModel : MVC.Model
    {
        [MVC.ModelProperty]
        protected int Id;

        [MVC.ModelProperty]
        protected string ProducerName;

        [MVC.ModelProperty]
        protected string ModelName;

        [MVC.ModelProperty]
        protected string BodyType;

        public CarModel()
        {
        }

        public CarModel(int id, string producerName, string modelName, string bodyType)
        {
            Id = id;
            ProducerName = producerName;
            ModelName = modelName;
            BodyType = bodyType;
        }
    }
}