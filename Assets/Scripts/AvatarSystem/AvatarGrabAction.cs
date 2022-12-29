using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AvatarGrabAction : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Rig _rig;
    [SerializeField] Picker _picker;
    [SerializeField] Transform _targetPlaceholder;
    [SerializeField] Transform _effectorTransform;

    [Header("Configuration")]
    [SerializeField] float _grabActivationTime = 0.3f;
    [SerializeField] float _grabTime = 1f;


    public void Start()
    {
        _picker.OnObjectPicked.AddListener(newPickable =>
        {
            _picker.Deactivate();
            newPickable.SetGrabbedBy(_picker.transform);
        });
    }


    public void ExecuteAction()
    {
        StartCoroutine(ExecuteActionSequence());
    }

    private IEnumerator ExecuteActionSequence()
    {
        DOTween.To(() => _rig.weight, x => _rig.weight = x, 1f, _grabActivationTime);
        yield return new WaitForSeconds(_grabActivationTime);
        _effectorTransform.transform.DOMove(_targetPlaceholder.position, _grabTime);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteAction();
        }
    }
}
