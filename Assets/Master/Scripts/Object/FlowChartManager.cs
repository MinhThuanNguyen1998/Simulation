using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class FlowChartManager : MonoBehaviour
{
    [Header("Images True")]
    [SerializeField] private List<Image> m_ListTrueImages;

    [Header("Images False")]
    [SerializeField] private List<Image> m_ListFalseImages;

    [Header("Color")]
    [SerializeField] private Color m_HighlighColor = Color.green;
    [SerializeField] private Color m_NormalColor = Color.white;

    [Header("Toggles")]
    [SerializeField] private Toggle m_ToggleNTrue;
    [SerializeField] private Toggle m_ToggleNFalse;

    [Header("Scene Controller")]
    [SerializeField] protected MonoBehaviour m_SceneController;

    private ISceneController SceneController => m_SceneController as ISceneController;
    private int m_CurrentTrue = 0;
    private int m_CurrentFalse = 0;

    private void OnEnable()
    {
        ResetFlow();
    }

    public void OnButtonRunFlow()
    {
        //Debug.Log("OnButtonNext");
        m_ToggleNTrue.interactable = false;
        m_ToggleNFalse.interactable = false;
        if (m_ToggleNTrue.isOn)
        {
            HighlightList(m_ListTrueImages, ref m_CurrentTrue);
        }
        else if (m_ToggleNFalse.isOn)
        {
            HighlightList(m_ListFalseImages, ref m_CurrentFalse);
        }
    }
    private void HighlightList(List<Image> list, ref int currentIndex)
    {
        //Debug.Log("HighlightList: " + currentIndex);
        foreach (var img in list)
            img.color = m_NormalColor;
        list[currentIndex].color = m_HighlighColor;
        currentIndex++;
        if (currentIndex >= list.Count)
            currentIndex = 0;
    }
    public void ResetFlow()
    {
        //Debug.Log("ResetFlow");
        foreach (var img in m_ListTrueImages)
            img.color = m_NormalColor;

        foreach (var img in m_ListFalseImages)
            img.color = m_NormalColor;

        m_CurrentTrue = 0;
        m_CurrentFalse = 0;

        m_ToggleNTrue.interactable = true;
        m_ToggleNFalse.interactable = true;

        m_ToggleNTrue.isOn = false;
        m_ToggleNFalse.isOn = false;
    }

    public void OnButtonBackSC01()
    {
        SceneController?.BackPreviousScene();
    }

}

