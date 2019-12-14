using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance
    {
        get
        {
            if(m_cInstance == null)
            {
                m_cInstance = FindObjectOfType<LogicManager>();
            }
            return m_cInstance;
        }
    }

    private static LogicManager m_cInstance;

    public LoginLogicHandler LoginLogic;
    public ServerListLogicHandler ServerListLogic;
    public ServerLobbyLogicHandler ServerLobbyLogic;

    private void Awake()
    {
        LoginLogic = new LoginLogicHandler();
        ServerListLogic = new ServerListLogicHandler(FindObjectOfType<ServerListUIHandler>());
        ServerLobbyLogic = new ServerLobbyLogicHandler(FindObjectOfType<ServerLobbyUIHandler>());
    }
}