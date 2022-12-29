using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    [SerializeField] CharacterController _characterController;
    [SerializeField] Transform cam;
    [SerializeField] float _speed = 6f;
    [SerializeField] float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;
    [SerializeField] Animator _animator;

    [Header("Trigger animatiosn")]
    [SerializeField] string _walk = "walk";
    [SerializeField] string _sit = "sit";
    [SerializeField] string _stand = "stand";


    public void SetWalk()
    {
        SetTrigger(_walk);
    }

    public void SetIdle()
    {

    }
    public void SetTrigger(string targetTrigger)
    {
        _animator.SetTrigger(targetTrigger);
    }

    public void Update()
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

        }
    }
}
