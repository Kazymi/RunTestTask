using DG.Tweening;
using UnityEngine;

public class UpDownTweener : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private Vector3 upValue = new Vector3(0, 0.3f, 0);
    [SerializeField] private float timeCycle;

    private void Start()
    {
        var startPosition = transform.localPosition;
        var endPosition = transform.localPosition + upValue;
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(endPosition, timeCycle / 2).SetEase(ease));
        sequence.Append(transform.DOLocalMove(startPosition, timeCycle / 2).SetEase(ease));
        sequence.SetLoops(-1);
    }
}