using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private egg[] eggs;

    [SerializeField] AudioSource jumpAudioSource;
    [SerializeField] private Player player;
    private float _nextFire;
    private Vector2 movment;

    private Rigidbody2D playerRigid;

    // PowerUps
    public bool isJetpackenable;
    public bool isPlayerDamaged;
    public bool isShieldEnable;

    [SerializeField] public bool isdead;

    public AudioSource jetPackAudioSource;
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip spring;
    public AudioClip hole;
    public AudioClip breakAblePlatform;
    public AudioClip jetPack;

    
        // Start is called before the first frame update
        void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");


        movment = new Vector2(inputX * 10, playerRigid.velocity.y);

        if (isJetpackenable)
        {
            playerRigid.AddForce(transform.up *20, ForceMode2D.Force);
        }
    }

    private void FixedUpdate()
    {
        if (!isPlayerDamaged)
        {
            playerRigid.velocity = movment;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform" /* && gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0f*/)
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform Manager")
        {
            isPlayerDamaged = true;
        }
    }

    public void PlayJumpSFX()
    {
        audioSource.PlayOneShot(jump);
    }

    public void PlayspringSFX()
    {
        audioSource.PlayOneShot(spring);
    }

    public void BreakAblePlatformSFX()
    {
        audioSource.PlayOneShot(breakAblePlatform);
    }

    public void PlayJetPackSFX()
    {
        jetPackAudioSource.Play(0);
    }
}