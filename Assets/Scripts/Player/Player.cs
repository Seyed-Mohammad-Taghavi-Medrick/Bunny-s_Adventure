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
    float targetInput;
    int inputHorizontal;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float lerpSpeed = 2f;


    [SerializeField] GameObject lSideMirror;

    [SerializeField] GameObject rSideMirror;

    // Start is called before the first frame update
    void Start()
    {
        _gyro = Input.gyro;
        _gyro.enabled = true;
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
        */


        float inputX = Input.GetAxis("Horizontal");


        movment = new Vector2(inputX * 10, playerRigid.velocity.y);

        if (isJetpackenable)
        {
            playerRigid.AddForce(transform.up * 1000 * Time.deltaTime, ForceMode2D.Force);
        }
    }

    private void FixedUpdate()
    {
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

    public void HorizontalMovment(int value)
    {
        inputHorizontal = value;
    }
}