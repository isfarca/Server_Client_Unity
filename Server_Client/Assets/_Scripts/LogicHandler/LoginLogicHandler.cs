using UnityEngine;

public class LoginLogicHandler
{
    public void Login(string sUsername, string sPortnumber, bool bServer)
    {
        if(string.IsNullOrEmpty(sUsername) || string.IsNullOrWhiteSpace(sUsername))
        {
            UIManager.Instance.cAlertUIHandler.ShowAlert(LanguageManager.sAlert_Username, UIManager.Instance.cLoginUIHandler);
            return;
        }
        if(string.IsNullOrEmpty(sPortnumber) || string.IsNullOrWhiteSpace(sPortnumber))
        {
            UIManager.Instance.cAlertUIHandler.ShowAlert(LanguageManager.sAlert_Port, UIManager.Instance.cLoginUIHandler);
            return;
        }

        int _nPort = -1;

        if(!int.TryParse(sPortnumber, out _nPort))
        {
            UIManager.Instance.cAlertUIHandler.ShowAlert(LanguageManager.sAlert_Port_NaN, UIManager.Instance.cLoginUIHandler);
            return;
        }

        if(bServer)
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
