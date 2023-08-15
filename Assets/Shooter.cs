using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private float _nextFire;
    private Vector2 movment;
    [SerializeField] private float fireRate = .5f;
    private Rigidbody2D playerRigid;


    // new sooter

    [SerializeField] private Camera camera;

    private Vector3 mousePos;
    public float minAngle = 45;

    public float maxAngle = 135;
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
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);


        Vector3 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        angle = Mathf.Repeat(angle, 360);

        if (((Input.GetButtonDown("Fire1") && angle >= minAngle && angle <= maxAngle)) &&
            Time.time > _nextFire  && ! player.isPlayerDamaged)
        {
            _nextFire = Time.time + fireRate;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            Shoot();
        }

        if (Input.GetButtonUp("Fire1")  && ! player.isPlayerDamaged)
        {
            Vector3 newEulerAngles = transform.eulerAngles;
            newEulerAngles.z = 0; 
            transform.eulerAngles = newEulerAngles;
        }

        void Shoot()
        {
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