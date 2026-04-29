using UnityEngine;

public class CharacterMoveMentSystem : MonoBehaviour
{
    public float character_Speed = 3f;
    public float character_RunSpeed = 5f;
    public float character_RotateSpeed = 10f;
    public float character_Jumpforce = 7f;

    public bool isMove;
    public bool isRun;
    public bool isJump;
    public bool isGound;

    public Rigidbody rb;
    public CharacterCameraSystem _characterCameraSystem;
    // public GameManager gameManger;
   


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _characterCameraSystem = Object.FindAnyObjectByType<CharacterCameraSystem>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        isMove = inputDir.magnitude > 0f;
        isRun = Input.GetKey(KeyCode.LeftShift) && isMove;

        if (!isMove) return;

        Vector3 cameraForward = _characterCameraSystem.MainCamera.transform.forward;
        Vector3 cameraRight = _characterCameraSystem.MainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDir = cameraForward * v + cameraRight * h;
        moveDir.Normalize();

        float currentSpeed = isRun ? character_RunSpeed : character_Speed;

        Vector3 moveVelocity = moveDir * currentSpeed;
        moveVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = moveVelocity;

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            character_RotateSpeed * Time.deltaTime
        );
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGound)
        {
            isJump = true;
            isGound = false;

            rb.AddForce(Vector3.up * character_Jumpforce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGound = true;
            isJump = false;
        }
    }
}
