using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    [Header("SC02")]
    [SerializeField] protected SC02 m_SC02;

    protected virtual void OnEnable()
    {
        ResetState();
    }

    public abstract void OnButtonIntroduction();

    public abstract void OnButtonComplete();

    protected abstract void ResetState();

    public virtual void OnBackSC02()
    {
        m_SC02.BackSC02();
    }
}
