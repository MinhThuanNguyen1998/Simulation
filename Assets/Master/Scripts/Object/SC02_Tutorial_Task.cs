using UnityEngine;
using UnityEngine.WSA;

public enum TaskType
{
    Delete,
    Rename
}

public class SC02_Tutorial_Task : MonoBehaviour
{
    [Header("SC02")]
    [SerializeField] private SC02 m_SC02;

    [Header("UI Prefabs")]
    [SerializeField] private GameObject m_Folder;

    [Header("Tutorial Settings")]
    [SerializeField] private TaskType m_TaskType = TaskType.Delete;

    private bool m_IsPressedRename = false;
    private void OnEnable()
    {
        ResetState();
    }
    public void OnButtonIntroduction()
    {
        string text = (m_TaskType == TaskType.Delete)? Config.Text_DeleteFolder : Config.Text_RenameFolder;
        PopupManager.Instance.ShowPopup(PopupType.Tutorial, text, Config.Text_OK);
    }
    public void DeleteFolder()
    {
        if (!m_Folder.activeSelf) return;
        m_Folder.SetActive(false);
        m_IsPressedRename = false;
    }
    public void RenameFolder()
    {
        m_IsPressedRename = true;
    }
    public void OnButtonComplete()
    {
        Debug.Log("OnButtonComplete");
        if (m_TaskType == TaskType.Delete)
        {
            if (m_IsPressedRename)
            {
                PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Thu_Lai, Config.Text_OK);
            }
            else if (!m_Folder.activeSelf)
            {
                PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Hoan_Thanh, Config.Text_OK);
            }
            else
            {
                PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Chua_Hoan_Thanh, Config.Text_OK);
            }
        }
        else if (m_TaskType == TaskType.Rename)
        {
            if (!m_Folder.activeSelf)
            {
                PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Thu_Lai, Config.Text_OK);
                ResetState(); // Try it again
            }
            else if (m_IsPressedRename)
                PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Hoan_Thanh, Config.Text_OK);
            else
                PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Chua_Hoan_Thanh, Config.Text_OK);
        }
    }
    private void ResetState()
    {
        m_IsPressedRename = false;
        if (m_Folder != null) m_Folder.SetActive(true);

    }
    public void OnBackSC02()
    {
        m_SC02.BackSC02();
    }
}
