using Content.Client.PowerCell;
using Content.Shared.BodyScanner;
using Robust.Client.GameObjects;

namespace Content.Client.BodyScanner;

public sealed class BodyScannerSystem : SharedBodyScannerSystem
{
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<BodyScannerVisualsComponent, AppearanceChangeEvent>(OnBodyScannerVisualsChange);
    }

    private void OnBodyScannerVisualsChange(Entity<BodyScannerVisualsComponent> entity, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        args.Sprite.LayerSetVisible(BodyScannerVisuals.Alerted, entity.Comp.IsAlerted);
    }
}
