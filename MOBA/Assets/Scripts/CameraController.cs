using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sprintMultiplier = 2f; // Во сколько раз увеличивается скорость
    
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;
    
    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    
    private Vector3 rotationAngles;
    private bool isRotating;
    
    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponentInChildren<Camera>().transform;
            if (cameraTransform == null)
                cameraTransform = transform;
        }
        
        rotationAngles = cameraTransform.localEulerAngles;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleRotationInput();
        HandleRotation();
    }
    
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        
        if (moveDirection != Vector3.zero)
        {
            // Базовая скорость
            float currentSpeed = moveSpeed;
            
            // Если зажат Shift - увеличиваем скорость
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed *= sprintMultiplier;
            }
            
            transform.position += moveDirection * currentSpeed * Time.deltaTime;
        }
    }
    
    private void HandleRotationInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    private void HandleRotation()
    {
        if (!isRotating) return;
        
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        
        rotationAngles.y += mouseX;
        rotationAngles.x -= mouseY;
        rotationAngles.x = Mathf.Clamp(rotationAngles.x, minVerticalAngle, maxVerticalAngle);
        
        cameraTransform.localEulerAngles = rotationAngles;
    }
}