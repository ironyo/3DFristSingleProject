using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField]  private float sensitivity = 100f;
    [SerializeField] private float clampAngle = 70f;

    private float rotX;
    private float rotY;
    private Vector2 lookInput;

    [SerializeField] private Transform realCamera;
    [SerializeField] private Vector3 dirNormarized;
    [SerializeField] private Vector3 finalDir;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float finalDistance;
    [SerializeField] private float smoothness = 10;

    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;        
        rotY = transform.localRotation.eulerAngles.y;

        dirNormarized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


    void Update()
    {
        HandleLook();
    }
    private void LateUpdate()
    {
        HitLook();
    }


    private void HandleLook()
    {
        rotX += lookInput.y * sensitivity * Time.deltaTime;
        rotY += lookInput.x * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
    private void HitLook()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormarized * maxDistance);

        RaycastHit hit;
        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormarized * finalDistance, Time.deltaTime * smoothness);
    }
}
