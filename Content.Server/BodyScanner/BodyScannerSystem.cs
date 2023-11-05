using Robust.Shared.Physics.Events;
using Content.Shared.BodyScanner;
using Content.Shared.Inventory;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Storage.Components;

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

    private void OnInit(Entity<BodyScannerComponent> bodyScanner, ComponentInit args)
    {
        UpdateAppearance(bodyScanner);
    }

    private void OnCollide(Entity<BodyScannerComponent> bodyScanner, ref StartCollideEvent args)
    {
        Log.Info("Bodyscanner OnCollide.");
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
            bodyScanner.Comp.IsAlerted = true;
            Dirty(bodyScanner.Owner, bodyScanner.Comp);
            UpdateAppearance(bodyScanner);
        }
    }

    private void OnEndCollide(Entity<BodyScannerComponent> bodyScanner, ref EndCollideEvent args)
    {
        Log.Info("End Collide.");
        bodyScanner.Comp.IsAlerted = false;
        Dirty(bodyScanner.Owner, bodyScanner.Comp);
        UpdateAppearance(bodyScanner);
    }

    //AppearanceComponent is how the server communicates visualizer data to the client
    private void UpdateAppearance(Entity<BodyScannerComponent> bodyScanner)
    {
        Log.Info("Updating Bodyscanner Appearance.");
        AppearanceComponent? appearance = null;

        //Do I need to try and get the AppearanceComponent?
        if (!Resolve(bodyScanner.Owner, ref appearance))
        {
            Log.Info("Couldn't resolve");
            return;
        }

        Log.Info(String.Format("isAlerted: {0}.", bodyScanner.Comp.IsAlerted));

        if (bodyScanner.Comp.IsAlerted)
            _appearance.SetData(bodyScanner.Owner, BodyScannerVisuals.Alerted, true, appearance);
        else
            _appearance.SetData(bodyScanner.Owner, BodyScannerVisuals.Alerted, false, appearance);
    }
}
