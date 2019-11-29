using UnityEngine;

public class LoginLogicHandler : MonoBehaviour
{
    public void Login(string sUsername, string sPortNumber, bool bServer)
    {
        if (string.IsNullOrEmpty(sUsername) || string.IsNullOrWhiteSpace(sUsername))
        {
            UIManager.Instance.cAlertUIHandler.ShowAlert(LanguageManager.sAlert_Username, UIManager.Instance.cLoginUIHandler);

            return;
        }
        if (string.IsNullOrEmpty(sPortNumber) || string.IsNullOrWhiteSpace(sPortNumber))
        {
            UIManager.Instance.cAlertUIHandler.ShowAlert(LanguageManager.sAlert_Port, UIManager.Instance.cLoginUIHandler);

            return;
        }

        int _nPort = -1;

        if (!int.TryParse(sPortNumber, out _nPort))
        {
            UIManager.Instance.cAlertUIHandler.ShowAlert(LanguageManager.sAlert_Port_NaN, UIManager.Instance.cLoginUIHandler);
        }

        if (bServer)
        {
            NetworkManager.Instance.Server.StartServer(sUsername, _nPort);
            Application.quitting += QuitServer;
        }
        else
        {
            NetworkManager.Instance.Client.StartClient(sUsername, _nPort);
            Application.quitting += QuitClient;
        }
    }

    private void QuitServer()
    {
        NetworkManager.Instance.Server.StopServer();
    }

    private void QuitClient()
    {
        NetworkManager.Instance.Client.StopClient();
    }
}