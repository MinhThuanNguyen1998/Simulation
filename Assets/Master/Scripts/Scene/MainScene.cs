using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : Singleton<MainScene>
{
    [System.Serializable]
    public class ViewPrefab
    {
        public ViewId id;
        public GameObject prefab;
    }
    [SerializeField] private List<ViewPrefab> m_Views;
    [SerializeField] private Transform m_ParentCanvas;

    private GameObject m_CurrentView;
    private void Start()
    {
        LoadView(ViewId.Home);
    }
    public void LoadView(ViewId viewId)
    {
        Debug.Log($"Loading view: {viewId}");
        // Destroy current view if exists
        if (m_CurrentView != null)
        {
            Destroy(m_CurrentView);
            m_CurrentView = null;
        }
        ViewPrefab viewPrefab = m_Views.Find(v => v.id == viewId);
        if (viewPrefab == null)
        {
            Debug.LogError($"View with ID {viewId} not found!");
            return;
        }
        m_CurrentView = Instantiate(viewPrefab.prefab, m_ParentCanvas);
    }

    public void QuitApplication()
    {
        Debug.Log("QuitApplication");
        Application.Quit();
    }

}
