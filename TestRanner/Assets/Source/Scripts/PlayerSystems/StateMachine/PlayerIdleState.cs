using StateMachine;

public class PlayerAnimationState : State
{
    private readonly PlayerAnimationController _playerAnimationController;
    private readonly PlayerAnimationType _playerAnimationType;

    public PlayerAnimationState(PlayerAnimationController playerAnimationController, PlayerAnimationType playerAnimationType = PlayerAnimationType.Idle)
    {
        _playerAnimationController = playerAnimationController;
        _playerAnimationType = playerAnimationType;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(_playerAnimationType, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(_playerAnimationType, false);
    }
}