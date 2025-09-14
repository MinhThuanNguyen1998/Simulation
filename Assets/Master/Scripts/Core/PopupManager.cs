using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private Popup m_PopupPrefab;

    public void ShowPopup(string content, string buttonText)
    {
       Debug.Log("ShowPopup:" + Equals(content, buttonText));
       Popup popup = Instantiate(m_PopupPrefab);
       popup.SetContent(content, buttonText);

    }
}
