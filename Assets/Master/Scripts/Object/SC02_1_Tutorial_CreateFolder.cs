using UnityEngine;

public class SC02_1_Tutorial_CreateFolder : MonoBehaviour
{
    [Header("SC02")] 
    [SerializeField] private SC02 m_SC02;

    [Header("UI Prefabs")]
    [SerializeField] private GameObject m_FolderPrefab;
    [SerializeField] private Transform m_FolderParent;

    private GameObject m_Folder;
    private bool IsPressRefresh = false;
    public void OnButtonIntroduction()
    {
        PopupManager.Instance.ShowPopup(PopupType.Tutorial, Config.Text_CreateFolder, Config.Text_OK);
    }
    public void OnButtonComplete()
    {
        Debug.Log("OnButtonComplete");
        if (IsPressRefresh)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification,Config.Text_Thu_Lai, Config.Text_OK);
        }
        if (m_Folder != null)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Hoan_Thanh, Config.Text_OK);
        }
    }
    public void OnButtonCreateNewFolder()
    {
        Debug.Log("OnButtonCreateFolder");
        if (m_Folder == null)
        {
            m_Folder = Instantiate(m_FolderPrefab, m_FolderParent);
            Debug.Log("Folder created");
        }
        IsPressRefresh = false;
    }
    public void OnButtonRefresh()
    {
        Debug.Log("OnButtonRefresh");
        IsPressRefresh = true;
        m_Folder = null;
    }
    public void OnBackSC02()
    {
        m_SC02.BackSC02();
    }
}
