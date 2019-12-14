using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public struct NetworkContainer
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string Command;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string Message;
}
