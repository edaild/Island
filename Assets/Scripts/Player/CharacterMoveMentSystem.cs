using System.Collections;
using UnityEngine;

public class CharacterMoveMentSystem : MonoBehaviour
{
    public float character_Speed = 3f;
    public float character_RunSpeed = 5f;
    public float character_RotateSpeed = 10f;
    public float character_Jumpforce = 7f;

    public bool isWalk;
    public bool isRun;
    public bool isJump;
    public bool isGound;

    public Rigidbody rb;
    public Animator characterAnimator;

    public CharacterCameraSystem _characterCameraSystem;
    public CharacterHelthandSteminaSystem _characterHelthandSteminaSystem;
    // public GameManager gameManger;
   


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _characterHelthandSteminaSystem = GetComponent<CharacterHelthandSteminaSystem>();
        _characterCameraSystem = Object.FindAnyObjectByType<CharacterCameraSystem>();
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_characterHelthandSteminaSystem.isPlayerDie) return;
        Move();
        Jump();
    }

    private void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(Horizontal, 0f, Vertical).normalized;

        isWalk = inputDir.magnitude > 0f;
        isRun = Input.GetKey(KeyCode.LeftShift) && isWalk;

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

        if (Derection != Vector3.zero)
        {
            if (isRun) 
            {
                _characterHelthandSteminaSystem.MinusStenima(100);
                characterAnimator.SetBool("IsRun", true); 
                characterAnimator.SetBool("IsWalk", false); 
                Debug.Log("달리기 애니매이션 실행"); 
            }

            else { characterAnimator.SetBool("IsWalk", true); characterAnimator.SetBool("IsRun", false); Debug.Log("걷기 애니매이션 실행"); }

            Quaternion targetRotation = Quaternion.LookRotation(Derection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, character_RotateSpeed * Time.deltaTime);
        }
        else
        {
            characterAnimator.SetBool("IsRun", false);
            characterAnimator.SetBool("IsWalk", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGound)
        {
            isJump = true;
            isGound = false;
            rb.AddForce(Vector3.up * character_Jumpforce, ForceMode.Impulse);
            StartCoroutine(JumpTime());
          
        }
    }

    IEnumerator JumpTime()
    {
        characterAnimator.SetBool("IsJump", true);
        Debug.Log("점프 실행");
        yield return new WaitForSeconds(1.5f);
        characterAnimator.SetBool("IsJump", false);
        Debug.Log("점프 종료");
    }
}
