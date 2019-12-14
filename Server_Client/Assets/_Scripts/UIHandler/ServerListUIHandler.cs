using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerListUIHandler : UIHandlerBase
{
    [SerializeField]
    private GameObject m_cObjServerPrefab;
    [SerializeField]
    private Transform m_cObjGrp;

    public event System.Action<string> OnConnect;

    protected override void Awake()
    {
        base.Awake();
        InitializePanel();
        base.HidePanel();
    }

    public override void InitializePanel()
    {
        int n = 0;
        foreach (Transform cChild in m_cObjGrp.transform)
        {
            if (n > 0)
            {
                Destroy(cChild);
            }
            n++;
        }
    }

    public void AddServer(string sIP)
    {
        GameObject cObjServer = Instantiate<GameObject>(m_cObjServerPrefab, m_cObjGrp);
        cObjServer.GetComponentInChildren<TextMeshProUGUI>().text = sIP;
        cObjServer.GetComponentInChildren<Button>().onClick.AddListener(delegate { OnConnect(sIP); });
    }
}