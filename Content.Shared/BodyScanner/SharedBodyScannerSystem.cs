using Content.Shared.Inventory;
using Content.Shared.Weapons.Ranged.Components;
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

    private void OnCollide(Entity<BodyScannerComponent> bodyScanner, ref StartCollideEvent args)
    {
        //Check if the other entity has a bodycomponent or inventorycomponent
        var other = args.OtherEntity;

        //Try and get the other's Inventory
        var otherInventory = _inventory.GetHandOrInventoryEntities(other);

        //If yes, then iterate through it's total inventory
        foreach (var i in otherInventory)
        {
            //Check each item to determine if its the target type
            //For now just check if an entity has the gun component
            if (!TryComp<GunComponent>(i, out var gunComp))
                continue;

            //If the item is a gun then alert somehow
        }


    }

    private void OnEndCollideEntity<BodyScannerComponent> bodyScanner, ref EndCollideEvent args)
    {

    }
}

public abstract class ContrabandDetectedEvent : EntityEventArgs
{
    /// <summary>
    /// The BodyScanner or other detector that detected the contraband.
    /// </summary>
    public readonly EntityUid Detector;

    /// <summary>
    /// The item that was detected as contraband.
    /// </summary>
    public readonly EntityUid DetectedItem;

    /// <summary>
    /// The entity that holds the detected object.
    /// </summary>
    public readonly EntityUid DetectedHolder;
}
