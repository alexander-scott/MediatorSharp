using Core;

namespace Messaging.Messages
{
    /// <summary>
    /// IMessage implementation that holds a reference to a string.
    /// </summary>
    public class StringMessage : IMessage
    {
        public MessageType MessageType { get; }

        public string String { get; }

        public StringMessage(MessageType type, string s)
        {
            MessageType = type;
            String = s;
        }
    }
}