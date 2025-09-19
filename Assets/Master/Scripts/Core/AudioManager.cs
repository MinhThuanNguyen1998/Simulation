using UnityEngine;

public enum SoundType
{
    Button,
    Popup,
    Toggle
}
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioPopupClip;
    [SerializeField] private AudioClip m_AudioButtonClip;
    [SerializeField] private AudioClip m_AudioToggleClip;

    public void PlayOnShot(SoundType soundType)
    {
        AudioClip clip = null;
        switch (soundType)
        {
            case SoundType.Button:
                clip = m_AudioButtonClip;
                break;
            case SoundType.Popup:
                clip = m_AudioPopupClip;
                break;
            case SoundType.Toggle:
                clip = m_AudioToggleClip;
                break;

        }
        if (clip != null)
        {
            m_AudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Audio clip for {soundType} is missing!");
        }
    }
}
