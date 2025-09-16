using UnityEngine;
using UnityEngine.UI;

public class RingTopology : NetworkTopology
{
    [Header("Ring Settings")]
    [SerializeField] private SC03 m_SC03;
    [SerializeField] private RectTransform m_Ellipse;   

    private float m_Angle = 180f;
    private void Start()
    {
        ResetTopology();
    }
    protected override void UpdateMovement()
    {
        if (m_Ellipse == null || m_LetterController == null) return;

        float a = m_Ellipse.sizeDelta.x / 2f; 
        float b = m_Ellipse.sizeDelta.y / 2f; 

        Vector2 center = m_Ellipse.anchoredPosition; // Center of the ellipse
        float deltaAngle = m_Speed * Time.deltaTime;

        m_Angle -= deltaAngle; // Move clockwise
        m_Travelled += deltaAngle; // Track total angle travelled

        float x = center.x + a * Mathf.Cos(m_Angle); // Parametric equation for ellipse
        float y = center.y + b * Mathf.Sin(m_Angle); // Parametric equation for ellipse
        m_LetterController.anchoredPosition = new Vector2(x, y);

        // Check if a full loop is completed
        if (m_Travelled >= 2f * Mathf.PI)
        {
            SetMovingState(false, false, true);
        }
    }
    public override void ResetTopology()
    {
        //Debug.Log("ResetRingTopology");   
        SetMovingState(false, false, true);

        if (m_Ellipse == null || m_LetterController == null) return;

        
        float a = m_Ellipse.sizeDelta.x / 2f;
        float b = m_Ellipse.sizeDelta.y / 2f;

        Vector2 center = m_Ellipse.anchoredPosition;
        m_Angle = Mathf.PI;
        float x = center.x + a * Mathf.Cos(m_Angle);
        float y = center.y + b * Mathf.Sin(m_Angle);

        m_LetterController.anchoredPosition = new Vector2(x, y);
    }
    public override void SendSignal()
    {
        Debug.Log("OnButtonSendSignalRingTopology");
        SetMovingState(true, true, false);
    }
    public void OnButtonBackSC03()
    {
        ResetTopology();
        if (m_SC03 == null) return;
        m_SC03.OnBackSC03();   
    }
}
        

   

