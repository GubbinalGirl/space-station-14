using Content.Shared.BodyScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Server.BodyScanner;

public sealed class BodyScannerSystem : EntitySystem
{
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;

    public override void Initialize()
    {
        base.Initialize();
    }
    private void UpdateAppearance(Entity<BodyScannerComponent>? entity, AppearanceComponent? appearance = null,
        StorageFillVisualizerComponent? component = null)
    {
        if (!Resolve(uid, ref storage, ref appearance, ref component, false))
            return;

        if (component.MaxFillLevels < 1)
            return;

        var level = ContentHelpers.RoundToEqualLevels(storage.StorageUsed, storage.StorageCapacityMax, component.MaxFillLevels);
        _appearance.SetData(uid, StorageFillVisuals.FillLevel, level, appearance);
    }

}
