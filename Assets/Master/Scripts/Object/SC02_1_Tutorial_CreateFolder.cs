using System.Collections;
using UnityEngine;

public class SC02_1_Tutorial_CreateFolder : TutorialBase
{
    [Header("UI Prefabs")]
    [SerializeField] private GameObject m_FolderPrefab;
    [SerializeField] private Transform m_FolderParent;

    private GameObject m_Folder;
    private bool m_IsPressedRefresh = false;
    
    public override void OnButtonIntroduction()
    {
        PopupManager.Instance.ShowPopup(PopupType.Tutorial, Config.Text_CreateFolder, Config.Text_OK);
    }
    public override void OnButtonComplete()
    {
        Debug.Log("OnButtonComplete");
        if (m_IsPressedRefresh)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification,Config.Text_Thu_Lai, Config.Text_OK);
        }
        else if (m_Folder != null)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Hoan_Thanh, Config.Text_OK);
        }
        else
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Chua_Hoan_Thanh, Config.Text_OK);
        }
    }
    public void OnButtonCreateNewFolder()
    {
        if (m_Folder != null) return;
        //Debug.Log("OnButtonCreateFolder");
        m_Folder = Instantiate(m_FolderPrefab, m_FolderParent);
        //Debug.Log("Folder created");
        m_IsPressedRefresh = false;
    }
    public void OnButtonRefresh()
    {
        //Debug.Log("OnButtonRefresh");
        if (m_Folder == null)
            m_IsPressedRefresh = true;
        StartCoroutine(RefreshRoutine());
    }
    private IEnumerator RefreshRoutine()
    {
        m_FolderParent.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        m_FolderParent.gameObject.SetActive(true);
    }
    protected override void ResetState()
    {
        if (m_Folder != null)
        {
            Destroy(m_Folder);
            m_Folder = null;
        }
        m_IsPressedRefresh = false;
    }
}
