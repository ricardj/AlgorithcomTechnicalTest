using UnityEngine;

public class Picker : MonoBehaviour
{
    public PickableEvent OnObjectPicked;

    public void OnTriggerEnter(Collider other)
    {

        TryPickObject(other.gameObject);
    }

    private void TryPickObject(GameObject targetObject)
    {
        IPickable pickable = targetObject.GetComponentInChildren<IPickable>();
        if (pickable != null)
        {
            OnObjectPicked.Invoke(pickable);
        }
    }
}