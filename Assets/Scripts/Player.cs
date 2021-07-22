using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    [Range(0, 100)]
    public float recoilForce = 50f;
    public Transform fireDirection;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private bool superJumpKeyWasPressed;
    private float normalJumpPower = 5.0f;
    private float epsilon = 0.0001f;

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
        InputMoveLogic();
    }

    public void PlayerRecoil()
    {
        rigidbodyComponent.AddForce(-fireDirection.right * recoilForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputMoveLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
            animator.SetBool("isJumping", true);
        }

        //super jump release
        if (Input.GetKeyDown(KeyCode.F))
        {
            superJumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    private void Move()
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
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        isGrounded = Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length != 0;

        if (!isGrounded)
        {
            return;
        }

        //normal jump check
        if (jumpKeyWasPressed)
        {
            MakeJump(normalJumpPower, ref jumpKeyWasPressed);
            Debug.Log(jumpKeyWasPressed);
        }

        //super jump check
        if (superJumpKeyWasPressed)
        {
            if (superJumpsRemaining > 0)
            {
                MakeJump(normalJumpPower * 1.5f, ref superJumpKeyWasPressed);
                superJumpsRemaining--;
            }
        }
    }

    private void MakeJump(float jumpPower, ref bool jumpKeyWasPressed)
    {
        rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        jumpKeyWasPressed = false;
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
