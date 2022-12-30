using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    [SerializeField] AvatarMovement _avatarMovement;
    [SerializeField] Animator _animator;
    [SerializeField] ISeatable _seatable;

    [Header("Trigger animatiosn")]
    [SerializeField] string _walk = "walk";
    [SerializeField] string _sit = "sit";
    [SerializeField] string _stand = "stand";


    public void Start()
    {
        _seatable.OnSeatActivated.AddListener(() => _avatarMovement.Deactivate());
        _seatable.OnSeatDeactivated.AddListener(() =>
        {
            SetIdleStand();
            _avatarMovement.Activate();
        });
        _seatable.OnFinishedSeatPositioning.AddListener(() => SetSit());

        _avatarMovement.OnStartMoving.AddListener(SetWalk);
        _avatarMovement.OnStopMoving.AddListener(SetIdleStand);
    }

    public void Update()
    {
        
    }


    public void SetWalk()
    {
        SetTrigger(_walk);
    }

    public void SetIdleStand()
    {
        SetTrigger(_stand);
    }

    public void SetSit()
    {
        SetTrigger(_sit);
    }
    public void SetTrigger(string targetTrigger)
    {
        _animator.SetTrigger(targetTrigger);
    }


}
