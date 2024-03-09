using System.Runtime.InteropServices;
using static p3rpc.socialStatTracker.Native.UnrealArray;

namespace p3rpc.socialStatTracker.Native;
internal class xrd777
{
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

}
