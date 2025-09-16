using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BusTopology : NetworkTopology
{
    [Header("Ring settings")]
    [SerializeField] private SC03 m_SC03;

    [Header("Network Nodes")]
    [SerializeField] private RectTransform m_NodeA;
    [SerializeField] private RectTransform m_NodeB;
    [SerializeField] private RectTransform m_NodeC;

    [Header("Bus Line Points")]
    [SerializeField] private Transform m_BusPointA;
    [SerializeField] private Transform m_BusPointB;
    [SerializeField] private Transform m_BusPointC;


    [Header("UI Dropdowns")]
    [SerializeField] private TMP_Dropdown m_DropdownStartNode;
    [SerializeField] private TMP_Dropdown m_DropdownEndNode;

    private RectTransform m_CurrentStartNode;
    private RectTransform m_CurrentEndNode;
    
    private Vector3 m_StartPoint;
    private Vector3 m_EndPoint;

    private Transform m_CurrentStartBusPoint;
    private Transform m_CurrentEndBusPoint;

    [SerializeField] private List<Vector3> m_Path = new List<Vector3>();
    private int m_CurrentPathIndex = 0;

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
            PopupManager.Instance.ShowPopup(Config.Text_Doi_Gia_Tri, Config.Text_OK);
            return;
        }
        m_CurrentStartBusPoint = GetBusPointFromNode(m_CurrentStartNode);
        m_CurrentEndBusPoint = GetBusPointFromNode(m_CurrentEndNode);
        BuildPath();
        SetMovingState(true, true, false);
        m_CurrentPathIndex = 0;
        MoveToNextPoint();
      
    }
    private void MoveToNextPoint()
    {
        if (m_CurrentPathIndex < m_Path.Count - 1)
        {
            m_StartPoint = m_Path[m_CurrentPathIndex];
            m_EndPoint = m_Path[m_CurrentPathIndex + 1];
            m_Travelled = 0f;
            m_CurrentPathIndex++;
        }
        else
        {

            SetMovingState(false, false, true);
        }
    }
    private void BuildPath()
    {
        m_Path.Clear();
        m_Path.Add(m_CurrentStartNode.position);
        m_Path.Add(m_CurrentStartBusPoint.position);

        float startX = m_CurrentStartBusPoint.position.x;
        float endX = m_CurrentEndBusPoint.position.x;
        if (startX < endX)
        {
           
            if (m_BusPointA.position.x > startX && m_BusPointA.position.x < endX) m_Path.Add(m_BusPointA.position);
            if (m_BusPointB.position.x > startX && m_BusPointB.position.x < endX) m_Path.Add(m_BusPointB.position);
            if (m_BusPointC.position.x > startX && m_BusPointC.position.x < endX) m_Path.Add(m_BusPointC.position);
            m_Path.Add(m_CurrentEndBusPoint.position);
        }
        else
        {
            if (m_BusPointC.position.x < startX && m_BusPointC.position.x > endX) m_Path.Add(m_BusPointC.position);
            if (m_BusPointB.position.x < startX && m_BusPointB.position.x > endX) m_Path.Add(m_BusPointB.position);
            if (m_BusPointA.position.x < startX && m_BusPointA.position.x > endX) m_Path.Add(m_BusPointA.position);

            m_Path.Add(m_CurrentEndBusPoint.position);
        }
        m_Path.Add(m_CurrentEndNode.position);
    }
    protected override void UpdateMovement()
    {
        if (m_IsMoving)
        {
            m_Travelled += m_Speed * Time.deltaTime;
            float ratio = m_Travelled / Vector3.Distance(m_StartPoint, m_EndPoint);
            m_CircleController.position = Vector3.Lerp(m_StartPoint, m_EndPoint, ratio);
            if (ratio >= 1.0f)
            {
                MoveToNextPoint();
            }
        }
    }
    public override void ResetTopology()
    {
        SetMovingState(false, false, true);
        m_CircleController.gameObject.SetActive(true);
        m_CircleController.position = GetNodeFromDropdown(m_DropdownStartNode).position;
        m_CircleController.gameObject.SetActive(false);
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
    private Transform GetBusPointFromNode(RectTransform node)
    {
        if (node == m_NodeA) return m_BusPointA;
        if (node == m_NodeB) return m_BusPointB;
        if (node == m_NodeC) return m_BusPointC;
        return null;
    }
    public void OnButtonBackSC03()
    {
        ResetTopology();
        if (m_SC03 == null) return;
        m_SC03.OnBackSC03();
    }
}
