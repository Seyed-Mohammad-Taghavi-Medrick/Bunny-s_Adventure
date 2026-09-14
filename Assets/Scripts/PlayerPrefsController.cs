using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Single access point for persisting and validating the global master-volume setting.</summary>
public class PlayerPrefsController : MonoBehaviour
{
    private const string MasterVolumeKey = "master volume";
    public const float DefaultMasterVolume = 0.8f;
    private const float MinVolume = 0f;
    private const float MaxVolume = 1f;

   

    public static void SetMasterVolume(float volume)
    {
        // Reject invalid values rather than silently writing a corrupted preference.
        if (volume >= MinVolume && volume <= MaxVolume)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, volume);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static float GetMasterVolume()
    {
        // A new installation receives the configured default when no preference has been saved.
        return PlayerPrefs.GetFloat(MasterVolumeKey, DefaultMasterVolume);
    }
}
