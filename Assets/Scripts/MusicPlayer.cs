using UnityEngine;

/* Script: Keeps one background-music object alive between scenes and starts its loop.
   Cheat sheet: Singleton pattern keeps one shared instance; DontDestroyOnLoad preserves an object across scene loads; AudioSource plays audio. */
/// <summary>Keeps the music object alive between scenes and applies the persisted master-volume preference.</summary>
public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerPrefsController.ApplyMasterVolume();

        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogWarning("MusicPlayer needs an AudioSource with a music clip.", this);
            return;
        }

        audioSource.loop = true;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
