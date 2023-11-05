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

    /// <summary>
    /// When the player steps onto the detector this is called. We use a portal fixture with the hard option off to allow this.
    /// </summary>
    /// <param name="bodyScanner"></param>
    /// <param name="args"></param>
    private void OnCollide(Entity<BodyScannerComponent> bodyScanner, ref StartCollideEvent args)
    {
        var other = args.OtherEntity;

        //Try and get the other's Inventory
        var otherInventory = _inventory.GetHandOrInventoryEntities(other);

        //Iterate through it's total inventory
        foreach (var i in otherInventory)
        {
            //Check each item to determine if its the target type
            //For now just check if an entity has the gun component
            if (!TryComp<GunComponent>(i, out var gunComp))
                continue;

            //If the item is a gun then alert somehow
            bodyScanner.Comp.IsAlerted = true;
            Dirty(bodyScanner);
            UpdateAppearance(bodyScanner);

            var ev = new ContrabandDetectedEvent(bodyScanner.Owner, other, i);
            RaiseNetworkEvent(ev);
        }
    }

    /// <summary>
    /// This is treated as a person stepping off the detector pad.
    /// </summary>
    /// <param name="bodyScanner"></param>
    /// <param name="args"></param>
    private void OnEndCollide(Entity<BodyScannerComponent> bodyScanner, ref EndCollideEvent args)
    {
        bodyScanner.Comp.IsAlerted = false;
        Dirty(bodyScanner.Owner, bodyScanner.Comp);
        UpdateAppearance(bodyScanner);
    }

    //AppearanceComponent is how the server communicates visualizer data to the client
    private void UpdateAppearance(Entity<BodyScannerComponent> bodyScanner)
    {
        AppearanceComponent? appearance = null;

        //Do I need to try and get the AppearanceComponent?
        if (!Resolve(bodyScanner.Owner, ref appearance))
            return;

        _appearance.SetData(bodyScanner.Owner, BodyScannerVisuals.Alerted, bodyScanner.Comp.IsAlerted, appearance);
    }
}
