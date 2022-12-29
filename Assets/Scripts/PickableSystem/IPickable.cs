using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPickable : MonoBehaviour
{

    public void SetGrabbedBy(Transform _targetTransform)
    {
        transform.SetParent(_targetTransform);
        transform.DOLocalMove(Vector3.zero, 0.3f);
    }
}
