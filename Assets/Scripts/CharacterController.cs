using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform characterSpawn;
    
    [Header("Character Parameters")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallSpeed;
    
    [Header("Raycast Parameters")]
    [SerializeField] private Transform raycastOrigin1;
    [SerializeField] private Transform raycastOrigin2;
    [SerializeField] private float raycastMaxDistance;

    private Rigidbody _rb;

    private Vector3 _rotationVelocity;

    private bool _isGrounded;

    private float _timerInAir;

    private const int LayerMask = 1 << 6; // Ground Layer

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
        CharacterJump(jumpAxis);
        CharacterFall();
        
        CheckCharacterGrounded();
    }

    public void RespawnPlayer()
    {
        transform.position = characterSpawn.position;
        transform.rotation = characterSpawn.rotation;
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

    private void CharacterJump(float jumpAxis)
    {
        if(_isGrounded && jumpAxis != 0f) _rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }

    private void CharacterFall()
    {
        switch (_isGrounded)
        {
            case false:
                _timerInAir += Time.fixedDeltaTime * 5f;
                _rb.velocity -= new Vector3(0f, fallSpeed * _timerInAir, 0f);
                break;
            default:
                _timerInAir = 0f;
                break;
        }
    }
    
    private void CheckCharacterGrounded()
    {
        _isGrounded = Physics.Raycast(raycastOrigin1.position, Vector3.down, out _, raycastMaxDistance, LayerMask)
            || Physics.Raycast(raycastOrigin2.position, Vector3.down, out _, raycastMaxDistance, LayerMask);
    }
}