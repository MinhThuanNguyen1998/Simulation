using System;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FolderController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private GameObject m_ContextMenu;
    [SerializeField] private Image m_HightLightImage;
    [SerializeField] private TMP_InputField m_RenameInputField;
    [SerializeField] private TextMeshProUGUI m_FolderNameText;

    [SerializeField] private MonoBehaviour m_ServiceObject;
    private IFolderService m_FolderService;
    private string m_OriginalName = "New Folder";

    private void Awake()
    {
        m_FolderService = m_ServiceObject as IFolderService;
    }
    private void Start()
    {
        ResetUI();
        m_OriginalName = m_FolderNameText.text;
    }
    private void OnEnable()
    {
        ResetUI();
        m_FolderNameText.text = m_OriginalName;
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && m_ContextMenu.activeSelf)
        {
            GameObject clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (clickedObj == null || !clickedObj.transform.IsChildOf(m_ContextMenu.transform)) SetActiveUI(m_ContextMenu, false);

        }
    }
    private void SetActiveUI(UnityEngine.Object target, bool isActive)
    {
        if (target == null) return;
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
    private void ResetUI()
    {
        SetActiveUI(m_HightLightImage, false);
        SetActiveUI(m_ContextMenu, false);
        SetActiveUI(m_RenameInputField, false);
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
        if (eventData.button == PointerEventData.InputButton.Right) SetActiveUI(m_ContextMenu, true);

    }
    public void OnButtonDeleteFolder()
    {
        m_FolderService?.DeleteFolder();
    }
    public void OnButtonRenameFolder()
    {
        m_FolderService?.RenameFolder();
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
