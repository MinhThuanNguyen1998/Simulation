using System;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FolderController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private SC02_3_Tutorial_DeleteFolder m_SC02_3;
    [SerializeField] private GameObject m_ContextMenu;

    [SerializeField] private Image m_HightLightImage;
    [SerializeField] private TMP_InputField m_RenameInputField;
    [SerializeField] private TextMeshProUGUI m_FolderNameText;
    private string m_OriginalName = "New Folder";
    private void Start()
    {
        ActiveHighLightImage(false);
        ActiveContextMenu(false);
        ActiveInputField(false);
        m_OriginalName = m_FolderNameText.text;
    }
    private void OnEnable()
    {
        ActiveHighLightImage(false);
        ActiveContextMenu(false);
        ActiveInputField(false);
        m_FolderNameText.text = m_OriginalName;
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
    private void ActiveInputField(bool isActive)
    {
        if (m_RenameInputField != null && m_FolderNameText != null) m_RenameInputField.gameObject.SetActive(isActive);

    }

    private void ActiveContent(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public void OnButtonDeleteFolder()
    {
        m_SC02_3.DeleteFolder();
    }
    public void OnButtonRenameFolder()
    {
        m_SC02_3.RenameFolder();
        ActiveContextMenu(false);
        ActiveInputField(true);
        m_RenameInputField.text = m_FolderNameText.text;
        m_RenameInputField.Select(); // Select this input field in the EventSystem
        m_RenameInputField.ActivateInputField();// Focus the input field so the user can start typing immediately

        m_RenameInputField.onEndEdit.RemoveAllListeners();
        m_RenameInputField.onEndEdit.AddListener(OnRenameSubmit);
    }

    private void OnRenameSubmit(string newName)
    {
        if (!string.IsNullOrEmpty(newName))
        {
            if (newName.Length > 10)
                newName = newName.Substring(0, 10) + "...";
            m_FolderNameText.text = newName;
        }
        ActiveInputField(false);

    }
}
