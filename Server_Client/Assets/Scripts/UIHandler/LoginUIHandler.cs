using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginUIHandler : UIHandlerBase
{
    [SerializeField] private TMP_InputField m_cTbxUsername;
    [SerializeField] private TMP_InputField m_cTbxPortNumber;
    [SerializeField] private Toggle m_cTglServer;
    [SerializeField] private Button m_cBtnStart;

    protected override void Awake()
    {
        base.Awake();
        InitializePanel();
    }

    public override void InitializePanel()
    {
        m_cTbxUsername.text = "";
        m_cTbxPortNumber.text = "";
        m_cTglServer.isOn = false;

        m_cBtnStart.onClick.AddListener(() => OnClick_Start());
    }

    private void OnClick_Start()
    {
        UIManager.Instance.cLoginLogicHandler.Login(m_cTbxUsername.text, m_cTbxPortNumber.text, m_cTglServer.isOn);
    }
}