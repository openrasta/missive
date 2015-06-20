using System;
using Missive;
using Missive.Configuration;
using Tests.Configuration;

namespace Tests.contexts
{
    public abstract class configurable
    {
        protected IMissiveConfiguration configuration;
        protected Exception error;
        private MissiveMessaging missive;

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

        protected void when_preparing_config()
        {
            configuration.ConfigurationModel.RaisePrepare();
        }

        protected void when_publishing(Fruit fruit)
        {
            missive.Advanced.Components.Publisher<Fruit>().Send(fruit);
        }

        protected void given_missive(IMissiveConfiguration configuration)
        {
            this.missive = new MissiveMessaging(configuration);
            this.missive.Start();
        }
    }
}