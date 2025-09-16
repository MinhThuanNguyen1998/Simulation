using UnityEngine;
using UnityEngine.UI;

public abstract class NetworkTopology : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] protected Button m_ButtonSendSignal;
    [SerializeField] protected RectTransform m_LetterController;

    [Header("Movement Settings")]
    [SerializeField] protected float m_Speed = 1f;

    protected bool m_IsMoving = false;
    protected float m_Travelled = 0f;

    protected void SetMovingState(bool isMoving, bool activeCircle, bool buttonInteractable)
    {
        m_IsMoving = isMoving;
        if (isMoving)
        {
            m_Travelled = 0f;
        }
        if (m_LetterController != null)
        {
            m_LetterController.gameObject.SetActive(activeCircle);
        }
        if (m_ButtonSendSignal != null)
        {
            m_ButtonSendSignal.interactable = buttonInteractable;
        }
    }

    protected virtual void Update()
    {
        // To be implemented in derived classes
        if (m_IsMoving)
        {
            UpdateMovement();
        }
    }
    protected abstract void UpdateMovement();
    public abstract void ResetTopology();
    public abstract void SendSignal();
}
