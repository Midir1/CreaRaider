using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Character Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sideJumpForce;
    [SerializeField] private float fallSpeed;
    
    [Header("Raycast Parameters")]
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastMaxDistance;

    private Rigidbody _rb;

    private Vector3 _rotationVelocity;

    private bool _isGrounded;

    private const int LayerMask = 1 << 6;

    private const string HorizontalInput = "Horizontal", VerticalInput = "Vertical";
    private const string MouseInput = "Mouse X";
    private const string JumpInput = "Jump";

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rotationVelocity = new Vector3(0f, rotationSpeed, 0f);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw(HorizontalInput) * movementSpeed;
        float vertical = Input.GetAxisRaw(VerticalInput) * movementSpeed;
        
        float mouseAxis = Input.GetAxisRaw(MouseInput);
        float jumpAxis = Input.GetAxisRaw(JumpInput);
        
        CharacterMovement(horizontal, vertical);
        CharacterRotation(mouseAxis);
        CharacterJump(jumpAxis, horizontal, vertical);
        CharacterFall();
        
        CheckCharacterGrounded();
    }

    private void CharacterMovement(float horizontal, float vertical)
    {
        _rb.AddForce(transform.forward * vertical + transform.right * horizontal, ForceMode.Force);
    }

    private void CharacterRotation(float mouseAxis)
    {
        Quaternion deltaRotation = Quaternion.Euler(_rotationVelocity * (Time.fixedDeltaTime * mouseAxis));
        
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    }

    private void CharacterJump(float jumpAxis, float horizontal, float vertical)
    {
        if (_isGrounded && jumpAxis != 0f) 
            _rb.AddForce(transform.up * (jumpForce * jumpAxis) + (transform.forward * (sideJumpForce * vertical) + transform.right * (sideJumpForce * horizontal)).normalized, ForceMode.Impulse);
    }

    private void CharacterFall()
    {
        if(!_isGrounded) _rb.velocity -= new Vector3(0f, fallSpeed, 0f);
    }
    
    private void CheckCharacterGrounded()
    {
        _isGrounded = Physics.Raycast(raycastOrigin.position, Vector3.down, out _, raycastMaxDistance, LayerMask);
    }
}