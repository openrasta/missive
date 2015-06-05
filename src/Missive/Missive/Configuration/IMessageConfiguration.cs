namespace Missive.Configuration
{
    public interface IMessageConfiguration<TMessage> : IMessageConfiguration
    {
    }

    public interface IMessageConfiguration : IMessageSection
    {
        MessageModel MessageModel { get; }
    }
}