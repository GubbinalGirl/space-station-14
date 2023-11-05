namespace Content.Shared.BodyScanner;

public abstract class SharedBodyScannerSystem : EntitySystem
{

    public override void Initialize()
    {
        base.Initialize();
    }
}

public sealed class ContrabandDetectedEvent : EntityEventArgs
{
    /// <summary>
    /// The BodyScanner or other detector that detected the contraband.
    /// </summary>
    public readonly EntityUid Detector;

    /// <summary>
    /// The person that holds the detected object.
    /// </summary>
    public readonly EntityUid DetectedPerson;

    /// <summary>
    /// The item that was detected as contraband.
    /// </summary>
    public readonly EntityUid DetectedItem;

    public ContrabandDetectedEvent(EntityUid detector, EntityUid detectedPerson, EntityUid detectedItem)
    {
        Detector = detector;
        DetectedPerson = detectedPerson;
        DetectedItem = detectedItem;
    }
}
