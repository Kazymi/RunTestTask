using StateMachine;
using UnityEngine;

public class PlayerMoveState : State
{
    private readonly PlayerAnimationController _playerAnimationController;
    private readonly InputHelper _inputHelper;
    private readonly PlayerConfiguration _playerConfiguration;
    private readonly PlayerTransformRepository _playerTransformRepository;

    public PlayerMoveState(PlayerAnimationController playerAnimationController, InputHelper inputHelper,
        PlayerConfiguration playerConfiguration, PlayerTransformRepository playerTransformRepository)
    {
        _playerAnimationController = playerAnimationController;
        _inputHelper = inputHelper;
        _playerConfiguration = playerConfiguration;
        _playerTransformRepository = playerTransformRepository;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.Walk, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.Walk, false);
    }

    public override void Tick()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        var rotatePosition = Vector3.zero;
        if (_inputHelper.Horizontal != 0)
        {
            rotatePosition = _inputHelper.Horizontal > 0
                ? _playerConfiguration.RotateStrafe
                : -_playerConfiguration.RotateStrafe;
        }

        _playerTransformRepository.PlayerStrafeRotateObject.localRotation = Quaternion.Lerp(
            _playerTransformRepository.PlayerStrafeRotateObject.localRotation, Quaternion.Euler(rotatePosition),
            _playerConfiguration.StrafeRotateSpeed * Time.deltaTime);
    }

    private void Move()
    {
        var moveDirection = _playerTransformRepository.PlayerMain.forward * _playerConfiguration.SpeedForward;
        moveDirection += new Vector3(_inputHelper.Horizontal, 0, 0) * _playerConfiguration.SpeedStrafe;
        moveDirection *= Time.deltaTime;
        _playerTransformRepository.PlayerMain.position += moveDirection;
    }
}