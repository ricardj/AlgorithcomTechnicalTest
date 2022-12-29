using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AvatarGrabAction : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Rig _rig;
    [SerializeField] Transform _targetPlaceholder;
    [SerializeField] Transform _effectorTransform;

    [Header("Configuration")]
    [SerializeField] float _grabActivationTime = 0.3f;



    public void ExecuteAction()
    {
        StartCoroutine(ExecuteActionSequence());
    }

    private IEnumerator ExecuteActionSequence()
    {
        DOTween.To(() => _rig.weight, x => _rig.weight = x, 1f, _grabActivationTime);
        yield return new WaitForSeconds(_grabActivationTime);
        _effectorTransform.transform.DOMove(_targetPlaceholder.position, 0.3f);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteAction();
        }
    }
}
