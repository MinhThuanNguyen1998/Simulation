using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextContent;
    [SerializeField] private TextMeshProUGUI m_TextButton;
    [SerializeField] private Button m_ButtonAction;

    public void SetContent(string content, string buttonText)
    {
        Debug.Log("SetContent");
        m_TextContent.text = content;
        m_TextButton.text = buttonText;
    }

    public void OnButtonAction()
    {
        Destroy(gameObject);
    }
}
