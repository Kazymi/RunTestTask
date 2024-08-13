using System.Collections;
using Cinemachine;
using EventBusSystem;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using StateMachine = StateMachine.StateMachine;

public class PlayerStateMachine : MonoBehaviour, IToResultScreen
{
    [SerializeField] private GameObject tutorialObject;
    [SerializeField] private PlayerConfiguration playerConfiguration;
    [SerializeField] private PlayerTransformRepository playerTransformRepository;
    [SerializeField] private Animator animator;

    [Header("Virtual camera")] [SerializeField]
    private CinemachineVirtualCamera finishCamera;

    [SerializeField] private CinemachineVirtualCamera defaultCamera;

    private InputHelper inputHelper;
    private bool isWin;
    private bool isLose;
    private global::StateMachine.StateMachine stateMachine;

    public PlayerAnimationController PlayerAnimationController { get; private set; }

    public PlayerStateMachine(PlayerAnimationController playerAnimationController)
    {
        PlayerAnimationController = playerAnimationController;
    }

    private void Awake()
    {
        PlayerAnimationController = new PlayerAnimationController(animator);
        inputHelper = new InputHelper();
        InitializeStateMachine();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Update()
    {
        inputHelper.Tick();
        stateMachine.Tick();
    }


    private void InitializeStateMachine()
    {
        var idleState = new PlayerAnimationState(PlayerAnimationController);
        var winState = new PlayerAnimationState(PlayerAnimationController, PlayerAnimationType.Win);
        var loseState = new PlayerAnimationState(PlayerAnimationController, PlayerAnimationType.Lose);
        var moveState = new PlayerMoveState(PlayerAnimationController, inputHelper, playerConfiguration,
            playerTransformRepository);

        idleState.AddTransition(new StateTransition(moveState, new FuncCondition(() =>
        {
            if (inputHelper.Horizontal != 0)
            {
                tutorialObject.SetActive(false);
            }

            return inputHelper.Horizontal != 0;
        })));
        moveState.AddTransition(new StateTransition(winState, new FuncCondition(() => isWin)));
        moveState.AddTransition(new StateTransition(loseState, new FuncCondition(() => isLose)));
        stateMachine = new global::StateMachine.StateMachine(idleState);
    }

    public void Finish(bool isWin)
    {
        defaultCamera.Priority = 0;
        finishCamera.Priority = 1;
        if (isWin)
        {
            this.isWin = true;
        }
        else
        {
            isLose = true;
        }
    }
}