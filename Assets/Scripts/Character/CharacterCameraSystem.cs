using UnityEngine;

public class CharacterCameraSystem : MonoBehaviour
{
    public Camera MainCamera;
    public Transform TargetCharacter;

    public float dierection = 5f;
    public float caeraRotateSpeed = 200f;

    public float minDierection = 2f;
    public float maxDierection = 10f;
    public float zoomSpeed = 5f;

    public float minYAngle = -30f;
    public float maxYAngle = 60f;

    public LayerMask WallLay; 

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        if (MainCamera == null)
            MainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        HandleCursor();

        if (Input.GetKey(KeyCode.LeftAlt)) return;

        CameraRotate();
        CameraZoom();
        CameraPositionUpdate();
    }

    private void HandleCursor()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void CameraRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * caeraRotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * caeraRotateSpeed * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minYAngle, maxYAngle);
    }

    private void CameraZoom()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel != 0f)
        {
            dierection -= mouseWheel * zoomSpeed;
            dierection = Mathf.Clamp(dierection, minDierection, maxDierection);
        }
    }

    private void CameraPositionUpdate()
    {
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Vector3 desiredOffset = rotation * new Vector3(0f, 0f, -dierection);

        Vector3 targetPos = TargetCharacter.position;
        Vector3 desiredPosition = targetPos + desiredOffset;

        RaycastHit hit;
        if (Physics.Linecast(targetPos, desiredPosition, out hit, WallLay))
            MainCamera.transform.position = hit.point + hit.normal * 0.2f;
        else
            MainCamera.transform.position = desiredPosition;

        MainCamera.transform.LookAt(TargetCharacter.position);
    }
}