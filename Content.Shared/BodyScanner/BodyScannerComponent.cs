using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
namespace Content.Shared.BodyScanner;

[RegisterComponent, NetworkedComponent]
public sealed partial class BodyScannerComponent : Component
{
    //How do I reference the type of item that is being looked for?
    [DataField("targetItem")]
    public string? TargetItem;

    [DataField("isAlerted")]
    public bool IsAlerted = false;
}

[Serializable, NetSerializable]
public enum BodyScannerVisuals : byte
{
    Base,
    Alerted
}
