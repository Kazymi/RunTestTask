using DG.Tweening;
using UnityEngine;

public class EnterNewZoneTrigger : PlayerTriggered
{
    public override void ActivateTrigger()
    {
        var player = FindObjectOfType<PlayerStateMachine>().transform;
        player.transform.parent = transform;
        player.transform.DOLocalRotate(Vector3.zero, 0.8f)
            .OnComplete(() => player.transform.localRotation = Quaternion.identity);
    }
}