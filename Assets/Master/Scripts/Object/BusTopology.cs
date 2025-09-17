using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BusTopology : NetworkTopology
{
    [Header("SC03")]
    [SerializeField] private SC03 m_SC03;
    [Header("Bus Settings")]
    [SerializeField] private RectTransform m_BusA;
    [SerializeField] private RectTransform m_BusB;
    [SerializeField] private RectTransform m_BusC;

    [Header("Network Nodes")]
    [SerializeField] private RectTransform m_NodeA;
    [SerializeField] private RectTransform m_NodeB;
    [SerializeField] private RectTransform m_NodeC;

    [Header("UI Dropdowns")]
    [SerializeField] private TMP_Dropdown m_DropdownStartNode;
    [SerializeField] private TMP_Dropdown m_DropdownEndNode;

    private RectTransform m_CurrentStartNode;
    private RectTransform m_CurrentEndNode;
    private RectTransform m_CurrentStartBus;
    private RectTransform m_CurrentEndBus;

    private Vector3 m_StartPoint;
    private Vector3 m_EndPoint;
    private bool m_IsMovingToBus = false;
    private bool m_IsMovingOnBus = false;
    private bool m_IsMovingFromBus = false;

    private void Start()
    {
        ResetTopology();
    }
    public override void SendSignal()
    {
        m_CurrentStartNode = GetNodeFromDropdown(m_DropdownStartNode);
        m_CurrentEndNode = GetNodeFromDropdown(m_DropdownEndNode);

        if (m_CurrentStartNode == m_CurrentEndNode)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification,Config.Text_Doi_Gia_Tri, Config.Text_OK);
            return;
        }

        m_CurrentStartBus = GetBusFromNode(m_CurrentStartNode);
        m_CurrentEndBus = GetBusFromNode(m_CurrentEndNode);

        SetMovingState(true, true, false);

        m_StartPoint = m_CurrentStartNode.position;
        m_EndPoint = m_CurrentStartBus.position;
        m_IsMovingToBus = true;
        m_IsMovingOnBus = false;
        m_IsMovingFromBus = false;
        m_Travelled = 0f;
    }
    protected override void UpdateMovement()
    {
        if (m_IsMoving)
        {
            m_Travelled += m_Speed * Time.deltaTime;
            float ratio = m_Travelled / Vector3.Distance(m_StartPoint, m_EndPoint);
            m_LetterController.position = Vector3.Lerp(m_StartPoint, m_EndPoint, ratio);
            if (ratio >= 1f)
            {
                if (m_IsMovingToBus)
                {
                    m_StartPoint = m_CurrentStartBus.position;
                    m_EndPoint = m_CurrentEndBus.position;
                    m_IsMovingToBus = false;
                    m_IsMovingOnBus = true;
                    m_Travelled = 0f;
                }
                else if(m_IsMovingOnBus)
                {
                    m_StartPoint = m_CurrentEndBus.position;
                    m_EndPoint = m_CurrentEndNode.position;
                    m_IsMovingOnBus = false;
                    m_IsMovingFromBus = true;
                    m_Travelled = 0f;
                }
                else if(m_IsMovingFromBus)
                {
                    SetMovingState(false, false, true);
                }
            }
        }
    }
    public override void ResetTopology()
    {
        SetMovingState(false, false, true);
        m_LetterController.gameObject.SetActive(true);
        m_LetterController.position = GetNodeFromDropdown(m_DropdownStartNode).position;
        m_LetterController.gameObject.SetActive(false);
    }
    private RectTransform GetNodeFromDropdown(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0: return m_NodeA;
            case 1: return m_NodeB;
            case 2: return m_NodeC;
            default: return m_NodeA;
        }
    }
    private RectTransform GetBusFromNode(RectTransform node)
    {
        if (node == m_NodeA) return m_BusA;
        if (node == m_NodeB) return m_BusB;
        if (node == m_NodeC) return m_BusC;
        return m_BusA;
    }
    public void OnButtonBackSC03()
    {
        ResetTopology();
        if (m_SC03 == null) return;
        m_SC03.BackSC03();
    }
}
