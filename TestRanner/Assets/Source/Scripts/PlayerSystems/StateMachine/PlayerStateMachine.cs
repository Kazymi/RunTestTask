using System.Collections;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using StateMachine = StateMachine.StateMachine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerConfiguration playerConfiguration;
    [SerializeField] private PlayerTransformRepository playerTransformRepository;
    [SerializeField] private Animator animator;
   
    private InputHelper inputHelper;
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

    private void Update()
    {
        inputHelper.Tick();
        stateMachine.Tick();
    }

    private void InitializeStateMachine()
    {
        var idleState = new PlayerIdleState(PlayerAnimationController);
        var moveState = new PlayerMoveState(PlayerAnimationController, inputHelper, playerConfiguration,
            playerTransformRepository);

        idleState.AddTransition(new StateTransition(moveState, new FuncCondition(() => inputHelper.Horizontal != 0)));

        stateMachine = new global::StateMachine.StateMachine(idleState);
    }
}