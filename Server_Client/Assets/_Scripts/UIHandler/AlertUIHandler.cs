using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AlertUIHandler : UIHandlerBase
{
    [SerializeField]
    private TextMeshProUGUI m_cLbAlert;
    [SerializeField]
    private Button m_cBtnOk;

    private UIHandlerBase m_cCallerBase;

    protected override void Awake()
    {
        base.Awake();
        InitializePanel();
        base.HidePanel();
    }

    public override void InitializePanel()
    {
        m_cLbAlert.text = "";
        m_cBtnOk.onClick.AddListener(() => OnClick_Ok());
    }

    public void ShowAlert(string sMessage, UIHandlerBase cCaller)
    {
        m_cLbAlert.text = sMessage;
        base.ShowPanel();
        cCaller.HidePanel();
        m_cCallerBase = cCaller;
    }

    private void OnClick_Ok()
    {
        m_cCallerBase.ShowPanel();
        base.HidePanel();
        m_cLbAlert.text = "";
        m_cCallerBase = null;
    }
}
