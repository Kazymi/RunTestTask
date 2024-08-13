using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateAroundTweener : MonoBehaviour
{
    [SerializeField] private Vector3 rotate = new Vector3(0, 360, 0);
    [SerializeField] private float time = 2;

    private void Start()
    {
        transform.DOLocalRotate(rotate, time, RotateMode.FastBeyond360).SetRelative(true)
            .SetEase(Ease.Linear).SetLoops(-1);
    }
}