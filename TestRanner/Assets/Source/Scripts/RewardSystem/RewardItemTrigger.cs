using EventBusSystem;
using UnityEngine;

public class RewardItemTrigger : PlayerTriggered
{
    [SerializeField] private RewardType rewardType;
    [SerializeField] private float value = 0.03f;

    public override void ActivateTrigger()
    {
        EventBus.RaiseEvent<IRewardRaised>(t => t.RewardRaised(rewardType,value));
        Destroy(gameObject);
    }
}