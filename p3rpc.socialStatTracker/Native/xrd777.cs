using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static p3rpc.socialStatTracker.Native.xrd777;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using static p3rpc.socialStatTracker.Native.UnrealString;
using System.Data;
using static p3rpc.socialStatTracker.Native.UnrealArray;

namespace p3rpc.socialStatTracker.Native;
internal class xrd777
{
    [StructLayout(LayoutKind.Explicit, Size = 0x2B8)]
    public unsafe struct AUIDrawBaseActor
    {
        // I don't actually need any of this and can't be bothered dealing with all of the extra stuff I'd need for them to work
        //[FieldOffset(0x0000)] public AAppActor baseObj;
        //[FieldOffset(0x0290)] public UAssetLoader* pAssetLoader;
        //[FieldOffset(0x0298)] public UUIDataAsset* ResourceDataAsset; 
    }

    public enum EUI_DRAW_POINT : byte
    {
        UI_DRAW_LEFT_TOP = 0,
        UI_DRAW_LEFT_CENTER = 1,
        UI_DRAW_LEFT_BOTTOM = 2,
        UI_DRAW_CENTER_TOP = 3,
        UI_DRAW_CENTER_CENTER = 4,
        UI_DRAW_CENTER_BOTTOM = 5,
        UI_DRAW_RIGHT_TOP = 6,
        UI_DRAW_RIGHT_CENTER = 7,
        UI_DRAW_RIGHT_BOTTOM = 8,
        UI_DRAW_MAX = 9,
    }

    public enum EUIFontStyle : byte
    {
        EUI_Defult_Value = 0,
        UI_FONT_STYLE_NORMAL_SSMALL = 3,
        UI_FONT_STYLE_NORMAL_SMALL = 0,
        UI_FONT_STYLE_NORMAL_MEDIUM = 1,
        UI_FONT_STYLE_NORMAL_LARGE = 2,
        EUIFontStyle_MAX = 4,
    };

    [StructLayout(LayoutKind.Explicit, Size = 0x10)]
    public unsafe struct FHeroParameterTable
    {
        [FieldOffset(0x0000)] public TArray<int> Points;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x40)]
    public unsafe struct UHeroParameterDataAsset
    {
        //[FieldOffset(0x0000)] public UAppMultiDataAsset baseObj;
        [FieldOffset(0x0030)] public TArray<FHeroParameterTable> Tables;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x40)]
    public unsafe struct UHeroParameterHandle
    {
        // Commenting out bits I don't need again
        //[FieldOffset(0x0000)] public UObject baseObj;
        [FieldOffset(0x0028)] public UHeroParameterPoint* points;
        [FieldOffset(0x0030)] public UHeroParameterDataAsset* pDataAsset;
        //[FieldOffset(0x0038)] public UDataTable* pParameterNameTable;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct UHeroParameterPoint
    {
        [FieldOffset(0)] int unk;
        [FieldOffset(4)] public int points;
    }

    public unsafe class HeroParameterHandle : ObjectBase<UHeroParameterHandle>
    {
        public HeroParameterHandle(IntPtr pointer) : base(pointer) { }
    }


    public unsafe class UIDrawBaseActor : ObjectBase<AUIDrawBaseActor>
    {
        public UIDrawBaseActor(IntPtr pointer) : base(pointer) { }
        public bool Sync()
        {
            Span<(string name, object value)> @params = [
            ];
            return ProcessEvent<bool>(GetFunction("Sync"), @params);
        }
        public void LoadStart()
        {
            Span<(string name, object value)> @params = [
            ];
            ProcessEvent(GetFunction("LoadStart"), @params);
        }
        //public UObject* GetResourceData(int Index)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("Index", Index)
        //    ];
        //    return (UObject*)ProcessEvent<IntPtr>(GetFunction("GetResourceData"), @params);
        //}
        //public void BPUIDebugCommand_DrawTriangle(float VX0, float VY0, float VX1, float VY1, float VX2, float VY2, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("VX0", VX0),
        //        ("VY0", VY0),
        //        ("VX1", VX1),
        //        ("VY1", VY1),
        //        ("VX2", VX2),
        //        ("VY2", VY2),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("Antialiasing", Antialiasing)
        //    ];
        //    ProcessEvent(GetFunction("BPUIDebugCommand_DrawTriangle"), @params);
        //}
        //public void BPUIDebugCommand_DrawTexture(float X, float Y, float Z, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float U0, float V0, float U1, float v1, UTexture* TextureHandle)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("U0", U0),
        //        ("V0", V0),
        //        ("U1", U1),
        //        ("v1", v1),
        //        ("TextureHandle", (IntPtr)TextureHandle)
        //    ];
        //    ProcessEvent(GetFunction("BPUIDebugCommand_DrawTexture"), @params);
        //}
        //public void BPUIDebugCommand_DrawSpr(float X, float Y, float Z, byte R, byte G, byte B, byte A, int SprNo, float ScaleX, float ScaleY, float Angle, USprAsset* SprHandle)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("SprNo", SprNo),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("SprHandle", (IntPtr)SprHandle)
        //    ];
        //    ProcessEvent(GetFunction("BPUIDebugCommand_DrawSpr"), @params);
        //}
        public void BPUIDebugCommand_DrawRectV4(float X, float Y, float Z, float VX0, float VY0, float VX1, float VY1, float VX2, float VY2, float VX3, float VY3, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing)
        {
            Span<(string name, object value)> @params = [
                ("X", X),
                ("Y", Y),
                ("Z", Z),
                ("VX0", VX0),
                ("VY0", VY0),
                ("VX1", VX1),
                ("VY1", VY1),
                ("VX2", VX2),
                ("VY2", VY2),
                ("VX3", VX3),
                ("VY3", VY3),
                ("R", R),
                ("G", G),
                ("B", B),
                ("A", A),
                ("ScaleX", ScaleX),
                ("ScaleY", ScaleY),
                ("Angle", Angle),
                ("Antialiasing", Antialiasing)
            ];
            ProcessEvent(GetFunction("BPUIDebugCommand_DrawRectV4"), @params);
        }
        //public void BPUIDebugCommand_DrawRect(float X, float Y, float Z, float Width, float Height, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("Width", Width),
        //        ("Height", Height),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("Antialiasing", Antialiasing)
        //    ];
        //    ProcessEvent(GetFunction("BPUIDebugCommand_DrawRect"), @params);
        //}
        //public void BPUIDebugCommand_DrawMaterial(float X, float Y, float Z, float Width, float Height, float Angle, UMaterialInstance* pMaterialInstance)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("Width", Width),
        //        ("Height", Height),
        //        ("Angle", Angle),
        //        ("pMaterialInstance", (IntPtr)pMaterialInstance)
        //    ];
        //    ProcessEvent(GetFunction("BPUIDebugCommand_DrawMaterial"), @params);
        //}
        public void BPUICommand_SetRenderTarget(int CanvasIndex)
        {
            Span<(string name, object value)> @params = [
                ("CanvasIndex", CanvasIndex)
            ];
            ProcessEvent(GetFunction("BPUICommand_SetRenderTarget"), @params);
        }
        //public void BPUICommand_SetPresetBlendState(EUIOTPRESET_BLEND_TYPE BlendType)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("BlendType", BlendType)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_SetPresetBlendState"), @params);
        //}
        //public void BPUICommand_SetBlendState(EUIBlendOperation OpColor, EUIBlendFactor SrcColor, EUIBlendFactor DstColor, EUIBlendOperation OpAlpha, EUIBlendFactor SrcAlpha, EUIBlendFactor DstAlpha)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("OpColor", OpColor),
        //        ("SrcColor", SrcColor),
        //        ("DstColor", DstColor),
        //        ("OpAlpha", OpAlpha),
        //        ("SrcAlpha", SrcAlpha),
        //        ("DstAlpha", DstAlpha)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_SetBlendState"), @params);
        //}
        //public void BPUICommand_ScalingItalicFontDraw(float X, float Y, FString String, FColor Color, float SizeX, float SizeY, float Scale, float Angle, bool ScalingOnlyX, EUI_DRAW_POINT DrawPoint, EUIFontStyle Style)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("String", String),
        //        ("Color", Color),
        //        ("SizeX", SizeX),
        //        ("SizeY", SizeY),
        //        ("Scale", Scale),
        //        ("Angle", Angle),
        //        ("ScalingOnlyX", ScalingOnlyX),
        //        ("DrawPoint", DrawPoint),
        //        ("Style", Style)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_ScalingItalicFontDraw"), @params);
        //}
        public void BPUICommand_ScalingFontDraw(float X, float Y, float Z, FString String, byte R, byte G, byte B, byte A, float SizeX, float SizeY, float Scale, float Angle, bool ScalingOnlyX, EUI_DRAW_POINT DrawPoint, EUIFontStyle Style, bool IsScaling)
        {
            Span<(string name, object value)> @params = [
                ("X", X),
                ("Y", Y),
                ("Z", Z),
                ("String", String),
                ("R", R),
                ("G", G),
                ("B", B),
                ("A", A),
                ("SizeX", SizeX),
                ("SizeY", SizeY),
                ("Scale", Scale),
                ("Angle", Angle),
                ("ScalingOnlyX", ScalingOnlyX),
                ("DrawPoint", DrawPoint),
                ("Style", Style),
                ("IsScaling", IsScaling)
            ];
            ProcessEvent(GetFunction("BPUICommand_ScalingFontDraw"), @params);
        }
        //public void BPUICommand_ItalicFontDraw(float X, float Y, FString String, FColor Color, float Scale, float Angle, EUI_DRAW_POINT DrawPoint, EUIFontStyle Style)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("String", String),
        //        ("Color", Color),
        //        ("Scale", Scale),
        //        ("Angle", Angle),
        //        ("DrawPoint", DrawPoint),
        //        ("Style", Style)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_ItalicFontDraw"), @params);
        //}
        //public UTextureRenderTarget2D* BPUICommand_GetRenderTarget(int CanvasIndex)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("CanvasIndex", CanvasIndex)
        //    ];
        //    return (UTextureRenderTarget2D*)ProcessEvent<IntPtr>(GetFunction("BPUICommand_GetRenderTarget"), @params);
        //}
        //public void BPUICommand_FontDrawFromFName(float X, float Y, float Z, ref FName String, byte R, byte G, byte B, byte A, float Scale, float Angle, EUI_DRAW_POINT DrawPoint, EUIFontStyle Style)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("String", String),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("Scale", Scale),
        //        ("Angle", Angle),
        //        ("DrawPoint", DrawPoint),
        //        ("Style", Style)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_FontDrawFromFName"), @params);
        //}
        //public void BPUICommand_FontDrawExFromFName(float X, float Y, float Z, ref FName String, byte R, byte G, byte B, byte A, float Scale, float Angle, float AnglePointX, float AnglePointY, EUIFontStyle Style)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("String", String),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("Scale", Scale),
        //        ("Angle", Angle),
        //        ("AnglePointX", AnglePointX),
        //        ("AnglePointY", AnglePointY),
        //        ("Style", Style)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_FontDrawExFromFName"), @params);
        //}
        public void BPUICommand_FontDrawEx(float X, float Y, float Z, FString String, byte R, byte G, byte B, byte A, float Scale, float Angle, float AnglePointX, float AnglePointY, EUIFontStyle Style)
        {
            Span<(string name, object value)> @params = [
                ("X", X),
                ("Y", Y),
                ("Z", Z),
                ("String", String),
                ("R", R),
                ("G", G),
                ("B", B),
                ("A", A),
                ("Scale", Scale),
                ("Angle", Angle),
                ("AnglePointX", AnglePointX),
                ("AnglePointY", AnglePointY),
                ("Style", Style)
            ];
            ProcessEvent(GetFunction("BPUICommand_FontDrawEx"), @params);
        }
        public void BPUICommand_FontDraw(float X, float Y, float Z, FString String, byte R, byte G, byte B, byte A, float Scale, float Angle, EUI_DRAW_POINT DrawPoint, EUIFontStyle Style)
        {
            Span<(string name, object value)> @params = [
                ("X", X),
                ("Y", Y),
                ("Z", Z),
                ("String", String),
                ("R", R),
                ("G", G),
                ("B", B),
                ("A", A),
                ("Scale", Scale),
                ("Angle", Angle),
                ("DrawPoint", DrawPoint),
                ("Style", Style)
            ];
            ProcessEvent(GetFunction("BPUICommand_FontDraw"), @params);
        }
        //public void BPUICommand_DrawVerticalGradationRect(float X, float Y, float Width, float Height, FColor TopColor, FColor BottomColor)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Width", Width),
        //        ("Height", Height),
        //        ("TopColor", TopColor),
        //        ("BottomColor", BottomColor)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawVerticalGradationRect"), @params);
        //}
        //public void BPUICommand_DrawTriangle(float X, float Y, float Z, float VX0, float VY0, float VX1, float VY1, float VX2, float VY2, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("VX0", VX0),
        //        ("VY0", VY0),
        //        ("VX1", VX1),
        //        ("VY1", VY1),
        //        ("VX2", VX2),
        //        ("VY2", VY2),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("Antialiasing", Antialiasing)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawTriangle"), @params);
        //}
        //public void BPUICommand_DrawTexture(float X, float Y, float Z, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float U0, float V0, float U1, float v1, UTexture* TextureHandle, EUI_DRAW_POINT DrawPoint)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("U0", U0),
        //        ("V0", V0),
        //        ("U1", U1),
        //        ("v1", v1),
        //        ("TextureHandle", (IntPtr)TextureHandle),
        //        ("DrawPoint", DrawPoint)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawTexture"), @params);
        //}
        //public void BPUICommand_DrawSpr(float X, float Y, float Z, byte R, byte G, byte B, byte A, int SprNo, float ScaleX, float ScaleY, float Angle, USprAsset* SprHandle, EUI_DRAW_POINT DrawPoint)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("SprNo", SprNo),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("SprHandle", (IntPtr)SprHandle),
        //        ("DrawPoint", DrawPoint)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawSpr"), @params);
        //}
        //public void BPUICommand_DrawSircle(float X, float Y, float Z, float Radius, byte R, byte G, byte B, byte A, float Antialiasing)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("Radius", Radius),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("Antialiasing", Antialiasing)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawSircle"), @params);
        //}
        //public void BPUICommand_DrawScrollbar(float X, float Y, float Z, float Width, float Height, float ScrollRange, int ScrollPos, int DrawListNum, int MaxListNum, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing, EUI_DRAW_POINT DrawPoint)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("Width", Width),
        //        ("Height", Height),
        //        ("ScrollRange", ScrollRange),
        //        ("ScrollPos", ScrollPos),
        //        ("DrawListNum", DrawListNum),
        //        ("MaxListNum", MaxListNum),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("Antialiasing", Antialiasing),
        //        ("DrawPoint", DrawPoint)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawScrollbar"), @params);
        //}
        //public void BPUICommand_DrawRoundRect(float X, float Y, float Z, float Width, float Height, int Round, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing, EUI_DRAW_POINT DrawPoint)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("Width", Width),
        //        ("Height", Height),
        //        ("Round", Round),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("Antialiasing", Antialiasing),
        //        ("DrawPoint", DrawPoint)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawRoundRect"), @params);
        //}
        //public void BPUICommand_DrawRectV4(float X, float Y, float Z, float VX0, float VY0, float VX1, float VY1, float VX2, float VY2, float VX3, float VY3, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing, EUI_DRAW_POINT DrawPoint)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("VX0", VX0),
        //        ("VY0", VY0),
        //        ("VX1", VX1),
        //        ("VY1", VY1),
        //        ("VX2", VX2),
        //        ("VY2", VY2),
        //        ("VX3", VX3),
        //        ("VY3", VY3),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("Antialiasing", Antialiasing),
        //        ("DrawPoint", DrawPoint)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawRectV4"), @params);
        //}
        //public void BPUICommand_DrawRect(float X, float Y, float Z, float Width, float Height, byte R, byte G, byte B, byte A, float ScaleX, float ScaleY, float Angle, float Antialiasing, EUI_DRAW_POINT DrawPoint)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("Width", Width),
        //        ("Height", Height),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("Antialiasing", Antialiasing),
        //        ("DrawPoint", DrawPoint)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawRect"), @params);
        //}
        //public void BPUICommand_DrawPlg(float X, float Y, float Z, byte R, byte G, byte B, byte A, int PlgID, float ScaleX, float ScaleY, float Angle, UPlgAsset* PlgHandle)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("R", R),
        //        ("G", G),
        //        ("B", B),
        //        ("A", A),
        //        ("PlgID", PlgID),
        //        ("ScaleX", ScaleX),
        //        ("ScaleY", ScaleY),
        //        ("Angle", Angle),
        //        ("PlgHandle", (IntPtr)PlgHandle)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawPlg"), @params);
        //}
        //public void BPUICommand_DrawMaterial(float X, float Y, float Z, float Width, float Height, float Angle, UObject* pMaterial)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("X", X),
        //        ("Y", Y),
        //        ("Z", Z),
        //        ("Width", Width),
        //        ("Height", Height),
        //        ("Angle", Angle),
        //        ("pMaterial", (IntPtr)pMaterial)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_DrawMaterial"), @params);
        //}
        //public USprAsset* BPUICommand_CastSprAsset(UObject* Asset)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("Asset", (IntPtr)Asset)
        //    ];
        //    return (USprAsset*)ProcessEvent<IntPtr>(GetFunction("BPUICommand_CastSprAsset"), @params);
        //}
        //public UPlgAsset* BPUICommand_CastPlgAsset(UObject* Asset)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("Asset", (IntPtr)Asset)
        //    ];
        //    return (UPlgAsset*)ProcessEvent<IntPtr>(GetFunction("BPUICommand_CastPlgAsset"), @params);
        //}
        //public void BPUICommand_AtlUIBlendState(EUIBLEND_STATE_TYPE BlendType)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("BlendType", BlendType)
        //    ];
        //    ProcessEvent(GetFunction("BPUICommand_AtlUIBlendState"), @params);
        //}
        //public void AddLoadAsset(TSoftObjectPtr<UObject> SoftAsset)
        //{
        //    Span<(string name, object value)> @params = [
        //        ("SoftAsset", SoftAsset)
        //    ];
        //    ProcessEvent(GetFunction("AddLoadAsset"), @params);
        //}
    }


}
