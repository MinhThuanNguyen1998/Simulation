using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FolderController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private SC02_3_Tutorial_DeleteFolder m_SC02_3;
    [SerializeField] private Image m_HightLightImage;
    [SerializeField] private GameObject m_ContextMenu;

    private void Start()
    {
        ActiveHighLightImage(false);
        ActiveContextMenu(false);
    }
    private void OnEnable()
    {
        ActiveHighLightImage(false);
        ActiveContextMenu(false);
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && m_ContextMenu.activeSelf)
        {
            GameObject clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (clickedObj == null || !clickedObj.transform.IsChildOf(m_ContextMenu.transform))
            {
                ActiveContextMenu(false);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveHighLightImage(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActiveHighLightImage(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            m_ContextMenu.SetActive(true);
        }
    }
    private void ActiveHighLightImage(bool isActive)
    {
        if(m_HightLightImage != null) m_HightLightImage.enabled = isActive;
    }
    private void ActiveContextMenu(bool isActive)
    {
        if(m_ContextMenu!=null) m_ContextMenu.SetActive(isActive);
    }
    
    public void OnButtonDeleteFolder()
    {
        m_SC02_3.DeleteFolder();
    }
    public void OnButtonRenameFolder()
    {
        m_SC02_3.RenameFolder();
    }
}
