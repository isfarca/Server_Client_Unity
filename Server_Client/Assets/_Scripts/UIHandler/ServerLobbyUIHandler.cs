using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ServerLobbyUIHandler : UIHandlerBase
{
    [SerializeField]
    private TextMeshProUGUI m_cLbChat;
    [SerializeField]
    private TMP_InputField m_cTbxMessage;

    public event System.Action<string> OnSendMessage;

    protected override void Awake()
    {
        base.Awake();
        InitializePanel();
        base.HidePanel();
    }

    public override void InitializePanel()
    {
        m_cLbChat.text = "";
        m_cTbxMessage.text = "";
    }

    public void AddMessage(string sMessage)
    {
        m_cLbChat.text += "\n" + sMessage;
    }

    public void OnClick_SendMessage()
    {
        OnSendMessage?.Invoke(m_cTbxMessage.text);
        m_cTbxMessage.text = "";
    }
}
