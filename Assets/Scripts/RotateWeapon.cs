using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    public Transform gun;
    public float rotationSpeed = 200;

    private float moveY;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveY = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        gun.Rotate(0, 0, moveY, Space.Self);
    }
}
