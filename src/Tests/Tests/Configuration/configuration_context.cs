using Missive.Configuration;

namespace Tests.Configuration
{
    public abstract class configuration_context
    {
        protected IMissiveConfiguration configuration;

        protected void given_config(IMissiveConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}