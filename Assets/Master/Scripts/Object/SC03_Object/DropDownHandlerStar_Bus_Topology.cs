using TMPro;
using UnityEngine;

public abstract class DropDownHandlerStar_Bus_Topology : NetworkTopology
{
    [Header("UI Dropdowns")]
    [SerializeField] protected TMP_Dropdown m_DropdownStartNode;
    [SerializeField] protected TMP_Dropdown m_DropdownEndNode;

    protected virtual void Awake()
    {
        if (m_DropdownStartNode != null)
            m_DropdownStartNode.onValueChanged.AddListener(OnValueChanged);

        if (m_DropdownEndNode != null)
            m_DropdownEndNode.onValueChanged.AddListener(OnValueChanged);
    }

    protected virtual void OnValueChanged(int value)
    {
        AudioManager.Instance.PlayOnShot(SoundType.Toggle);
    }
}
