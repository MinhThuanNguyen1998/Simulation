using UnityEngine;
using UnityEngine.UI;

public class RingTopology : MonoBehaviour
{
    [SerializeField] private SC03 m_SC03;
    [SerializeField] private Button m_ButtonSendSignal;
    [SerializeField] private RectTransform m_Ellipse;   
    [SerializeField] private RectTransform m_CircleController; 
    [SerializeField] private float m_Speed = 1f; 

    private float m_Angle = 180f;
    private bool m_IsMoving = false;
    private float m_Travelled = 0f;
    private void Start()
    {
        Reset();
    }
    void Update()
    {
        if (!m_IsMoving) return;
        if (m_Ellipse == null || m_CircleController == null) return;

        float a = m_Ellipse.sizeDelta.x / 2f;
        float b = m_Ellipse.sizeDelta.y / 2f;

        Vector2 center = m_Ellipse.anchoredPosition;
        float deltaAngle = m_Speed * Time.deltaTime;
        m_Angle -= deltaAngle;
        m_Travelled += deltaAngle;

        float x = center.x + a * Mathf.Cos(m_Angle);
        float y = center.y + b * Mathf.Sin(m_Angle);
        m_CircleController.anchoredPosition = new Vector2(x, y);

        // Kiểm tra đã đi hết 1 vòng (2π radian = 360°)
        if (m_Travelled >= 2f * Mathf.PI)
        {
            SetMovingState(false, false, true);
        }
    }
    private void Reset()
    {
        //Debug.Log("ResetRingTopology");   
        SetMovingState(false, false, true);

        if (m_Ellipse == null || m_CircleController == null) return;

        // Lấy bán trục ellipse
        float a = m_Ellipse.sizeDelta.x / 2f;
        float b = m_Ellipse.sizeDelta.y / 2f;

        Vector2 center = m_Ellipse.anchoredPosition;
        m_Angle = Mathf.PI;
        float x = center.x + a * Mathf.Cos(m_Angle);
        float y = center.y + b * Mathf.Sin(m_Angle);

        m_CircleController.anchoredPosition = new Vector2(x, y);
    }
    public void OnButtonSendSignal()
    {
        Debug.Log("OnButtonSendSignalRingTopology");
        SetMovingState(true, true, false);
    }
    public void OnButtonBackSC03()
    {
        Reset();
        if (m_SC03 == null) return;
        m_SC03.OnBackSC03();   
    }

    private void SetMovingState(bool isMoving, bool activeCircleController, bool activeButton)
    {
        //Debug.Log("SetStateRingNetwork");
        m_IsMoving = isMoving;
        m_Travelled = 0f;
        m_CircleController.gameObject.SetActive(activeCircleController);
        m_ButtonSendSignal.interactable = activeButton;
    }
}
        

   

