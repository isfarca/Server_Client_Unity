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

    private TcpClient m_cTCPClient;
    private UdpClient m_cUDPClient;

    private bool m_bSearching;

    public void StartClient(string sUsername, int nPort)
    {
        m_cIP = IPAddress.Any;
        m_sUsername = sUsername;
        m_nPort = nPort;

        Debug.Log("Started Client");

        //try
        //{
        //    m_cTCPClient = new TcpClient();
        //    m_cTCPClient.Connect(m_cIP, m_nPort);
        //}
        //catch
        //{
        //    StopClient();
        //}
    }

    private IEnumerator SearchForServer()
    {
        m_cUDPClient = new UdpClient();
        m_cUDPClient.Client.Bind(new IPEndPoint(m_cIP, m_nPort));
        m_bSearching = true;

        IPEndPoint cResponder = new IPEndPoint(0, 0);

        yield return new WaitForEndOfFrame();

        byte[] acData = Encoding.UTF8.GetBytes("SERVER SEARCH");
        m_cUDPClient.Send(acData, acData.Length, new IPEndPoint(IPAddress.Broadcast, m_nPort));

        yield return new WaitForEndOfFrame();
        StartCoroutine(EndSearching());

        Task.Run(() =>
        {
            while (m_bSearching)
            {
                var cResponseBuffer = m_cUDPClient.Receive(ref cResponder);

                if (cResponseBuffer != null)
                {
                    Debug.Log("Get something!");

                    if (Encoding.UTF8.GetString(cResponseBuffer) == "SERVER_RESPONSE")
                    {
                        Debug.Log("Server responded " + cResponder.Address.ToString());

                        if (NetworkManager.Instance.AvailableServer == null)
                        {
                            NetworkManager.Instance.AvailableServer = new List<IPAddress>();
                        }
                        if (NetworkManager.Instance.AvailableServer.Contains(cResponder.Address))
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
        Debug.Log("End searching!");

        foreach (IPAddress cIP in NetworkManager.Instance.AvailableServer)
        {
            UIManager.Instance.cServerListUIHandler.AddServer(cIP.ToString());
        }
        UIManager.Instance.cServerListUIHandler.ShowPanel();
    }

    public void StopClient()
    {
        if (m_cTCPClient != null)
        {
            if (m_cTCPClient.Connected)
            {
                m_cTCPClient.Close();
            }
            m_cTCPClient = null;
        }
    }
}