using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSCRT : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed = 20;
    private string lastInput = "";
    public Vector2 moveDirection;
    Animator animator;
    Vector3 dirVec;
    GameObject scanObject;
    public GameManager manager;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        //bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        //bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        //bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        ////switch (Input.GetAxisRaw("Horizontal"))
        ////{
        ////    case 
        ////}
            


        //if (!right && !left && !up && !down)
        //{
        //    lastInput = ""; // 모든 키를 떼면 입력 초기화
        //}
        //else
        //{
        //    // 가장 마지막에 누른 방향 저장
        //    if (right) lastInput = "right";
        //    if (left) lastInput = "left";
        //    if (up) lastInput = "up";
        //    if (down) lastInput = "down";
        //}


        //// 대각선 이동 방지 (마지막 입력 방향을 우선 처리)
        //switch (lastInput)
        //{
        //    case "right": moveDirection = new Vector2(1, 0); break;
        //    case "left": moveDirection = new Vector2(-1, 0); break;
        //    case "up": moveDirection = new Vector2(0, 1); break;
        //    case "down": moveDirection = new Vector2(0, -1); break;
        //    default: moveDirection = Vector2.zero; break;
        //}

        ////애니매이션
        //if (animator.GetInteger("hAxisRaw")!= moveDirection.x)
        //{
        //    animator.SetBool("isChange", true);
        //    animator.SetInteger("hAxisRaw", (int)moveDirection.x);
        //}
        //else if (animator.GetInteger("vAxisRaw") != moveDirection.y)
        //{
        //    animator.SetBool("isChange", true);
        //    animator.SetInteger("vAxisRaw", (int)moveDirection.y);
        //}
        //else
        //{
        //    animator.SetBool("isChange", false);
        //}

        //if(lastInput == "right" && moveDirection.x == 1)
        //{
        //    dirVec = Vector3.right;
        //}
        //if(lastInput == "left" && moveDirection.x == -1)
        //{
        //    dirVec = Vector3.left;
        //}
        //if(lastInput == "up" && moveDirection.y == 1)
        //{
        //    dirVec = Vector3.up;
        //}
        //if (lastInput == "down" && moveDirection.y == -1)
        //{
        //    dirVec = Vector3.down;
        //}
        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }

    private void FixedUpdate()
    {
        //rigid.linearVelocity = moveDirection * Speed *Time.deltaTime;
        HandleMovement();
        //RAY
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayhit.collider != null)
        {
            scanObject = rayhit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A, D, ←, →
        float moveZ = Input.GetAxisRaw("Vertical");   // W, S, ↑, ↓

        Vector2 moveDirection = new Vector2(moveX, moveZ).normalized;

        if (moveDirection != Vector2.zero)
        {
            Move(moveDirection);
            MoveAnim(moveDirection);
        }
    }
    void Move(Vector2 direction)
    {
        rigid.linearVelocity += direction * speed * Time.deltaTime;
    }
    void MoveAnim(Vector2 direction)
    {
        //애니매이션
        if (animator.GetInteger("hAxisRaw") != moveDirection.x)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("hAxisRaw", (int)moveDirection.x);
        }
        else if (animator.GetInteger("vAxisRaw") != moveDirection.y)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)moveDirection.y);
        }
        else
        {
            animator.SetBool("isChange", false);
        }
    }
}
