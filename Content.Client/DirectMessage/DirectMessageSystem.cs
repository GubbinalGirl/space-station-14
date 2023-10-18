using Content.Shared.CartridgeLoader;
using Content.Shared.DirectMessage;

namespace Content.Client.DirectMessage;

public sealed class DirectMessageSystem : EntitySystem
{
    [Dependency] private readonly EntityManager _entityManager = default!;

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
        //ev.Loader is the loader EntityUID
        if (!_entityManager.TryGetNetEntity(ev.Loader, out var netEntity))
        {
            return;
        }

        //We want to register our user. What do we do if there is no user?
        //How do I get the NetEntity of the logged in user?
        //We cast to non-nullable because we've already checked that it is not null
        RaiseNetworkEvent(new RegisterDMUserMessage((NetEntity) netEntity));
    }
}
