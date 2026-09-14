using UnityEngine;
using UnityEngine.UI;

/// <summary>Connects the options-screen volume slider to live audio preview and persistent preferences.</summary>
public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private float defaultVolume = PlayerPrefsController.DefaultMasterVolume;

    private void Awake()
    {
        // Load without firing the listener, then subscribe and explicitly preview the saved value.
        volumeSlider.SetValueWithoutNotify(PlayerPrefsController.GetMasterVolume());
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(volumeSlider.value);
    }

    private void OnDestroy()
    {
        // Remove the runtime listener to avoid leaving this UI object referenced after destruction.
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // This is a preview only; persistence occurs when the user exits via SaveAndexit.
        AudioListener.volume = volume;
    }

    public void SaveAndexit()
    {
        // Commit the selected value before returning to the main menu.
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }

    public void SetDefaults()
    {
        // Assigning slider.value invokes the listener and therefore previews the default immediately.
        volumeSlider.value = defaultVolume;
    }
}
