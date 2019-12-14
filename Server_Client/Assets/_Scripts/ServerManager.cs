using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private IPAddress m_cIP;
    private string m_sUsername;
    private int m_nPort;
    
    private UdpClient m_cUdpClient;

    private List<IPAddress> m_acClients;
    
    public void StartServer(string sUsername, int nPort)
    {
        m_cIP = IPAddress.Parse("127.0.0.1");
        m_sUsername = sUsername;
        m_nPort = nPort;
        
        StartCoroutine(ListeningUDP());

        Debug.Log("Server successfully started!");
    }

    private IEnumerator ListeningUDP()
    {
        m_cUdpClient = new UdpClient();
        m_cUdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, m_nPort));

        IPEndPoint cEnd = new IPEndPoint(0, 0);
        m_acClients = new List<IPAddress>();

        Task.Run(() => {
            while (true)
            {
                var cBuffer = m_cUdpClient.Receive(ref cEnd);

                if (cBuffer != null)
                {
                    if (Encoding.UTF8.GetString(cBuffer) == "SERVER_SEARCH")
                    {
                        byte[] acResponse = Encoding.UTF8.GetBytes("SERVER_RESPONSE");
                        m_cUdpClient.Send(acResponse, acResponse.Length, cEnd.Address.ToString(), m_nPort);
                    }
                    else
                    {
                        Debug.Log("Trying to join " + cEnd.Address.ToString());
                        NetworkContainer cMessage = NetworkManager.Instance.fromBytes(cBuffer);

                        if (cMessage.Command == "JOIN_SERVER")
                        {
                            m_acClients.Add(cEnd.Address);

                            Debug.Log("Joined server successfully " + cEnd.Address.ToString());

                            NetworkContainer cResponse = new NetworkContainer();
                            cResponse.Command = "ADD_CHAT_LOG";
                            cResponse.Message = cMessage.Message + " ist dem Server beigetreten!";

                            foreach (IPAddress cIP in m_acClients)
                            {
                                m_cUdpClient.Send(NetworkManager.Instance.getBytes(cResponse), NetworkManager.Instance.getBytes(cResponse).Length, new IPEndPoint(cIP, m_nPort));
                            }
                        }
                    }
                }
            }
        });

        yield return new WaitForEndOfFrame();
    }
    
    public void StopServer()
    {
        if (m_cUdpClient != null)
        {
            m_cUdpClient.Close();
            m_cUdpClient = null;
        }
        Debug.Log("Server successfully stopped!");
    }
}