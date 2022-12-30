using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPickable : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Collider _collider;



    public void SetGrabbedBy(Transform _targetTransform)
    {
        transform.SetParent(_targetTransform);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    internal void SetNotGrabbed()
    {
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
        _collider.enabled = true;
    }
}
