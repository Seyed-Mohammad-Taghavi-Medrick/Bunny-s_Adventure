using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

/// <summary>Owns player movement, power-up state flags, boundary wrapping, hazard detection, and player sound effects.</summary>
public class Player : MonoBehaviour
{
    [SerializeField] private egg[] eggs;

    [SerializeField] AudioSource jumpAudioSource;
    [SerializeField] private Player player;
    private float _nextFire;
    private Vector2 movment;
    private Rigidbody2D playerRigid;
    private Gyroscope _gyro;

    public Text gyroData;

    // Power-up and damage states are read and orchestrated by GameManager and collision components.
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
    float targetInput;
    int inputHorizontal;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float lerpSpeed = 2f;


    // Scene boundary markers used for horizontal world wrapping.
    [SerializeField] GameObject lSideMirror;

    [SerializeField] GameObject rSideMirror;

    // Start is called before the first frame update
    void Start()
    {
        // Gyroscope is enabled for the retained mobile-control experiments; current horizontal input uses UI/buttons.
        _gyro = Input.gyro;
        _gyro.enabled = true;
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
        


        float inputX = Input.GetAxis("Horizontal");


        movment = new Vector2(inputX * 10, playerRigid.velocity.y);

        if (isJetpackenable)
        {
            // Jetpack adds continuous upward force while GameManager keeps its timed state active.
            playerRigid.AddForce(transform.up * 1000 * Time.deltaTime, ForceMode2D.Force);
        }
    }

    private void FixedUpdate()
    {
        // Exact-position boundary checks move the player to the opposite side of the play field.
        if (/*gameObject.GetComponent<BoxCollider2D>() is null &&*/
            gameObject.transform.position.x == lSideMirror.transform.position.x)
        {
            Vector3 newPosition = gameObject.transform.position;
            newPosition.x = rSideMirror.transform.position.x - 1;
            gameObject.transform.position = newPosition;
        }

        if (/*gameObject.GetComponent<BoxCollider2D>() is null &&*/
            gameObject.transform.position.x == rSideMirror.transform.position.x)
        {
            Vector3 newPosition = gameObject.transform.position;
            newPosition.x = lSideMirror.transform.position.x - +1;
            gameObject.transform.position = newPosition;
        }

        
        
        targetInput = Mathf.Lerp(targetInput, inputHorizontal, Time.deltaTime * lerpSpeed);
        if (!isPlayerDamaged)
        {
            // Smooth button input to avoid abrupt horizontal translation; damaged players no longer respond.
            transform.Translate(targetInput * moveSpeed * Time.deltaTime, 0, 0);


            /*playerRigid.velocity = new Vector2(_gyro.attitude.z,playerRigid.velocity.y) * 10f;*/
            /*playerRigid.velocity = movment;*/

            /*gyroData.text =
                $"Gyro rotation rate: {_gyro.rotationRate}\nGyro attitude:{_gyro.attitude}\nGyro enabled: {_gyro.enabled}";*/
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
        // Falling into the Platform Manager cleanup area starts the shared player-death flow.
        if (other.gameObject.tag == "Platform Manager")
        {
            isPlayerDamaged = true;
        }
    }

    public void PlayJumpSFX()
    {
        // Called by ordinary Platform landings.
        audioSource.PlayOneShot(jump);
    }

    public void PlayspringSFX()
    {
        // Called by Spring landings.
        audioSource.PlayOneShot(spring);
    }

    public void BreakAblePlatformSFX()
    {
        // Called when a breakable platform begins its collapse sequence.
        audioSource.PlayOneShot(breakAblePlatform);
    }

    public void PlayJetPackSFX()
    {
        // Uses the dedicated looping/assigned jetpack source rather than the shared one-shot source.
        jetPackAudioSource.Play(0);
    }

    public void HorizontalMovment(int value)
    {
        // UI buttons pass -1/0/1 here; FixedUpdate smooths and applies the requested direction.
        inputHorizontal = value;
    }
}
