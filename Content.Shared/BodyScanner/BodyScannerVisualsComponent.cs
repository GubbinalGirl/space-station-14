using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Client.BodyScanner;

[RegisterComponent]
public sealed partial class BodyScannerVisualsComponent : Component
{
    [DataField("isAlerted")]
    public bool IsAlerted = false;
}

public enum BodyScannerVisuals : byte
{
    Base,
    Alerted
}
