using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform gunFireTransform;
    public float bulletSpeed = 10.0f;
    public float fireRate = 1.0f;
    public Player playerScript;

    private bool isShooting = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {

    }

    void Shoot()
    {
        if (isShooting)
        {
            playerScript.PlayerRecoil();
            Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, gunFireTransform.position, bullet.transform.rotation);
            bulletClone.AddForce(gunFireTransform.right * bulletSpeed, ForceMode.Impulse);
            StartCoroutine("FireDelay");
        }
    }

    IEnumerator FireDelay()
    {
        isShooting = false;
        yield return new WaitForSeconds(fireRate);
        isShooting = true;
    }
}
