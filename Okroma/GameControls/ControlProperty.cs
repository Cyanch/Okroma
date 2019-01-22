using System;

namespace Okroma.GameControls
{
    [Flags]
    public enum ControlProperty
    {
        None = 0,

        JustPressed = 1 << 0,
        Held = 1 << 1,

        Pressed = JustPressed | Held
    }
}
