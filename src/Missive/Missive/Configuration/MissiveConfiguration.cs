namespace Missive.Configuration
{
    public class MissiveConfiguration : IMissiveConfiguration
    {
        private ConfigurationModel _configurationModel;

        public MissiveConfiguration()
        {
            _configurationModel = new ConfigurationModel();
        }
        
        ConfigurationModel IMissiveConfiguration.ConfigurationModel { get { return _configurationModel; }}
    }
}