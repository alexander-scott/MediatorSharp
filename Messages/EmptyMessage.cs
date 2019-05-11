namespace Messaging.Messages
{
    /// <summary>
    /// IMessage implementation that holds no references and is used as an event or trigger for a system.
    /// </summary>
    public class EmptyMessage : IMessage
    {
        public MessageType MessageType { get; }

        public EmptyMessage(MessageType type)
        {
            MessageType = type;
        }
    }
}
