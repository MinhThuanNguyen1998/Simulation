using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarTopology : DropDownHandlerStar_Bus_Topology
{
    [Header("Network Nodes")]
    [SerializeField] private RectTransform m_NodeA;
    [SerializeField] private RectTransform m_NodeB;
    [SerializeField] private RectTransform m_NodeC;
    [SerializeField] private RectTransform m_Switch;

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
        m_CurrentStartNode = GetNodeFromDropdown(m_DropdownStartNode);
        m_CurrentEndNode = GetNodeFromDropdown(m_DropdownEndNode);
        if (m_CurrentStartNode == m_CurrentEndNode)
        {
            PopupManager.Instance.ShowPopup(PopupType.Notification, Config.Text_Doi_Gia_Tri, Config.Text_OK);
            return;
        }
        SetMovingState(true, true, false);
        m_StartPoint = m_CurrentStartNode.position;
        m_EndPoint = m_Switch.position;
        m_IsMovingToSwitch = true;
        m_Travelled = 0f;
    }

    protected override void UpdateMovement()
    {
        if (m_IsMoving)
        {
            m_Travelled += m_Speed * Time.deltaTime; // Update travelled
            float ratio = m_Travelled / Vector3.Distance(m_StartPoint, m_EndPoint); // Calculate the percentage of the distance traveled to the total distance

            m_LetterController.position = Vector3.Lerp(m_StartPoint, m_EndPoint, ratio);

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
                    SetMovingState(false, false, true); // Finish moving
                    //Debug.Log("Signal arrived at destination!");
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
    


}
