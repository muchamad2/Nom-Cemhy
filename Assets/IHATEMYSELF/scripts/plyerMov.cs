using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plyerMov : MonoBehaviour
{
    [Range(10,20)]
    public float moveSpeed;
    private float movInput;
    private bool facingRight = true;
    [SerializeField] private float fallMultiplier;
    [Range(1,2)]
    [SerializeField] private float lowJumpMultiplier;
    [Range(10,20)]
    public float jumpVelocity;
    public bool isGrounded = false;
    public Transform clampMin;
    public Transform clampMax;
    private Rigidbody2D rb;
    private readonly string horizonMov = "Horizontal";

    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        movInput = Input.GetAxis(horizonMov);

        InputHandler();
    }
    private void LateUpdate() {
        clampCharacter();
    }

    void clampCharacter()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampMin.position.x, clampMax.position.x),
                                          transform.position.y, transform.position.z);
    }

    void InputHandler()
    {
        if (movInput != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else { anim.SetBool("isWalk", false); }


        if (Input.GetKey(KeyCode.Z) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move() {
        Vector3 mov = new Vector3(movInput, 0f, 0f);

        transform.position += mov * Time.deltaTime * moveSpeed;
        
        if(facingRight == false && movInput > 0) {
            Flip();
        } else if (facingRight == true && movInput < 0) {
            Flip();
        }
    }

    void Jump() {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Z)) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }    
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
