using Reloaded.Hooks.Definitions;
using System.Globalization;
using System.Runtime.InteropServices;
using static p3rpc.socialStatTracker.Native.UnrealString;

namespace p3rpc.socialStatTracker.Native;
internal unsafe class UI
{
    internal static RenderTextDelegate RenderText;

    internal static void Initialise(IReloadedHooks hooks)
    {
        Utils.SigScan("4C 8B DC 53 55 48 81 EC A8 01 00 00", "RenderText", address =>
        {
            RenderText = hooks.CreateWrapper<RenderTextDelegate>(address, out _);
        });
    }

    [StructLayout(LayoutKind.Explicit, Size = 0xa4)]
    internal struct TextInfo
    {
        internal TextInfo(float x, float y, Colour colour, float scale, float angle, FString text)
        {
            X = x;
            Y = y;
            Colour = colour;
            Scale = scale;
            Angle = angle;
            String = text;
        }

        [FieldOffset(0)]
        internal float X;

        [FieldOffset(4)]
        internal float Y;

        [FieldOffset(0xC)]
        internal Colour Colour;

        [FieldOffset(0x10)]
        internal float Scale;

        [FieldOffset(0x38)]
        float float_0x38 = 1;

        [FieldOffset(0x40)]
        internal float Angle;

        [FieldOffset(0x60)]
        float float_0x60 = 1;

        [FieldOffset(0x74)]
        float float_0x74 = 1;

        [FieldOffset(0x88)]
        float float_0x88 = 1;

        [FieldOffset(0x9c)]
        float float_0x9c = 1;

        [FieldOffset(0x18)]
        internal FString String;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Colour
    {
        internal byte A;
        internal byte R;
        internal byte G;
        internal byte B;
    }

    internal delegate void RenderTextDelegate(TextInfo* text, nuint param_2, float param_3, float param_4, float param_5,
               float param_6, char param_7, int param_8);
}
