using UnityEngine;
using UnityEngine.Events;

public class ISeatable : MonoBehaviour
{

    [Header("Debug values")]
    [SerializeField] bool _isSeated;

    [Header("Events")]
    public UnityEvent OnSeatActivated;
    public UnityEvent OnSeatDeactivated;
    public UnityEvent OnFinishedSeatPositioning;
    public ISeatableEvent OnTryActivateSeat;


    public void TryActivateSeat()
    {
        OnTryActivateSeat.Invoke(this);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryActivateSeat();
        }
    }


    public void SetSeatActivated()
    {
        _isSeated = true;
        OnSeatActivated.Invoke();
    }

    public void SetSeatDeactivated()
    {
        OnSeatDeactivated.Invoke();
    }
}