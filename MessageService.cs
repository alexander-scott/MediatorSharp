using System;
using System.Collections.Generic;

namespace Messaging
{
    public enum MessageType
    {
        ApplicationStarted
        // Add more MessageTypes here.
    }

    /// <summary>
    /// Core interface that all messages must implement. It also has a MessageType enum which is used by the MessageService to
    /// pass the correct messages to each MessageType's subscriber.
    /// </summary>
    public interface IMessage
    {
        MessageType MessageType { get; }
    }
    
    /// <summary>
    /// Instance that is used by all systems to pass messages to each other. A system must initially subscribe to a MessageType then, if
    /// the MessageService receives a message of that type it will forward it to that system. 
    /// </summary>
    public sealed class MessageService
    {
        public MessageService()
        {
            _subscriptionMap = new Dictionary<MessageType, List<Action<IMessage>>>();
        }

        /// <summary>
        /// Dictionary that maps a subscribed system to their chosen MessageType. Each subscription is an action (function call) that is called
        /// by the MessageService when a message of that type is received. 
        /// </summary>
        private readonly Dictionary<MessageType, List<Action<IMessage>>> _subscriptionMap;

        /// <summary>
        /// Sends a message to any systems which have subscribed to its message type.
        /// </summary>
        /// <param name="message">The Message object that will be sent to subscribed systems.</param>
        public void SendMessage(IMessage message)
        {
            if (!_subscriptionMap.ContainsKey(message.MessageType))
            {
                return;
            }

            for (int i = 0; i < _subscriptionMap[message.MessageType].Count; i++)
            {
                _subscriptionMap[message.MessageType][i].Invoke(message);
            }
        }

        /// <summary>
        /// Subscribes a system to a message type.
        /// </summary>
        /// <param name="subscriber">An action (function call) that should be called if a certain message type is received.</param>
        /// <param name="messageType">The message type that the action is subscribing to.</param>
        public void Subscribe(Action<IMessage> subscriber, MessageType messageType)
        {
            if (!_subscriptionMap.ContainsKey(messageType))
            {
                _subscriptionMap[messageType] = new List<Action<IMessage>>();
            }

            _subscriptionMap[messageType].Add(subscriber);
        }

        /// <summary>
        /// Removes a systems subscription to a specific message type.
        /// </summary>
        /// <param name="subscriber">An action (function call) that should be called if a certain message type is received.</param>
        /// <param name="messageType">The message type that the action is unsubscribing from.</param>
        public void RemoveSubscription(Action<IMessage> subscriber, MessageType messageType)
        {
            if (!_subscriptionMap.ContainsKey(messageType) || !_subscriptionMap[messageType].Contains(subscriber))
            {
                // Attempting to remove a subscription when it does not exist in the message service
                return;
            }

            _subscriptionMap[messageType].Remove(subscriber);
        }

        /// <summary>
        /// Returns the number of subscriptions a specific message type has
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CountSubscriptionsOfType(MessageType type)
        {
            if (!_subscriptionMap.ContainsKey(type))
            {
                // Attempting to count the subscriptions of a type when it does not exist in the message service
                return -1;
            }

            return _subscriptionMap[type].Count;
        }
    }
}