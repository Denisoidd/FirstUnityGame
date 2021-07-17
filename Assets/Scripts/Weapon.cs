using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform gunFireTransform;
    public float bulletSpeed = 10.0f;

    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isShooting = true;
        }
    }

    void FixedUpdate()
    {
        Shoot();
    }

    void Shoot()
    {
        if (isShooting)
        {
            Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, gunFireTransform.position, bullet.transform.rotation);
            bulletClone.AddForce(gunFireTransform.right * bulletSpeed, ForceMode.Impulse);
        }
        isShooting = false;
    }
}
