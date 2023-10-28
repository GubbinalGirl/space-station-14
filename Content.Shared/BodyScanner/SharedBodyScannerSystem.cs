namespace Content.Shared.BodyScanner;

public abstract class SharedBodyScannerSystem : EntitySystem
{

    public override void Initialize()
    {
        base.Initialize();
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
