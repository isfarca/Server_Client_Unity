using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIHandlerBase : MonoBehaviour
{
    private CanvasGroup m_cCanvasGroup;

    protected virtual void Awake()
    {
        m_cCanvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public abstract void InitializePanel();

    public void ShowPanel()
    {
        m_cCanvasGroup.interactable = true;
        m_cCanvasGroup.blocksRaycasts = true;
        m_cCanvasGroup.alpha = 1.0f;
    }

    public void HidePanel()
    {
        m_cCanvasGroup.interactable = false;
        m_cCanvasGroup.blocksRaycasts = false;
        m_cCanvasGroup.alpha = 0.0f;
    }
}
