using TMPro;
using UnityEngine;

public class ServerListUIHandler : UIHandlerBase
{
    [SerializeField] private GameObject m_cobjServerPrefab;
    [SerializeField] private Transform m_cObjGrp;

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
        var cObjServer = Instantiate(m_cobjServerPrefab, m_cObjGrp);
        cObjServer.GetComponent<TextMeshProUGUI>().text = sIP;
    }
}