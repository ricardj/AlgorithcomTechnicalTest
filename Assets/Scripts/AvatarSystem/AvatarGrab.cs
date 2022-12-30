using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;

public class AvatarGrab : MonoBehaviour
{
    [SerializeField] Picker _currentPicker;
    [SerializeField] Transform _pickerHandler;
    [SerializeField] Rig _rig;
    [SerializeField] float _pickingRadius = 1f;

    [Header("Configuration")]
    [SerializeField] float _grabActivationTime = 1f;
    [SerializeField] float _grabTime = 1f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryGrabSomething();
        }
    }

    private void TryGrabSomething()
    {
        if (!_currentPicker.HasGrabbedSomething())
        {

            TryPickSomething();
        }
        else
        {
            ThrowPickedObject();
        }
    }

    private void ThrowPickedObject()
    {

        _currentPicker.SetEmpty();
        DOTween.To(() => _rig.weight, x => _rig.weight = x, 0f, _grabActivationTime);
    }

    private void TryPickSomething()
    {
        Collider[] results = Physics.OverlapSphere(_currentPicker.transform.position, _pickingRadius);
        List<IPickable> pickables = new List<IPickable>();
        results.ToList().ForEach(result =>
        {
            IPickable pickable = result.GetComponentInChildren<IPickable>();
            if (pickable != null)
            {
                pickables.Add(pickable);
            }
        });

        if (pickables.Count > 0)
        {

            pickables.OrderBy(pickable => Vector3.Distance(pickable.transform.position, _currentPicker.transform.position));

            IPickable targetPickable = pickables[0];
            Vector3 transformedPosition = _pickerHandler.transform.parent.InverseTransformPoint(targetPickable.transform.position);
            _pickerHandler.transform.DOLocalMove(transformedPosition, _grabTime).SetLoops(2, LoopType.Yoyo);
            DOTween.To(() => _rig.weight, x => _rig.weight = x, 1f, _grabActivationTime).OnComplete(() =>
            {
                _currentPicker.SetPickObject(targetPickable);
                targetPickable.SetGrabbedBy(_currentPicker.transform);
            });

        }
        else
        {

            ThrowPickedObject();
        }
    }
}
