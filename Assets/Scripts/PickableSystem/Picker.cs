using System;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public PickableEvent OnObjectPicked;

    [Header("Debug values")]
    [SerializeField] bool _isActivated = true;

    public void OnTriggerEnter(Collider other)
    {

        TryPickObject(other.gameObject);
    }

    public void Deactivate()
    {
        _isActivated = false;
    }

    public void Activate()
    {
        _isActivated = true;
    }

    private void TryPickObject(GameObject targetObject)
    {
        if (_isActivated)
        {
            IPickable pickable = targetObject.GetComponentInChildren<IPickable>();
            if (pickable != null)
            {

                OnObjectPicked.Invoke(pickable);
            }
        }
    }
}