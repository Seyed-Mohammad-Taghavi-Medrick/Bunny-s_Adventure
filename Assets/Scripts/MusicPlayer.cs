using UnityEngine;
/// <summary>Keeps the music object alive between scenes and applies the persisted master-volume preference.</summary>
public class MusicPlayer : MonoBehaviour
{

    private void Awake()
    {
        // This object is intended to be created once, then continue through scene loads.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Apply the saved setting when this persistent audio object first initializes.
        SetVolume(PlayerPrefsController.GetMasterVolume());
    }

    public void SetVolume(float volume)
    {
        // AudioListener is global, so clamping here protects all game audio from invalid input.
        AudioListener.volume = Mathf.Clamp01(volume);
    }
}
