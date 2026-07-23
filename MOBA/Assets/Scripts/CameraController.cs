using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float sprintMultiplier = 2f;
    
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
        // Получаем ввод
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // ВАЖНО: направление движения зависит от поворота КАМЕРЫ
        // Forward и Right камеры, но спроецированные на плоскость XZ для горизонтального движения
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        
        // Если камера смотрит вниз/вверх, forward имеет вертикальную компоненту
        // Мы используем ее для движения вниз/вверх при нажатии W/S
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        
        // Если есть движение
        if (moveDirection != Vector3.zero)
        {
            float currentSpeed = moveSpeed;
            if (Input.GetKey(KeyCode.LeftControl))
                currentSpeed *= sprintMultiplier;
            
            transform.position += moveDirection * currentSpeed * Time.deltaTime;
        }
    }
    
    private void HandleRotationInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
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