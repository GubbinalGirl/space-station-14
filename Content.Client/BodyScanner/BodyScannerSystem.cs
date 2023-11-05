using Content.Shared.BodyScanner;
using Robust.Client.GameObjects;

namespace Content.Client.BodyScanner;

public sealed class BodyScannerSystem : SharedBodyScannerSystem
{
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<BodyScannerComponent, AppearanceChangeEvent>(OnBodyScannerVisualsChange);
    }

    private void OnBodyScannerVisualsChange(Entity<BodyScannerComponent> entity, ref AppearanceChangeEvent args)
    {
        var appearance = args.Component;

        if (args.Sprite == null)
            return;

        if (entity.Comp.IsAlerted)
        {
            args.Sprite.LayerSetVisible(BodyScannerVisuals.Alerted, true);
            args.Sprite.LayerSetVisible(BodyScannerVisuals.Base, false);
            args.Sprite.LayerSetState(BodyScannerVisuals.Alerted, "alerted");

        }
        else
        {
            args.Sprite.LayerSetVisible(BodyScannerVisuals.Alerted, false);
            args.Sprite.LayerSetVisible(BodyScannerVisuals.Base, true);
            args.Sprite.LayerSetState(BodyScannerVisuals.Alerted, "base");
        }
    }
}
