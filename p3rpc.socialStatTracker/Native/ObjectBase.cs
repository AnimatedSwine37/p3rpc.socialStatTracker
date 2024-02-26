using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UE4SSDotNetFramework.Framework;

namespace p3rpc.socialStatTracker.Native;
public class ObjectBase<TObjType> : ObjectReference where TObjType : unmanaged
{
    public TObjType Instance => Marshal.PtrToStructure<TObjType>(Pointer);

    public ObjectBase(IntPtr pointer)
    {
        Pointer = pointer;
    }
}
