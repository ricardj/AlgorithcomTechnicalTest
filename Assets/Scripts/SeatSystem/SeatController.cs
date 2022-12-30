using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatController : MonoBehaviour
{

    [Header("References")]
    public Transform _seatPlaceholder;



    [Header("Debug values")]
    [SerializeField] List<ISeatable> _seatablesInbounds = new List<ISeatable>();
    [SerializeField] ISeatable _currentSeatable;




    public void OnTriggerEnter(Collider other)
    {
        GameObject targetGameObject = other.gameObject;
        ISeatable seatable = targetGameObject.GetComponentInChildren<ISeatable>();
        if (seatable != null)
        {
            AddSeatable(seatable);
        }
    }

    private void AddSeatable(ISeatable seatable)
    {
        seatable.OnTryActivateSeat.AddListener(ToogleActivateSeat);
        _seatablesInbounds.Add(seatable);
    }

    private void RemoveSeatable(ISeatable seatable)
    {
        seatable.OnTryActivateSeat.RemoveListener(ToogleActivateSeat);
        _seatablesInbounds.Remove(seatable);
    }

    private void ToogleActivateSeat(ISeatable seatable)
    {
        if (_currentSeatable == null)
        {
            _currentSeatable = seatable;
            _currentSeatable.SetSeatActivated();
            StartCoroutine(ActivateSeatSequence(seatable));
        }
        else
        {
            if (_currentSeatable == seatable)
            {
                _currentSeatable.SetSeatDeactivated();
                _currentSeatable = null;
            }
        }
    }


    public IEnumerator ActivateSeatSequence(ISeatable seatable)
    {
        float seatTime = 0.3f;
        seatable.transform.DOMove(_seatPlaceholder.position, seatTime);
        seatable.transform.DOLookAt(_seatPlaceholder.forward, seatTime, AxisConstraint.Y, Vector3.up);
        yield return new WaitForSeconds(seatTime);
        seatable.OnFinishedSeatPositioning.Invoke();
        
    }




   


    public void OnTriggerExit(Collider other)
    {
        GameObject targetGameObject = other.gameObject;
        ISeatable seatable = targetGameObject.GetComponentInChildren<ISeatable>();
        if (seatable != null && _seatablesInbounds.Contains(seatable))
        {
            RemoveSeatable(seatable);
        }
    }


}
