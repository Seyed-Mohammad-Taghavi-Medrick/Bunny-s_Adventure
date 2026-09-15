using UnityEngine;

/* Script: Saves, validates, and applies the global master-volume setting.
   Cheat sheet: static members belong to the class; PlayerPrefs stores simple local values; Mathf.Clamp limits a number to a range. */

/// <summary>Single access point for persisting and validating the global master-volume setting.</summary>
public class PlayerPrefsController : MonoBehaviour
{
    private const string MasterVolumeKey = "master volume";
    public const float DefaultMasterVolume = 0.8f;
    private const float MinVolume = 0f;
    private const float MaxVolume = 1f;

   

    public static void SetMasterVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, MinVolume, MaxVolume);
        PlayerPrefs.SetFloat(MasterVolumeKey, clampedVolume);
        PlayerPrefs.Save();
        ApplyMasterVolume();
    }

    public static float GetMasterVolume()
    {
        // A new installation receives the configured default when no preference has been saved.
        return Mathf.Clamp(PlayerPrefs.GetFloat(MasterVolumeKey, DefaultMasterVolume), MinVolume, MaxVolume);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ApplySavedMasterVolumeOnStartup()
    {
        ApplyMasterVolume();
    }

    public static void ApplyMasterVolume()
    {
        AudioListener.volume = GetMasterVolume();
    }
}
