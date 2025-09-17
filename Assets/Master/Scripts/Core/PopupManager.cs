using UnityEngine;

public enum PopupType
{
    Notification,
    Tutorial,
}
public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private Popup m_NotificationPopupPrefab;
    [SerializeField] private Popup m_TutorialPopupPrefab;

    public void ShowPopup(PopupType type, string content, string buttonText)
    {
        Debug.Log("ShowPopup:" + Equals(content, buttonText));
        Popup prefab = null;

        switch (type)
        {
            case PopupType.Notification:
                prefab = m_NotificationPopupPrefab;
                break;
            case PopupType.Tutorial:
                prefab= m_TutorialPopupPrefab;
                break;
        }

        if (prefab != null)
        {
            Popup popup = Instantiate(prefab);
            popup.SetContent(content, buttonText);
        }
        else 
        {
            Debug.LogError("Prefab not found");
        }
    }
}
