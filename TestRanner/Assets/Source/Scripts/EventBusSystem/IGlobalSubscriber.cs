namespace EventBusSystem
{
    public interface IGlobalSubscriber
    {
    }

    public interface IRewardRaised : IGlobalSubscriber
    {
        void RewardRaised(RewardType rewardType);
    }
}