using UnityEngine;

public class FPS_PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField, Range(1, 5)] private float moveSpeed;

    [SerializeField, Range(20, 360)] private float rotationSpeed;
    
    [SerializeField] private float rotationAngleLimit = 40f;

    [SerializeField] private float jumpForce, gravity, verticalVelocity, terminalVelocity, groundVelocity = -1f;

    [SerializeField] private int jumpBufferFrames;

    [SerializeField] private bool isXInverted, isYInverted;

    private int invertedX => isXInverted ? -1 : 1;
    private int invertedY => isYInverted ? 1 : -1;

    private int _jumpBufferFrameCount = 0;

    private bool _isJumping, _jumpRequested;

    private CharacterController _cc;

    private Vector3 _playerCamRotation;
    private Vector2 _cameraRotation;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        _playerCamRotation = Vector3.zero;
        _cameraRotation = Vector2.zero;
    }

    private void Update()
    {
        ApplyJump();

        var finalMoveDirection = GetMovementFinalDirection();
        
        _cc.Move(finalMoveDirection);

        ApplyRotation();
    }

    private void ApplyJump()
    {
        _jumpRequested = _jumpRequested && !(_jumpBufferFrameCount++ >= jumpBufferFrames);
        if (!_jumpRequested) _jumpBufferFrameCount = 0;

        if (!_cc.isGrounded)
        {
            ApplyGravity();
        }
        else
        {
            if(_jumpRequested && !_isJumping)
            {
                _jumpRequested = false;
                _jumpBufferFrameCount = 0;
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity = groundVelocity;
            }
        }
            
    }

    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity <= -terminalVelocity)
            verticalVelocity = -terminalVelocity;
    }

    private Vector3 GetMovementFinalDirection()
    {
        var inputMoveDirection = FPS_InputManager.Instance.GetInputMoveValue();

        var moveDirection =
            inputMoveDirection.x * transform.right +
            inputMoveDirection.y * transform.forward;

        if (moveDirection.sqrMagnitude > 1) moveDirection.Normalize();

        return moveDirection * (moveSpeed * Time.deltaTime) + Vector3.up * (verticalVelocity * Time.deltaTime);
    }

    private void ApplyRotation()
    {
        var inputLook = FPS_InputManager.Instance.GetInputLookValue();

        _cameraRotation.x = invertedY * inputLook.y;
        _cameraRotation.y = invertedX * inputLook.x;

        var cameraRotationVector = new Vector3(_cameraRotation.x, 0, 0) * (rotationSpeed * Time.deltaTime);

        cameraTransform.Rotate(cameraRotationVector);

        var angleTransformation = cameraTransform.eulerAngles.x > 180 ? cameraTransform.localEulerAngles.x - 360 : cameraTransform.localEulerAngles.x;

        cameraRotationVector = new Vector3(Mathf.Clamp(angleTransformation, -rotationAngleLimit, rotationAngleLimit), 0f, 0f);

        cameraTransform.localEulerAngles = cameraRotationVector;

        _playerCamRotation.y = _cameraRotation.y;
        transform.Rotate(_playerCamRotation * (rotationSpeed * Time.deltaTime));
    }

    private void OnEnable()
    {
        FPS_InputManager.Instance.Jump += () => _jumpRequested = true;
    }

    private void OnDisable()
    {
        FPS_InputManager.Instance.Jump -= () => _jumpRequested = true;
    }
}
