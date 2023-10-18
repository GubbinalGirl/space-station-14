using Content.Shared.DirectMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Server.DirectMessage;

public sealed class DirectMessageSystem : EntitySystem
{
    //Each registered DM user has an associated List of messages
    private readonly Dictionary<EntityUid, List<DirectMessageEntry>> _messages_per_user;

    public override void Initialize()
    {
        base.Initialize();
    }
}
