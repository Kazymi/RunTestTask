using EventBusSystem;
using UnityEngine;

public class RewardItemTrigger : PlayerTriggered
{
    [SerializeField] private RewardType rewardType;


    public override void ActivateTrigger()
    {
        EventBus.RaiseEvent<IRewardRaised>(t => t.RewardRaised(rewardType));
        Destroy(gameObject);
    }
}