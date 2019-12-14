using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerListLogicHandler
{
    private ServerListUIHandler m_cUIHandler;

    public ServerListLogicHandler(ServerListUIHandler cUIHandler)
    {
        m_cUIHandler = cUIHandler;
        m_cUIHandler.OnConnect += ConnectToServer;
    }

    private void ConnectToServer(string sIP)
    {
        UIManager.Instance.cServerLobbyUIHandler.ShowPanel();
        m_cUIHandler.HidePanel();
        NetworkManager.Instance.Client.ConnectToServer(sIP);
    }
}
