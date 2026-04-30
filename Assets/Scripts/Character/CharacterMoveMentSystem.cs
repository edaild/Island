using System.Collections;
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
    public Animator characterAnimator;

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
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(Horizontal, 0f, Vertical).normalized;

        isMove = inputDir.magnitude > 0f;
        isRun = Input.GetKey(KeyCode.LeftShift) && isMove;

        if (!isMove) return;

        Vector3 cameraForward = _characterCameraSystem.MainCamera.transform.forward;
        Vector3 cameraRight = _characterCameraSystem.MainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 Derection = cameraForward * Vertical + cameraRight * Horizontal;
        Derection.Normalize();

        float currentSpeed = isRun ? character_RunSpeed : character_Speed;

        Vector3 moveVelocity = Derection * currentSpeed;
        moveVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = moveVelocity;

        if(Derection != Vector3.zero)
        {
            if (isRun)
            {
                //characterAnimator.SetBool("Run", true);
                Debug.Log("달리기 애니매이션 실행");
            }
            else
            {
                //characterAnimator.SetBool("IsMove", true);
                Debug.Log("걷기 애니매이션 실행");
            }
           

            Quaternion targetRotation = Quaternion.LookRotation(Derection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                character_RotateSpeed * Time.deltaTime
            );
        }
        else
        {
            //characterAnimator.SetBool("Run", false);
            //characterAnimator.SetBool("IsMove", false);
        }

       
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGound)
        {
            isJump = true;
            isGound = false;
            rb.AddForce(Vector3.up * character_Jumpforce, ForceMode.Impulse);
            JumpTime();
        }
    }

    IEnumerator JumpTime()
    {
        //characterAnimator.SetBool("Jump", true);
        Debug.Log("점프 실행");
        yield return new WaitForSeconds(1f);
        //characterAnimator.SetBool("Jump", false);
        Debug.Log("점프 종료");
    }
}
