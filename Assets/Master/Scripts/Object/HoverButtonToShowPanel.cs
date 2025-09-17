using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButtonToShowPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject m_PanelFolder;

    private void Start()
    {
        ActivePanelFolder(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActivePanelFolder(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActivePanelFolder(false);
    }
    public void ActivePanelFolder(bool isActivePanelFolder)
    {
        if (m_PanelFolder == null) return;
        m_PanelFolder.SetActive(isActivePanelFolder);
    }
}
