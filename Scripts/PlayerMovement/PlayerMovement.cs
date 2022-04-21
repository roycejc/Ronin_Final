using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float counter;

    [Header("Multi-Jumps")]
    [SerializeField] private int multiJumps;
    private int jmpCntr;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSnd;
    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCD;
    private float horizontalInput;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip Avatar Left
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        //Flip Avatar Right
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 2;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                counter = coyoteTime;
                jmpCntr = multiJumps;
            }
            else
            {
                counter -= Time.deltaTime;
            }
            
        }
     }

    private void Jump()
    {
        if (counter < 0 && !onWall() && jmpCntr <=0) return;
        SoundManager.instance.PlaySound(jumpSnd);

        if (onWall())
        {
            WallJump();
        }
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpStrength);
            else
            {
                if (counter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpStrength);
                else
                {
                    if (jmpCntr > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpStrength);
                        jmpCntr--;
                    }
                }
            }
            counter = 0;          
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCD = 0;
    }

      private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    { 
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x , 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
