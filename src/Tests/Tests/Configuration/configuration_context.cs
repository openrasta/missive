using System;
using Missive.Configuration;

namespace Tests.Configuration
{
    public abstract class configuration_context
    {
        protected IMissiveConfiguration configuration;
        protected Exception error;

        protected void given_config(Func<IMissiveConfiguration> configuration)
        {
            try
            {
                this.configuration = configuration();
            }
            catch (Exception e)
            {
                error = e;
            }
        }
        protected void given_config(IMissiveConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}