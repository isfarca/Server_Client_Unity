using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private IPAddress m_cIP;
    private int m_nPort;

    private string m_sUsername;

    private TcpListener m_cTCPListener;
    private TcpClient m_cTCPClient;

    protected Action OnClientConnected = null;

    private UdpClient m_cUDPClient;

    public void StartServer(string sUsername, int nPort)
    {
        m_cIP = IPAddress.Parse("127.0.0.1");
        m_sUsername = sUsername;
        m_nPort = nPort;

        m_cTCPListener = new TcpListener(m_cIP, nPort);
        m_cTCPListener.Start();

        m_cTCPListener.BeginAcceptTcpClient(ClientConnected, null);

        Debug.Log("Server successfully started!");
    }

    public void StopServer()
    {
        if (m_cTCPListener != null)
        {
            m_cTCPListener.Stop();
            m_cTCPListener = null;
        }
        if (m_cTCPClient != null)
        {
            m_cTCPClient.Close();
            m_cTCPClient = null;
        }

        Debug.Log("Server successfully stopped!");
    }

    private void ClientConnected(IAsyncResult cResult)
    {
        m_cTCPClient = m_cTCPListener.EndAcceptTcpClient(cResult);
        OnClientConnected?.Invoke();
        Debug.Log("Client connected");
    }
}