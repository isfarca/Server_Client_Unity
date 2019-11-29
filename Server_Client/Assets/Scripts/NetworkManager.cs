using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance
    {
        get
        {
            if (m_cInstance == null)
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
}