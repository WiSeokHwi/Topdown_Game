using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSCRT : MonoBehaviour
{
    Rigidbody2D rigid;
    public float Speed = 30;
    private string lastInput = "";
    public Vector2 moveDirection;
    Animator animator;
    Vector3 dirVec;
    GameObject scanObject;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (!right && !left && !up && !down)
        {
            lastInput = ""; // ��� Ű�� ���� �Է� �ʱ�ȭ
        }
        else
        {
            // ���� �������� ���� ���� ����
            if (right) lastInput = "right";
            if (left) lastInput = "left";
            if (up) lastInput = "up";
            if (down) lastInput = "down";
        }


        // �밢�� �̵� ���� (������ �Է� ������ �켱 ó��)
        switch (lastInput)
        {
            case "right": moveDirection = new Vector2(1, 0); break;
            case "left": moveDirection = new Vector2(-1, 0); break;
            case "up": moveDirection = new Vector2(0, 1); break;
            case "down": moveDirection = new Vector2(0, -1); break;
            default: moveDirection = Vector2.zero; break;
        }

        //�ִϸ��̼�
        if (animator.GetInteger("hAxisRaw")!= moveDirection.x)
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

        if(lastInput == "right" && moveDirection.x == 1)
        {
            dirVec = Vector3.right;
        }
        if(lastInput == "left" && moveDirection.x == -1)
        {
            dirVec = Vector3.left;
        }
        if(lastInput == "up" && moveDirection.y == 1)
        {
            dirVec = Vector3.up;
        }
        if (lastInput == "down" && moveDirection.y == -1)
        {
            dirVec = Vector3.down;
        }
        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log("This is :" + scanObject.name);
        }
    }

    private void FixedUpdate()
    {
        rigid.linearVelocity = moveDirection * Speed *Time.deltaTime;

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
}
