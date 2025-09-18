using UnityEngine;

public class ContextMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_ContextMenuPanel;
    private void Start()
    {
        ActiveContextMenu(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Input.mousePosition;
            RectTransform panelRect = m_ContextMenuPanel.GetComponent<RectTransform>();
            float panelWidth = panelRect.rect.width;
            float panelHeight = panelRect.rect.height;
            float clampedX = Mathf.Clamp(mousePos.x, panelWidth / 2, Screen.width - panelWidth / 2);
            float clampedY = Mathf.Clamp(mousePos.y, panelHeight / 2, Screen.height - panelHeight / 2);

            m_ContextMenuPanel.transform.position = new Vector2(clampedX, clampedY);
            m_ContextMenuPanel.transform.SetAsLastSibling();
            ActiveContextMenu(true);
        }
        if (Input.GetMouseButtonDown(0) && m_ContextMenuPanel.activeSelf)
        {
            GameObject clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (clickedObj == null || !clickedObj.transform.IsChildOf(m_ContextMenuPanel.transform))
            {
                ActiveContextMenu(false);
            }
        }
    }
    private void ActiveContextMenu(bool isActiveContextMenu)
    {
        m_ContextMenuPanel.SetActive(isActiveContextMenu);
    }
}
