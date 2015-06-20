using System;
using Missive.Configuration;

namespace Missive
{
    public class MissiveMessaging
    {
        private readonly IMissiveConfiguration _configuration;

        public MissiveMessaging(IMissiveConfiguration configuration)
        {
            _configuration = configuration;
            Advanced = new MissiveInternals();
        }

        public MissiveInternals Advanced { get; private set; }
        public void Start()
        {
            
        }
    }

    public class MissiveInternals
    {
        public MissiveInternals()
        {
             Components = new ComponentFactory();
        }
        public ComponentFactory Components { get; private set; }
    }

    public class ComponentFactory
    {
        
        public IPublisher<T> Publisher<T>()
        {
            return new ObjectPublisher<T>();
        }
    }

    public class ObjectPublisher<T> : IPublisher<T>
    {
        public void Send(T fruit)
        {
            
        }
    }

    public interface IPublisher<T>
    {
        void Send(T fruit);
    }
}