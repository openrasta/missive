using System.Collections.Generic;

namespace Missive
{
    public class ObjectMessage<T>
    {
        public ObjectMessage()
        {
            Headers = new MessageHeaders();
        }
        public T Instance { get; set; }
        public MessageHeaders Headers { get; private set; }
    }

    public class MessageHeaders : Dictionary<string,object>
    {
        
    }

    public class RawMessage
    {
        public MessageHeaders Headers { get; private set; }
        public byte[] Body { get; set; }
    
    }
}