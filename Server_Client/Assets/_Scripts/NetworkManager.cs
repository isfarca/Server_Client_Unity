using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance
    {
        get
        {
            if(m_cInstance == null)
            {
                m_cInstance = FindObjectOfType<NetworkManager>();
            }
            return m_cInstance;
        }
    }

    private static NetworkManager m_cInstance;

    public ServerManager Server;
    public ClientManager Client;

    public List<IPAddress> AvailableServer;

    public byte[] getBytes(NetworkContainer str)
    {
        int size = Marshal.SizeOf(str);
        byte[] arr = new byte[size];

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(str, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }

    public NetworkContainer fromBytes(byte[] arr)
    {
        NetworkContainer str = new NetworkContainer();

        int size = Marshal.SizeOf(str);
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.Copy(arr, 0, ptr, size);

        str = (NetworkContainer)Marshal.PtrToStructure(ptr, str.GetType());
        Marshal.FreeHGlobal(ptr);

        return str;
    }
}
