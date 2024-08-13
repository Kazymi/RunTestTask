using DG.Tweening;
using EventBusSystem;
using UnityEngine;

[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerReward : MonoBehaviour, IRewardRaised, IToResultScreen
{
    [SerializeField] private PlayerModelConfiguration[] playerModelConfigurations;
    [SerializeField] private PlayerStateStorage playerStateStorage;
    [SerializeField] private PlayerRewardUI playerRewardUi;
    [SerializeField] private Transform playerRotateObject;

    private float currentValue = 0.25f;
    private PlayerStateMachine stateMachine;
    private PlayerStateConfiguration currentStateConfiguration;

    public float CurrentValue => currentValue;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Awake()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Start()
    {
        var currentPlayerStateConfig = GetCurrentPlayerStateConfiguration();
        playerRewardUi.SetAllValue(currentValue, currentPlayerStateConfig.Description, currentPlayerStateConfig.Color);
        currentStateConfiguration = currentPlayerStateConfig;
        UpdateModelAndAnimation(true);
    }

    public void SetCanvasStatus(bool value)
    {
        playerRewardUi.SetCanvasStatus(value);
    }

    private void UpdateCurrentStateConfiguration()
    {
        var newStateConfiguration = GetCurrentPlayerStateConfiguration();

        playerRewardUi.UpdateSlider(currentValue, newStateConfiguration.Color);
        if (currentStateConfiguration != newStateConfiguration)
        {
            playerRewardUi.UpdateDescription(newStateConfiguration.Description,
                newStateConfiguration.Color);
        }

        if (currentStateConfiguration != newStateConfiguration)
        {
            currentStateConfiguration = newStateConfiguration;
            UpdateModelAndAnimation();
        }
    }

    private void UpdateModelAndAnimation(bool instantly = false)
    {
        stateMachine.PlayerAnimationController.SetFloat(PlayerAnimationType.MoveFloat,
            currentStateConfiguration.AnimationMoveFloatValue);
        foreach (var playerModelConfigurations in playerModelConfigurations)
        {
            playerModelConfigurations.Model.SetActive(currentStateConfiguration.PlayerState ==
                                                      playerModelConfigurations.PlayerStateType);
        }

        if (instantly == false)
        {
            EventBus.RaiseEvent<IPlaySound>(t => t.Play(SoundPlayType.LevelUp));
            EventBus.RaiseEvent<ISpawnEffect>(t => t.SpawnEffect(transform, EffectType.LevelUp, true));
            playerRotateObject.DOLocalRotate(new Vector3(0, 360, 0), 0.6f, RotateMode.FastBeyond360).SetRelative(true)
                .SetEase(Ease.Linear).OnComplete(() => playerRotateObject.localRotation = Quaternion.identity);
        }
    }

    private PlayerStateConfiguration GetCurrentPlayerStateConfiguration()
    {
        var returnValue = playerStateStorage.PlayerStorageConfigurations[0];
        foreach (var playerStorage in playerStateStorage.PlayerStorageConfigurations)
        {
            if (playerStorage.MaxLevel <= currentValue && playerStorage.MaxLevel >= returnValue.MaxLevel)
                returnValue = playerStorage;
        }

        return returnValue.PlayerStateConfiguration;
    }

    public void RewardRaised(RewardType rewardType, float value)
    {
        switch (rewardType)
        {
            case RewardType.Money:
                currentValue += value;
                EventBus.RaiseEvent<ISpawnEffect>(t => t.SpawnEffect(transform, EffectType.TakeMoney, true));
                EventBus.RaiseEvent<IPlaySound>(t => t.Play(SoundPlayType.Coin));
                break;
            case RewardType.Bottle:
                EventBus.RaiseEvent<ISpawnEffect>(t => t.SpawnEffect(transform, EffectType.TakeBottle, true));
                EventBus.RaiseEvent<IPlaySound>(t => t.Play(SoundPlayType.Bottle));
                currentValue -= value;
                break;
        }

        if (currentValue > 1) currentValue = 1;
        if (currentValue < 0)
        {
            EventBus.RaiseEvent<IToResultScreen>(t => t.Finish(false));
            currentValue = 0;
        }

        UpdateCurrentStateConfiguration();
    }

    public void Finish(bool isWin)
    {
        if (isWin == false)
        {
            SetCanvasStatus(false);
        }
    }
}