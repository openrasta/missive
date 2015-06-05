namespace Missive.Configuration
{
    public static class MessageExtensions
    {
        public static IMessageConfiguration<TMessage> OfType<TMessage>(this IMessageSection configuration)
        {
            var newModel = new MessageModel() { Type = typeof(TMessage), Covariant = true };
            configuration.ConfigurationModel.MessageFamilies.Add(newModel);
            return new TypedMessageConfig<TMessage>(configuration, newModel);
        }

        public static T Only<T>(this T messageConfiguration, bool onlySpecificType = true)
            where T : IMessageConfiguration
        {
            messageConfiguration.MessageModel.Covariant = !onlySpecificType;
            return messageConfiguration;
        }
    }
}