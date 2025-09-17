using System.Collections;
using UnityEngine;

public class SC02_1_Tutorial_CreateFolder : MonoBehaviour
{
    [Header("SC02")] 
    [SerializeField] private SC02 m_SC02;

    [Header("UI Prefabs")]
    [SerializeField] private GameObject m_FolderPrefab;
    [SerializeField] private Transform m_FolderParent;

    private GameObject m_Folder;
    private bool m_IsPressRefresh = false;
    private void OnEnable()
    {
        ResetState();
    }
    public void OnButtonIntroduction()
    {
        PopupManager.Instance.ShowPopup(PopupType.Tutorial, Config.Text_CreateFolder, Config.Text_OK);
    }
    public void OnButtonComplete()
    {
        Debug.Log("OnButtonComplete");
        if (m_IsPressRefresh)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification,Config.Text_Thu_Lai, Config.Text_OK);
        }
        else if (m_Folder != null)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Hoan_Thanh, Config.Text_OK);
        }
        else
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Can_Tao_Thu_Muc, Config.Text_OK);
        }
    }
    public void OnButtonCreateNewFolder()
    {
        if (m_Folder != null) return;
        //Debug.Log("OnButtonCreateFolder");
        m_Folder = Instantiate(m_FolderPrefab, m_FolderParent);
        //Debug.Log("Folder created");
        m_IsPressRefresh = false;
    }
    public void OnButtonRefresh()
    {
        //Debug.Log("OnButtonRefresh");
        if (m_Folder == null)
            m_IsPressRefresh = true;
        StartCoroutine(RefreshRoutine());
    }
    private IEnumerator RefreshRoutine()
    {
        //m_IsRefreshing = true;
        m_FolderParent.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        m_FolderParent.gameObject.SetActive(true);
    }
    public void OnBackSC02()
    {
        m_SC02.BackSC02();
    }
    private void ResetState()
    {
        if (m_Folder != null)
        {
            Destroy(m_Folder);
            m_Folder = null;
        }
        m_IsPressRefresh = false;
    }
}
