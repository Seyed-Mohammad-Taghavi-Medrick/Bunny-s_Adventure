using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// رفتار اصلی بازیکن: حرکت افقی با دکمه‌های UI، نیروی جت‌پک، ظاهر پرش/سقوط و صداها.
/// وضعیت‌های عمومی پایین توسط Pickupها و GameManager خوانده یا تغییر داده می‌شوند.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] Sprite playerFallingSprite;
    [SerializeField] Sprite playerJumpingSprite;

    // این سه پرچم، وضعیت‌های موقت بازیکن هستند.
    public bool isJetpackenable;
    public bool isPlayerDamaged;
    public bool isBeingPulledIntoHole;
    public bool isShieldEnable;
    [SerializeField] public bool isdead;

    [Header("Audio")]
    public AudioSource jetPackAudioSource;
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip spring;
    public AudioClip hole;
    public AudioClip breakAblePlatform;
    public AudioClip jetPack;

    [Header("Horizontal movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lerpSpeed = 2f;

    // فیلدهای باقی‌مانده برای سازگاری با اتصال‌های قبلی Inspector نگه داشته شده‌اند.
    public Text gyroData;
    [SerializeField] GameObject lSideMirror;
    [SerializeField] GameObject rSideMirror;

    private Rigidbody2D playerRigid;
    private SpriteRenderer playerSpriteRenderer;
    private float smoothInput;
    private int inputHorizontal;

    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (isJetpackenable)
            playerRigid.AddForce(Vector2.up * 1000f * Time.fixedDeltaTime);

        if (!isPlayerDamaged && !isBeingPulledIntoHole)
        {
            // Mathf.Lerp حرکت دکمه‌ای را نرم می‌کند؛ -1 چپ و 1 راست است.
            smoothInput = Mathf.Lerp(smoothInput, inputHorizontal, Time.fixedDeltaTime * lerpSpeed);
            transform.Translate(Vector2.right * (smoothInput * moveSpeed * Time.fixedDeltaTime));
        }

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (playerRigid.velocity.y < -0.01f)
            playerSpriteRenderer.sprite = playerFallingSprite;
        else if (playerRigid.velocity.y > 0.01f)
            playerSpriteRenderer.sprite = playerJumpingSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // عبور از محدوده‌ی پایین صفحه یعنی بازیکن سقوط کرده است.
        if (other.CompareTag("Platform Manager"))
            isPlayerDamaged = true;
    }

    public void PlayJumpSFX() => audioSource.PlayOneShot(jump);
    public void PlayspringSFX() => audioSource.PlayOneShot(spring);
    public void BreakAblePlatformSFX() => audioSource.PlayOneShot(breakAblePlatform);

    public void PlayJetPackSFX()
    {
        // فقط اگر در حال پخش نیست شروع کن؛ از شروع‌شدن مجدد صدا در هر فریم جلوگیری می‌شود.
        if (!jetPackAudioSource.isPlaying)
            jetPackAudioSource.Play();
    }

    /// <summary>تابع متصل به دکمه‌های چپ و راست UI. مقدار باید -1، 0 یا 1 باشد.</summary>
    public void HorizontalMovment(int value)
    {
        inputHorizontal = Mathf.Clamp(value, -1, 1);
    }
}
