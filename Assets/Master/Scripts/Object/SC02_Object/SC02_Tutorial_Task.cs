using UnityEngine;

public enum TaskType
{
    Delete,
    Rename
}

public class SC02_Tutorial_Task : TutorialBase,IFolderService
{
    [Header("Tutorial Settings")]
    [SerializeField] private TaskType m_TaskType = TaskType.Delete;

    [SerializeField] private GameObject m_Folder;
    private bool m_IsPressedRename = false;

    public override void OnButtonIntroduction()
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
    public override void OnButtonComplete()
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
    protected override void ResetState()
    {
        m_IsPressedRename = false;
        if (m_Folder != null) m_Folder.SetActive(true);
    }  
}
