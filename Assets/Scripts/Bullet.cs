using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Rigidbody rigidBodyComponent;
    
    [Range(0, 100)]
    public float ricochetForce = 50f;
    
    private int maxRicochet = 2;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        Invoke("DisableBullet", 7f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (counter < maxRicochet)
        {
            rigidBodyComponent.AddForce(col.contacts[0].normal * ricochetForce, ForceMode.Impulse);
            counter++;
        }
        else
        {
            DisableBullet();
        }
        
    }
    
    void DisableBullet()
    {
        Destroy(gameObject);
    }
}
