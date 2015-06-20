using Missive.Configuration;

namespace Missive.Plugins.RabbitMq
{
    public static class RabbitConfigurationExtensions
    {
        public static RabbitModel Rabbit(this ConfigurationModel model)
        {
            object rabbitModel;
            if (!model.Extensions.TryGetValue("rabbit", out rabbitModel))
                rabbitModel = model.Extensions["rabbit"] = new RabbitModel();
            return (RabbitModel)rabbitModel;
        }
    }
}