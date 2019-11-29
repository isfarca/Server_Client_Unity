using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get
        {
            if (m_cInstance == null)
            {
                m_cInstance = FindObjectOfType<UIManager>();
            }

            return m_cInstance;
        }
    }
    private static UIManager m_cInstance;

    [Header("UI Handler")]
    public LoginUIHandler cLoginUIHandler;
    public AlertUIHandler cAlertUIHandler;
    public ServerListUIHandler cServerListUIHandler;

    public LoginLogicHandler cLoginLogicHandler
    {
        get
        {
            if (m_cLoginLogicHandler == null)
            {
                m_cLoginLogicHandler = new LoginLogicHandler();
            }

            return m_cLoginLogicHandler;
        }
    }

    private LoginLogicHandler m_cLoginLogicHandler;
}