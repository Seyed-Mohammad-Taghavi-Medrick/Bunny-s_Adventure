using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script: Aims the shooter at the mouse and spawns a random egg on a valid click.
   Cheat sheet: Update runs every frame; SerializeField exposes a private value in Inspector; Instantiate creates a prefab. */

/// <summary>Rotates the firing point toward the mouse and creates a randomly selected egg projectile on a valid click.</summary>
public class Shooter : MonoBehaviour
{
    private float _nextFire;
    private Vector2 movment;
    // Minimum real-time interval between shots.
    [SerializeField] private float fireRate = .5f;
    private Rigidbody2D playerRigid;


    // new sooter

    // Camera used to convert the cursor's screen position to world space.
    [SerializeField] private Camera camera;

    private Vector3 mousePos;
    // Inclusive aiming arc, in world-space degrees, that restricts firing to the upward direction.
    public float minAngle = 45;

    public float maxAngle = 135;
    // Transform/prefab location and projectile variants configured from the Inspector.
    [SerializeField] private GameObject projectile;
    [SerializeField] private egg[] eggs;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Derive a normalized 0-to-360 degree aim angle from the shooter to the mouse every frame.
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);


        Vector3 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        angle = Mathf.Repeat(angle, 360);

        if (((Input.GetButtonDown("Fire1") && angle >= minAngle && angle <= maxAngle)) &&
            Time.time > _nextFire  && ! player.isPlayerDamaged)
        {
            // Fire once, then lock firing until the cooldown expires. Damage state disables player actions.
            _nextFire = Time.time + fireRate;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            Shoot();
        }

        if (Input.GetButtonUp("Fire1")  && ! player.isPlayerDamaged)
        {
            // Returning the sprite to neutral on release keeps its idle orientation consistent.
            Vector3 newEulerAngles = transform.eulerAngles;
            newEulerAngles.z = 0; 
            transform.eulerAngles = newEulerAngles;
        }

        void Shoot()
        {
            // Each shot picks one egg variant, preserving the same configured spawn transform.
            egg bullet = eggs[Random.Range(0, eggs.Length)];
            Instantiate(bullet, projectile.transform.position, projectile.transform.rotation);
        }
    }


    /*private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;

            egg bullet = eggs[Random.Range(0, eggs.Length)];
            Instantiate(bullet, projectile.transform.position, projectile.transform.rotation);
        }
    }*/
}
