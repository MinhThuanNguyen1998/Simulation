using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIEffect : MonoBehaviour
{
    [SerializeField] private  Button m_Button;

    private void Awake()
    {
        if(m_Button != null)
        {
            m_Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayOnShot(SoundType.Button);
            });
        }
       
    }

}
