using System.Collections;
using UnityEngine;

/// <summary>
/// کنترل‌کننده‌ی وضعیت‌های کلی بازی: مرگ بازیکن و پاورآپ‌ها.
/// هر وضعیت فقط یک Coroutine فعال دارد؛ بنابراین صدا و تایمرها تکراری اجرا نمی‌شوند.
/// </summary>
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
        // این اجزا در ابتدای بازی یک‌بار پیدا می‌شوند، نه در هر فریم.
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

        if (loseAudio != null)
            audioSource.PlayOneShot(loseAudio);

        yield return new WaitForSeconds(1f);
        lostCanvas.SetActive(true);
    }

    private void UpdateJetpack()
    {
        jetpack.SetActive(player.isJetpackenable);

        if (!player.isJetpackenable && player.jetPackAudioSource.isPlaying)
            player.jetPackAudioSource.Stop();

        if (player.isJetpackenable && jetpackTimer == null)
        {
            player.PlayJetPackSFX();
            jetpackTimer = StartCoroutine(DisableJetPack());
        }

        // هنگام بالا رفتن، برخورد با سکوها موقتاً خاموش است.
        if (!player.isPlayerDamaged)
            playerCollider.enabled = playerRigidbody.velocity.y <= 0f;
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
