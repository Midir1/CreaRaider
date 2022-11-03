using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Character Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody _rb;

    private Vector3 _rotationVelocity;

    private const string HorizontalInput = "Horizontal", VerticalInput = "Vertical";
    private const string MouseInput = "Mouse X";

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rotationVelocity = new Vector3(0f, rotationSpeed, 0f);
    }

    private void FixedUpdate()
    {
        CharacterMovement();
        CharacterRotation();
    }

    private void CharacterMovement()
    {
        float horizontal = Input.GetAxisRaw(HorizontalInput) * movementSpeed;
        float vertical = Input.GetAxisRaw(VerticalInput) * movementSpeed;
        
        _rb.AddForce(transform.forward * vertical + transform.right * horizontal, ForceMode.Force);
    }

    private void CharacterRotation()
    {
        float mouseAxis = Input.GetAxisRaw(MouseInput);
        Quaternion deltaRotation = Quaternion.Euler(_rotationVelocity * (Time.fixedDeltaTime * mouseAxis));
        
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    }
}