using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIHandler : UIHandlerBase
{
    [SerializeField]
    private TMP_InputField m_cTbxUsername;
    [SerializeField]
    private TMP_InputField m_cTbxPortnumber;
    [SerializeField]
    private Toggle m_cTglServer;
    [SerializeField]
    private Button m_cBtnStart;
    
    protected override void Awake()
    {
        base.Awake();
        InitializePanel();
    }

    public override void InitializePanel()
    {
        m_cTbxUsername.text = "";
        m_cTbxPortnumber.text = "";
        m_cTglServer.isOn = false;

        m_cBtnStart.onClick.AddListener(() => OnClick_Start());
    }

    private void OnClick_Start()
    {
        LogicManager.Instance.LoginLogic.Login(m_cTbxUsername.text, m_cTbxPortnumber.text, m_cTglServer.isOn);
    }
}
