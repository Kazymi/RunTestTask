using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using UnityEngine;

public class DoorTrigger : PlayerTriggered
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private float minValue;

    public override void ActivateTrigger()
    {
        var rewardPlayer = FindObjectOfType<PlayerReward>();
        rewardPlayer.SetCanvasStatus(false);
        if (rewardPlayer.CurrentValue >= minValue)
        {
            var doorAnimationName = "Open";
            doorAnimator.SetBool(doorAnimationName, true);
        }
        else
        {
            EventBus.RaiseEvent<IToResultScreen>(t => t.Finish(true));
        }
    }
}