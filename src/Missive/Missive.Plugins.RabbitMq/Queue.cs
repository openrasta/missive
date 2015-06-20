namespace Missive.Plugins.RabbitMq
{
    public class Queue
    {
        public string Name { get; set; }
        public bool Persistent { get; set; }

        public Queue(string name)
        {
            Name = name;
        }
    }
}