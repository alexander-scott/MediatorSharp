# MediatorSharp

A basic implementation of the Mediator design pattern that is used to promote decoupling between different systems, as described [here](https://sourcemaking.com/design_patterns/mediator).

## Usage

Create an instance of the MessageService class when your application first starts. Then, when creating your systems, inject the MessageService instance as a dependency. Systems can then subscribe to a specific message type by providing a function callback and the message type:

```csharp
_messageService.Subscribe(ApplicationStarted, MessageType.ApplicationStarted);
```

To send messages between systems you simply need to send an IMessage to the MessageService:

```csharp
 _messageService.SendMessage(new EmptyMessage(MessageType.ApplicationStarted));
```

In the callback function of a subscribed system, the IMessage can be cast into whatever data type your system requires:

```csharp
private void ReceiveStringMessage(IMessage obj)
{
    StringMessage msg = (StringMessage) obj;
    Debug.Log("Receive message: " + msg.String)
}
```

Additional message types can be constructing by defining more values in the MessageType enum and by also creating classes that inherit from IMessage.