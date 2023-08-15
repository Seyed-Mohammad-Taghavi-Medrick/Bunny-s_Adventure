using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip loseAudio;
    [SerializeField] private Player player;
    [SerializeField] private GameObject stars;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject jetpack;

    [SerializeField] private GameObject lostCanvas;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame


    void Update()
    {
        if (FindObjectOfType<Player>().isPlayerDamaged)
        {
            StartCoroutine(KillPlayer());
        }

        IEnumerator KillPlayer()
        {
            stars.SetActive(true);
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10;
            player.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1);
            lostCanvas.gameObject.SetActive(true);
        }


        if (FindObjectOfType<Player>().isJetpackenable)
        {
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
            shield.SetActive(true);
            
            StartCoroutine(DisableShield());
        }
    }


    IEnumerator DisableJetPack()
    {
        yield return new WaitForSeconds(5);
        FindObjectOfType<Player>().gameObject.GetComponent<Player>().isJetpackenable = false;
        jetpack.SetActive(false);
    }

    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(10);
        FindObjectOfType<Player>().gameObject.GetComponent<Player>().isShieldEnable = false;
        shield.SetActive(false);
    }
}