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
        }
        public string Application { get; set; }
        public ICollection<MessageModel> MessageFamilies { get; private set; }
        public string Provider { get; set; }
        public List<Type> Converters { get; private set; }
    }
}