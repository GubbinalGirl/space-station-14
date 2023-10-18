using Content.Shared.Eui;
using Robust.Shared.Serialization;

namespace Content.Shared.DirectMessage;

// <summary>
//      Sends a message from the user sender to the user recipient
// </summary>
[Serializable, NetSerializable]
public sealed class SendDirectMessageToUserMessage : EntityEventArgs
{
    public DirectMessageEntry? Message { get; }
    public NetEntity Sender { get; }
    public NetEntity Receiver { get; }
    public SendDirectMessageToUserMessage(DirectMessageEntry message, NetEntity sender, NetEntity receiver)
    {
        Message = message;
        Sender = sender;
        Receiver = receiver;
    }
}

// <summary>
//      Registers a user with the DM server when the app is first opened
// </summary>
[Serializable, NetSerializable]
public sealed class RegisterDMUserMessage : EntityEventArgs
{
    public NetEntity UserUID { get; }

    /// <summary>
    ///     
    /// </summary>
    /// <param name="userUID">The uid of the DM user that is registering</param>
    public RegisterDMUserMessage(NetEntity userUID)
    {
        UserUID = userUID;
    }
}

[Serializable, NetSerializable]
public sealed class DirectMessageEntry
{
    public string Sender { get; }
    public string Recipient { get; }
    public string MessageText { get; }

    public DirectMessageEntry(string sender, string recipient, string messageText)
    {
        Sender = sender;
        Recipient = recipient;
        MessageText = messageText;
    }
}
