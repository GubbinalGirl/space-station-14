using Robust.Shared.GameStates;

namespace Content.Shared.BodyScanner;

[RegisterComponent, NetworkedComponent]
public sealed partial class BodyScannerComponent : Component
{
    //How do I reference the type of item that is being looked for?
    [DataField("targetItem")]
    public string? TargetItem;
}

public enum BodyScannerVisuals : byte
{
    Base,
    Alerted
}
