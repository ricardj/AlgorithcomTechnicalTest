using System;
using UnityEngine;

public class Picker : MonoBehaviour
{


    [Header("Debug values")]

    [SerializeField] IPickable _currentPickedObject = null;




    public void TryPickObject(GameObject targetObject)
    {
        IPickable pickable = targetObject.GetComponentInChildren<IPickable>();
        if (pickable != null)
        {
            SetPickObject(pickable);
        }
    }

    public void SetPickObject(IPickable pickable)
    {
        _currentPickedObject = pickable;
    }

    public bool HasGrabbedSomething()
    {
        return _currentPickedObject != null;
    }

    public void SetEmpty()
    {
        if (_currentPickedObject != null)
        {
            _currentPickedObject.SetNotGrabbed();
            _currentPickedObject = null;
        }
    }
}