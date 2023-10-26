using Content.Shared.Inventory;
using Robust.Shared.Physics.Events;

namespace Content.Shared.BodyScanner;

public abstract class SharedBodyScannerSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventory = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<BodyScannerComponent, StartCollideEvent>(OnCollide);
        SubscribeLocalEvent<BodyScannerComponent, EndCollideEvent>(OnEndCollide);
    }

    private void OnCollide(EntityUid uid, BodyScannerComponent component, ref StartCollideEvent args)
    {
        //Check if the other entity has a bodycomponent or inventorycomponent
        var other = args.OtherEntity;

        //Try and get the other's Inventory
        var otherInventory = _inventory.GetHandOrInventoryEntities(other);

        //If yes, then iterate through it's total inventory
        foreach (var i in otherInventory)
        {
            //Check each item to determine if its the target type

        }

        //For now just check if an entity has the gun component
    }

    private void OnEndCollide(EntityUid uid, BodyScannerComponent component, ref EndCollideEvent args)
    {

    }
}
