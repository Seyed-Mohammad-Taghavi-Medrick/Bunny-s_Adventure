using System.Collections;
using UnityEngine;

/* Script: Coordinates player death plus timed shield and jetpack effects.
   Cheat sheet: Awake caches components; Coroutine pauses work with yield; SetActive shows or hides a GameObject. */

/// <summary>Coordinates player death and the timed shield and jetpack states.</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip loseAudio;
    [SerializeField] private Player player;
    [SerializeField] private GameObject stars;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject jetpack;
    [SerializeField] private GameObject lostCanvas;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip eggThrow;

    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;
    private bool deathSequenceStarted;
    private Coroutine jetpackTimer;
    private Coroutine shieldTimer;

    private void Awake()
    {
        // Cache physics components once instead of searching for them every frame.
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerCollider = player.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (player.isPlayerDamaged && !deathSequenceStarted)
            StartCoroutine(KillPlayer());

        UpdateJetpack();
        UpdateShield();
    }

    private IEnumerator KillPlayer()
    {
        deathSequenceStarted = true;
        stars.SetActive(true);
        playerCollider.enabled = false;
        playerRigidbody.velocity = Vector2.down * 10f;

        if (loseAudio != null && audioSource != null)
            audioSource.PlayOneShot(loseAudio);

        yield return new WaitForSeconds(1f);
        lostCanvas.SetActive(true);
    }

    private void UpdateJetpack()
    {
        jetpack.SetActive(player.isJetpackenable);

        if (!player.isJetpackenable && player.jetPackAudioSource != null && player.jetPackAudioSource.isPlaying)
            player.jetPackAudioSource.Stop();

        if (player.isJetpackenable && jetpackTimer == null)
        {
            player.PlayJetPackSFX();
            jetpackTimer = StartCoroutine(DisableJetPack());
        }

    }

    private IEnumerator DisableJetPack()
    {
        yield return new WaitForSeconds(5f);
        player.isJetpackenable = false;
        jetpackTimer = null;
    }

    private void UpdateShield()
    {
        shield.SetActive(player.isShieldEnable);

        if (player.isShieldEnable && shieldTimer == null)
            shieldTimer = StartCoroutine(DisableShield());
    }

    private IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(10f);
        player.isShieldEnable = false;
        shieldTimer = null;
    }
}
