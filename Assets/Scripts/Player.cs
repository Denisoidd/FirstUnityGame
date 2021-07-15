using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    private int superJumpsRemaining;
    private bool isFacingRight = true;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
            animator.SetBool("isJumping", true);
        }

        horizontalInput = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 1.5f, rigidbodyComponent.velocity.y, 0);

        //Flip the object when turns left or right
        if (isFacingRight & horizontalInput < 0)
        {
            Flip();
        }

        if (!isFacingRight & horizontalInput > 0)
        {
            Flip();
        }

        //Check the velocity of the rigid body. If it's 0 then body is grounded
        if (rigidbodyComponent.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        { 
            return;
        }



        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 1.5f;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpKeyWasPressed = false;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
