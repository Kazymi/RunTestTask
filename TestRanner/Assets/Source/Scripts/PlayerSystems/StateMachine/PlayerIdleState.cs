using StateMachine;

public class PlayerIdleState : State
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerIdleState(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.Idle, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.Idle, false);
    }
}