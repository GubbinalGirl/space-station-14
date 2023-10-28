using Robust.Shared.Physics.Events;
using Content.Shared.BodyScanner;
using Content.Shared.Inventory;
using Content.Shared.Weapons.Ranged.Components;

namespace Content.Server.BodyScanner;

public sealed class BodyScannerSystem : SharedBodyScannerSystem
{
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;
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
            Log.Info("Gun detected.");
            //If the item is a gun then alert somehow
            UpdateAppearance(bodyScanner);
        }
    }

    private void OnEndCollide(Entity<BodyScannerComponent> bodyScanner, ref EndCollideEvent args)
    {

    }

    //AppearanceComponent is how the server communicates visualizer data to the client
    private void UpdateAppearance(Entity<BodyScannerComponent> bodyScanner)
    {

    }
}
