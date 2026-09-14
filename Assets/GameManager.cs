using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Coordinates game-wide player states: death presentation, temporary power-ups, and hit audio.</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip loseAudio;
    [SerializeField] private Player player;
    // Scene references for the death effect and the two temporary power-up visuals.
    [SerializeField] private GameObject stars;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject jetpack;

    [SerializeField] private GameObject lostCanvas;


    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip eggThrow;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame


    void Update()
    {
        // Enemy damage is surfaced as a flag and translated here into the shared egg-hit sound.
        if (FindObjectOfType<Enemy>().enemyDamaged)
        {
            PlayEggHit();
        }


        if (FindObjectOfType<Player>().isPlayerDamaged)
        {
            // Begin the death sequence after any hazard marks the player as damaged.
            StartCoroutine(KillPlayer());
        }

        IEnumerator KillPlayer()
        {
            // Disable landing, force the fall, then reveal the loss UI after the visual beat.
            stars.SetActive(true);
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10;
            player.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1);
            lostCanvas.gameObject.SetActive(true);
        }


        if (FindObjectOfType<Player>().isJetpackenable)
        {
            // While enabled, show the jetpack and temporarily disable the player collider during ascent.
            jetpack.SetActive(true);
            player.GetComponent<Player>().PlayJetPackSFX();
            StartCoroutine(DisableJetPack());
            if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            jetpack.SetActive(false);
        }

        if (player.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            if (!FindObjectOfType<Player>().isPlayerDamaged)
            {
                player.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }


        if (FindObjectOfType<Player>().isShieldEnable)
        {
            // Shield visibility and expiry are deliberately controlled centrally, not by the pickup trigger.
            shield.SetActive(true);

            StartCoroutine(DisableShield());
        }
    }


    IEnumerator DisableJetPack()
    {
        // Jetpack is a five-second timed state measured in scaled game time.
        yield return new WaitForSeconds(5);
        FindObjectOfType<Player>().gameObject.GetComponent<Player>().isJetpackenable = false;
        jetpack.SetActive(false);
    }

    IEnumerator DisableShield()
    {
        // Shield lasts longer than jetpack: ten seconds in scaled game time.
        yield return new WaitForSeconds(10);
        FindObjectOfType<Player>().gameObject.GetComponent<Player>().isShieldEnable = false;
        shield.SetActive(false);
    }

    void PlayEggHit()
    {
        // PlayOneShot allows this effect without interrupting any clip already assigned to the source.
        audioSource.PlayOneShot(eggThrow);
    }
}
