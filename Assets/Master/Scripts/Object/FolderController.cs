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
        SetActiveUI(m_HightLightImage, false);
        SetActiveUI(m_ContextMenu, false);
        SetActiveUI(m_RenameInputField, false);
        m_OriginalName = m_FolderNameText.text;
    }
    private void OnEnable()
    {
        SetActiveUI(m_HightLightImage, false);
        SetActiveUI(m_ContextMenu, false);
        SetActiveUI(m_RenameInputField, false);
        m_FolderNameText.text = m_OriginalName;
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && m_ContextMenu.activeSelf)
        {
            GameObject clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (clickedObj == null || !clickedObj.transform.IsChildOf(m_ContextMenu.transform))
            {
                SetActiveUI(m_ContextMenu, false);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetActiveUI(m_HightLightImage, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetActiveUI(m_HightLightImage, false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            m_ContextMenu.SetActive(true);
        }
    }


    private void SetActiveUI(UnityEngine.Object target, bool isActive)
    {
        if(target == null) return;
        switch (target)
        {
            case UnityEngine.UI.Image image:
                image.enabled = isActive;
                break;

            case GameObject go:
                go.SetActive(isActive);
                break;

            case TMP_InputField tmpInputField:
                tmpInputField.gameObject.SetActive(isActive);
                break;
        }
    }
    
    public void OnButtonDeleteFolder()
    {
        m_SC02_3.DeleteFolder();
    }
    public void OnButtonRenameFolder()
    {
        m_SC02_3.RenameFolder();
        SetActiveUI(m_ContextMenu, false);
        SetActiveUI(m_RenameInputField, true);
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
            if (newName.Length > 8)
                newName = newName.Substring(0, 8) + "...";
            m_FolderNameText.text = newName;
        }
        SetActiveUI(m_RenameInputField, false);
    }
}
