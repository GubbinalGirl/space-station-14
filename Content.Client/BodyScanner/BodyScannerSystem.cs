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
        SubscribeLocalEvent<BodyScannerComponent, AppearanceChangeEvent>(OnBodyScannerVisualsChange);
    }

    private void OnBodyScannerVisualsChange(Entity<BodyScannerComponent> entity, ref AppearanceChangeEvent args)
    {
        Log.Info("Changing appearance.");

        var appearance = args.Component;

        if (args.Sprite == null)
            return;
        Log.Info("Sprite ain't null.");

        //Bail if there is no relevant key
        if (!args.AppearanceData.ContainsKey(BodyScannerVisuals.Alerted))
            return;
        Log.Info("Relevant key");

        if (!args.AppearanceData.TryGetValue(BodyScannerVisuals.Alerted, out var alerted))
            return;
        Log.Info("Got key value");

        args.Sprite.LayerSetVisible(BodyScannerVisuals.Alerted, (bool)alerted);
    }
}
