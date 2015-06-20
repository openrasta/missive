using System;
using System.Collections.Generic;

namespace Missive.Configuration
{
    public class ConfigurationModel
    {
        public ConfigurationModel()
        {
            MessageFamilies = new List<MessageModel>();
            Converters = new List<Type>();
            Handlers = new List<HandlerModel>();
            Extensions = new Dictionary<string, object>();
        }
        public string Application { get; set; }
        public ICollection<MessageModel> MessageFamilies { get; private set; }
        public string Provider { get; set; }
        public List<Type> Converters { get; private set; }
        public ICollection<HandlerModel> Handlers { get; private set; }
        public IDictionary<string, object> Extensions { get; private set; }
        public event EventHandler<ConfigurationModel> Prepare;
        public void RaisePrepare()
        {
            var prepare = Prepare;
            if (prepare != null)
                prepare(this, this);
        }
    }
}