using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControll : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    public float gravityModifier = 10f;
    private float _currentSpeed;

    public LayerMask platformLayerMask;
    public bool jumping;

    public Joystick joystick;
    public Button button;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRender;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    
    private float moveHorizontal = 0f;

    [Header("For box colliders")]
    private Vector2 boxOffestOrigin;
    private Vector2 boxSizeOrigin;

    public Vector2 boxOffsetJump;
    public Vector2 boxSizeJump;    
    
    private void Awake()
    {
        button = GameObject.FindGameObjectWithTag("Button").GetComponent<Button>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        //button.onClick.AddListener(() => { Jump(); }) ;
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnJump((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.gravity *= gravityModifier; 
        spriteRender = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();        

        boxOffestOrigin = boxCollider2D.offset;
        boxSizeOrigin = boxCollider2D.size;

        _currentSpeed = speed;
    }

   
    void FixedUpdate()
    {
#if UNITY_EDITOR
        moveHorizontal = Input.GetAxis("Horizontal");
#else        
        if (GameManager.current.isAccel)
        {
            moveHorizontal = Input.acceleration.x;
            if (moveHorizontal >= .2f)
                moveHorizontal = 1f;
            else if (moveHorizontal <= -.2f)
                moveHorizontal = -1f;
            else
                moveHorizontal = 0f;
        }
        else if (!GameManager.current.isAccel)
        {
            moveHorizontal = joystick.Horizontal;
            if (joystick.Horizontal >= .2f)
                moveHorizontal = 1f;
            else if (joystick.Horizontal <= -.2f)
                moveHorizontal = -1f;
            else
                moveHorizontal = 0f;
        }        
#endif



        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb.AddForce(movement * speed);

        //animation
        if (IsGrounded())
        {
            //grounded = true;
            GameEvent.current.GroundEnter();
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
            animator.SetBool("Ground", true);

            boxCollider2D.size = boxSizeOrigin;
            boxCollider2D.offset = boxOffestOrigin;
        }
        else if(!jumping)
        {
            //grounded = false;
            GameEvent.current.GroundExit();
            animator.SetBool("Ground", false);
        }
        //
        
        if (moveHorizontal < 0 && !spriteRender.flipX)
        {
            spriteRender.flipX = !spriteRender.flipX;
        }
        else if(moveHorizontal > 0 && spriteRender.flipX)
        {
            spriteRender.flipX = !spriteRender.flipX;
        }

        if (GameManager.current.endGame == true)
        {
            animator.SetBool("Death", true);
            speed = 0f;
        }
        else
        {
            animator.SetBool("Death", false);
            speed = _currentSpeed;
        }
    }
public void OnJump(PointerEventData data)
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("Ground", false);
            jumping = true;

            boxCollider2D.size = boxSizeJump;
            boxCollider2D.offset = boxOffsetJump;
        }
    }
    private bool IsGrounded()
    {
        float extraHeighText = .05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeighText, platformLayerMask);        
        return raycastHit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Parallel"))
        {
            jumping = false;                        
        }
        
    }    
}
