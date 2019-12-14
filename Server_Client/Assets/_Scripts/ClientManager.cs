using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    private IPAddress m_cIP;
    private string m_sUsername;
    private int m_nPort;
    
    private UdpClient m_cUdpClient;

    private bool m_bSearching;
    private bool m_bCommunicating;

    private IPEndPoint m_cServer;

    public void StartClient(string sUsername, int nPort)
    {
        m_cIP = IPAddress.Any;
        m_sUsername = sUsername;
        m_nPort = nPort;

        StartCoroutine(SearchForServer());
        
        Debug.Log("Started Client");
    }

    private IEnumerator SearchForServer()
    {
        m_cUdpClient = new UdpClient();
        m_cUdpClient.Client.Bind(new IPEndPoint(m_cIP, m_nPort));
        m_bSearching = true;

        IPEndPoint cResponder = new IPEndPoint(0, 0);

        yield return new WaitForEndOfFrame();

        byte[] acData = Encoding.UTF8.GetBytes("SERVER_SEARCH");
        m_cUdpClient.Send(acData, acData.Length, new IPEndPoint(IPAddress.Broadcast, m_nPort));

        yield return new WaitForEndOfFrame();
        StartCoroutine(EndSearching());

        if (NetworkManager.Instance.AvailableServer == null)
        {
            NetworkManager.Instance.AvailableServer = new List<IPAddress>();
        }

        Task.Run(() => {
            while (m_bSearching)
            {
                var cResponseBuffer = m_cUdpClient.Receive(ref cResponder);

                if (cResponseBuffer != null)
                {
                    Debug.Log("Get something: " + cResponder.Address.ToString());

                    if(Encoding.UTF8.GetString(cResponseBuffer) == "SERVER_RESPONSE")
                    {
                        Debug.Log("Server responsed: " + cResponder.Address.ToString());
                        if(!NetworkManager.Instance.AvailableServer.Contains(cResponder.Address))
                        {
                            NetworkManager.Instance.AvailableServer.Add(cResponder.Address);
                        }
                    }
                }
            }
        });
    }

    private IEnumerator EndSearching()
    {
        yield return new WaitForSeconds(10.0f);
        m_bSearching = false;
        Debug.Log("End searching");

        foreach(IPAddress cIP in NetworkManager.Instance.AvailableServer)
        {
            UIManager.Instance.cServerListUIHandler.AddServer(cIP.ToString());
        }
        UIManager.Instance.cServerListUIHandler.ShowPanel();
    }

    public void StopClient()
    {
        m_bSearching = false;
        m_bCommunicating = false;
        if (m_cUdpClient != null)
        {
            m_cUdpClient.Client.Shutdown(SocketShutdown.Both);
            m_cUdpClient.Close();
            m_cUdpClient = null;
        }
        Debug.Log("Stopped Client");
    }

    public void ConnectToServer(string sIP)
    {
        m_cServer = new IPEndPoint(IPAddress.Parse(sIP), m_nPort);
        /*
        StopClient();
        
        m_cUdpClient = new UdpClient();
        m_cUdpClient.Client.Bind(m_cServer);
        */
        m_bCommunicating = true;

        NetworkContainer cMessage = new NetworkContainer();
        cMessage.Command = "JOIN_SERVER";
        cMessage.Message = m_sUsername;

        byte[] acData = NetworkManager.Instance.getBytes(cMessage);
        m_cUdpClient.Send(acData, acData.Length, m_cServer);

        StartCoroutine(Communicating());
    }

    public void SendChatMessage(string sMessage)
    {
        NetworkContainer cMessage = new NetworkContainer();
        cMessage.Command = "SEND_MESSAGE";
        cMessage.Message = m_sUsername + ": " + sMessage;

        byte[] acData = NetworkManager.Instance.getBytes(cMessage);
        m_cUdpClient.Send(acData, acData.Length, m_cServer);
    }

    private IEnumerator Communicating()
    {
        Task.Run(() => {
            while (m_bCommunicating)
            {
                byte[] cResponseBuffer = m_cUdpClient.Receive(ref m_cServer);

                if (cResponseBuffer != null)
                {
                    NetworkContainer cMessage = NetworkManager.Instance.fromBytes(cResponseBuffer);

                    if (cMessage.Command == "ADD_CHAT_LOG")
                    {
                        LogicManager.Instance.ServerLobbyLogic.AddMessage(cMessage.Message);
                    }
                }
            }
        });
        yield return new WaitForEndOfFrame();
    }
}