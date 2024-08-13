using StateMachine;
using UnityEngine;

public class PlayerMoveState : State
{
    private readonly PlayerAnimationController _playerAnimationController;
    private readonly InputHelper _inputHelper;
    private readonly PlayerConfiguration _playerConfiguration;
    private readonly PlayerTransformRepository _playerTransformRepository;

    private const float XInterval = 2.1f;

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
        Strafe();
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

    private void Strafe()
    {
        var endValue = _playerTransformRepository.PlayerMain.localPosition +
                       new Vector3(_inputHelper.Horizontal, 0, 0) * _playerConfiguration.SpeedStrafe * Time.deltaTime;
        var fixLocalX = Mathf.Clamp(endValue.x, -XInterval, XInterval);
        _playerTransformRepository.PlayerMain.localPosition = new Vector3(fixLocalX, endValue.y, endValue.z);
    }

    private void Move()
    {
        var moveDirectionMain = _playerTransformRepository.PlayerMain.forward * _playerConfiguration.SpeedForward;
        moveDirectionMain *= Time.deltaTime;
        _playerTransformRepository.PlayerMain.transform.position += moveDirectionMain;
    }
}