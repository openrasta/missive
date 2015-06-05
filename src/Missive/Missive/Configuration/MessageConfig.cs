namespace Missive.Configuration
{
    public class MessageConfig : IMessageSection
    {
        private readonly IMissiveConfiguration _configuration;
        private MessageModel _model;

        public MessageConfig(IMissiveConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        ConfigurationModel IMissiveConfiguration.ConfigurationModel { get { return _configuration.ConfigurationModel; } }
        
    }
}