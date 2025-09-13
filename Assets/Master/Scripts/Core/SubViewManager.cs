using System.Collections.Generic;
using UnityEngine;

public class SubViewManager <T>: MonoBehaviour where T : System.Enum
{
    [System.Serializable]
    public class ViewItem
    {
        public T id;
        public GameObject viewObject;
    }
    [SerializeField] private List<ViewItem> m_Views;
    private GameObject m_CurrentView;

    public virtual void ShowView (T viewId)
    {
        Debug.Log($"Showing view: {viewId}");
        // Deactivate current view if exists
        if (m_CurrentView != null)
        {
            m_CurrentView.SetActive(false);
            m_CurrentView = null;
        }
        ViewItem viewItem = m_Views.Find(v => v.id.Equals(viewId));
        if (viewItem == null)
        {
            Debug.LogError($"View with ID {viewId} not found!");
            return;
        }
        m_CurrentView = viewItem.viewObject;
        m_CurrentView.SetActive(true);
    }
}
