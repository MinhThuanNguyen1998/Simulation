using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarTopology : NetworkTopology
{
    [Header("Network Nodes")]
    [SerializeField] private RectTransform m_NodeA;
    [SerializeField] private RectTransform m_NodeB;
    [SerializeField] private RectTransform m_NodeC;
    [SerializeField] private RectTransform m_Switch;

    [Header("UI Dropdowns")]
    [SerializeField] private TMP_Dropdown m_DropdownStartNode;
    [SerializeField] private TMP_Dropdown m_DropdownEndNode;

    private RectTransform m_CurrentStartNode;
    private RectTransform m_CurrentEndNode;

    private Vector3 m_StartPoint;
    private Vector3 m_EndPoint;
    private bool m_IsMovingToSwitch = false;

    private void Start()
    {
        ResetTopology();
    }

    public override void SendSignal()
    {
        SetMovingState(true, true, false);

        m_CurrentStartNode = GetNodeFromDropdown(m_DropdownStartNode);
        m_CurrentEndNode = GetNodeFromDropdown(m_DropdownEndNode);

        m_StartPoint = m_CurrentStartNode.position;
        m_EndPoint = m_Switch.position;
        m_IsMovingToSwitch = true;
        m_Travelled = 0f;
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
                if (m_IsMovingToSwitch)
                {
                    m_StartPoint = m_Switch.position;
                    m_EndPoint = m_CurrentEndNode.position;
                    m_IsMovingToSwitch = false;
                    m_Travelled = 0f;
                }
                else
                {
                    SetMovingState(false, true, true);
                    //Debug.Log("Signal arrived at destination!");
                }
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
}
