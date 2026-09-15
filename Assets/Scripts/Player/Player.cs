using System;
using UnityEngine;
using UnityEngine.UI;

/* Script: Controls player movement, jetpack force, jump/fall sprites, damage state, and player sounds.
   Cheat sheet: RequireComponent enforces required components; Rigidbody2D handles 2D physics; Mathf.Lerp smooths a changing value. */

/// <summary>Controls player movement, temporary states, visuals, and sound effects.</summary>
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] Sprite playerFallingSprite;
    [SerializeField] Sprite playerJumpingSprite;

    // These flags are shared temporary states used by pickups, hazards, and GameManager.
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

    // Legacy Inspector references are kept so existing scene bindings remain valid.
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
            // Lerp smooths button input; -1 means left and 1 means right.
            int keyboardHorizontal = 0;
            if (Input.GetKey(KeyCode.A))
                keyboardHorizontal--;
            if (Input.GetKey(KeyCode.D))
                keyboardHorizontal++;

            int horizontalInput = Mathf.Clamp(inputHorizontal + keyboardHorizontal, -1, 1);
            smoothInput = Mathf.Lerp(smoothInput, horizontalInput, Time.fixedDeltaTime * lerpSpeed);
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
        // Entering the lower cleanup area means the player fell off-screen.
        if (other.CompareTag("Platform Manager"))
            isPlayerDamaged = true;
    }

    public void PlayJumpSFX() => audioSource.PlayOneShot(jump);
    public void PlayspringSFX() => audioSource.PlayOneShot(spring);
    public void BreakAblePlatformSFX() => audioSource.PlayOneShot(breakAblePlatform);

    public void PlayJetPackSFX()
    {
        // Start the loop only once; this prevents restarting the sound every frame.
        if (!jetPackAudioSource.isPlaying)
            jetPackAudioSource.Play();
    }

    /// <summary>UI button callback. Value should be -1, 0, or 1.</summary>
    public void HorizontalMovment(int value)
    {
        inputHorizontal = Mathf.Clamp(value, -1, 1);
    }
}
