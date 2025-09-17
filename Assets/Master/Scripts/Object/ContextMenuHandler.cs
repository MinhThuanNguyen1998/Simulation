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
            m_ContextMenuPanel.transform.position = mousePos;
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
