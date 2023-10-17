using Content.Shared.CartridgeLoader;

namespace Content.Client.DirectMessage;

public sealed class DirectMessageSystem : EntitySystem
{


    public override void Initialize()
    {
        base.Initialize();

        //Do I need to subscribe to a LocalEvent? or a NetworkEvent?
        SubscribeAllEvent<CartridgeActivatedEvent>(OnCartridgeActivated);
    }

    public override void Shutdown()
    {
        base.Shutdown();
    }

    public void OnCartridgeActivated(CartridgeActivatedEvent ev)
    {
        //We want to register our user. What do we do if there is no user?
    }
}
