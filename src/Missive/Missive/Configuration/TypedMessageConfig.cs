namespace Missive.Configuration
{
    public class TypedMessageConfig<TMessage> : IMessageConfiguration<TMessage>
    {
        private readonly IMissiveConfiguration _configuration;
        private readonly MessageModel _messageMessageModel;

        public TypedMessageConfig(IMissiveConfiguration configuration, MessageModel messageMessageModel)
        {
            _configuration = configuration;
            _messageMessageModel = messageMessageModel;
            _messageMessageModel.Type = typeof (TMessage);
        }

        public ConfigurationModel ConfigurationModel
        {
            get { return _configuration.ConfigurationModel; }
        }

        MessageModel IMessageConfiguration.MessageModel
        {
            get { return _messageMessageModel; }
        }
    }
}