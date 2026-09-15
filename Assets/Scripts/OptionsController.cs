using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

/* Script: Connects the options volume slider to immediate audio preview and saved settings.
   Cheat sheet: UI events call listeners on value changes; OnDestroy cleans up listeners; FormerlySerializedAs preserves old Inspector data. */

/// <summary>Connects the options-screen volume slider to live audio preview and persistent preferences.</summary>
public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    [FormerlySerializedAs("defaultvolume")]
    [SerializeField] private float defaultVolume = PlayerPrefsController.DefaultMasterVolume;

    private void Awake()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("OptionsController requires a volume slider.", this);
            enabled = false;
            return;
        }

        // Load without firing the listener, then subscribe and explicitly preview the saved value.
        volumeSlider.SetValueWithoutNotify(PlayerPrefsController.GetMasterVolume());
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(volumeSlider.value);
    }

    private void OnDestroy()
    {
        // Remove the runtime listener to avoid leaving this UI object referenced after destruction.
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Persist immediately so the setting survives scene changes even if the player leaves without Exit.
        PlayerPrefsController.SetMasterVolume(volume);
    }

    public void BackToMenu()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);

        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        if (levelLoader != null)
            levelLoader.LoadMainMenu();
        else
            Debug.LogError("OptionsController could not find a LevelLoader to return to the main menu.", this);
    }

    // Kept for existing UnityEvent bindings from older scene versions.
    public void SaveAndexit() => BackToMenu();

    public void SetDefaults()
    {
        // Assigning slider.value invokes the listener and therefore previews the default immediately.
        volumeSlider.value = defaultVolume;
    }
}
