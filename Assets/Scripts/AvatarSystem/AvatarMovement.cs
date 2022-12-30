using UnityEngine;
using UnityEngine.Events;

public class AvatarMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController _characterController;
    [SerializeField] Transform cam;
    [SerializeField] float _speed = 6f;
    [SerializeField] float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;
    bool _isMoving = false;


    [Header("Debug values")]
    [SerializeField] bool _isMovementActivated = true;

    public UnityEvent OnStartMoving;
    public UnityEvent OnStopMoving;



    public void Update()
    {
        if (_isMovementActivated)
        {

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _characterController.Move(moveDir.normalized * _speed * Time.deltaTime);

                if (!_isMoving)
                {
                    _isMoving = true;
                    OnStartMoving.Invoke();
                }
            }else
            {
                if (_isMoving)
                {
                    _isMoving = false;
                    OnStopMoving.Invoke();
                }
            }
        }
    }

    public void Activate()
    {
        _isMovementActivated = true;
    }

    public void Deactivate()
    {
        _isMovementActivated = false;
    }
}