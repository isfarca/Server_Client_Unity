using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLobbyLogicHandler
{
    private List<string> m_acMessages;

    private ServerLobbyUIHandler m_cUIHandler;

    public ServerLobbyLogicHandler(ServerLobbyUIHandler cUIHandler)
    {
        m_acMessages = new List<string>();
        m_cUIHandler = cUIHandler;
        m_cUIHandler.OnSendMessage += SendMessageToServer;
    }

    public void SendMessageToServer(string sMessage)
    {
        NetworkManager.Instance.Client.SendChatMessage(sMessage);
    }

    public void AddMessage(string sMessage)
    {
        m_acMessages.Add(sMessage);
        m_cUIHandler.AddMessage(sMessage);
    }
}
