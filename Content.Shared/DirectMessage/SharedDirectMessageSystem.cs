using Content.Shared.Eui;
using Robust.Shared.Serialization;

namespace Content.Shared.DirectMessage;

// <summary>
//      Request all the messages between the given pair of users
// </summary>
[Serializable, NetSerializable]
public sealed class RequestDirectMessagesForPairMessage : EntityEventArgs
{
    public RequestDirectMessagesForPairMessage() { }
}

// <summary>
//      Request all the known contacts for a given user. Currently just returns the entire crew.
// </summary>
[Serializable, NetSerializable]
public sealed class RequestContactsForUserMessage : EntityEventArgs
{
    public RequestContactsForUserMessage() { }
}

// <summary>
//      Sends a message from the user sender to the user recipient
// </summary>
[Serializable, NetSerializable]
public sealed class SendDirectMessageToUserMessage : EntityEventArgs
{
    public SendDirectMessageToUserMessage() { }
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
