﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Rigidbody rigidBodyComponent;
    
    [Range(0, 100)]
    public float ricochetForce = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        rigidBodyComponent.AddForce(col.contacts[0].normal * ricochetForce, ForceMode.Impulse);
    }
}