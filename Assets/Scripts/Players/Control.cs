using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Player _player;

    private InputAction _moveUpAction;
    private InputAction _moveDownAction;
    private InputAction _moveRightAction;
    private InputAction _moveLeftAction;
    private InputAction _shootBulletAction;
    private InputAction _firstSkillAction;
    private InputAction _secondSkillAction;
    private InputAction _thirdSkillAction;
    private InputAction _autoShootAction;
    private InputAction _pauseAction;
    private InputAction _boosterUIAction;

    private InputAction _moveAction;
    private Vector2 joystick;
    private bool isMove = false;
    private Coroutine _movementProcess;
    private Coroutine _shootingProcess;
    public enum Direction {up, down, right, left}
    private Direction _direction;


    private void Awake()
    {
        OnUpdate();
    }
    public void OnUpdate()
    {
        _player = GetComponent<Player>();
        if (_playerInput == null)
        {
            var regedit = GameObject.FindObjectsOfType<PlayerRegister>();
            foreach (var item in regedit)
            {
                if (item.thisPlayer == this.gameObject.tag)
                    _playerInput = item.gameObject.GetComponent<PlayerInput>();
            }
        }
    }

    private void OnEnable()
    {
        AddActions();
    }
    private void OnDisable()
    {
        RemoveActions();
    }

    private void AddActions()
    {
        _moveUpAction = _playerInput.actions["MoveUp"];
        _moveUpAction.performed += MoveUp;
        _moveUpAction.Enable();

        _moveDownAction = _playerInput.actions["MoveDown"];
        _moveDownAction.performed += MoveDown;
        _moveDownAction.Enable();

        _moveRightAction = _playerInput.actions["MoveRight"];
        _moveRightAction.performed += MoveRight;
        _moveRightAction.Enable();

        _moveLeftAction = _playerInput.actions["MoveLeft"];
        _moveLeftAction.performed += MoveLeft;
        _moveLeftAction.Enable();

        _moveAction = _playerInput.actions["Move"];
        _moveAction.performed += Move;
        _moveAction.Enable();

        //_shootBulletAction = _playerInput.actions["Shoot"];
        //_shootBulletAction.performed += ShootBullet;
        //_shootBulletAction.Enable();

        _firstSkillAction = _playerInput.actions["FirstSkill"];
        _firstSkillAction.performed += FirstSkill;
        _firstSkillAction.Enable();

        _secondSkillAction = _playerInput.actions["SecondSkill"];
        _secondSkillAction.performed += SecondSkill;
        _secondSkillAction.Enable();

        _thirdSkillAction = _playerInput.actions["ThirdSkill"];
        _thirdSkillAction.performed += ThirdSkill;
        _thirdSkillAction.Enable();

        _autoShootAction = _playerInput.actions["AutoShoot"];
        _autoShootAction.performed += AutoShoot;
        _autoShootAction.Enable();

        _pauseAction = _playerInput.actions["Pause"];
        _pauseAction.performed += UsePause;
        _pauseAction.Enable();

        _boosterUIAction = _playerInput.actions["BoosterUI"];
        _boosterUIAction.performed += ShowBoosters;
        _boosterUIAction.Enable();
    }

    private void RemoveActions()
    {
        _moveUpAction.canceled -= MoveUp;
        _moveUpAction.Disable();

        _moveDownAction.canceled -= MoveDown;
        _moveDownAction.Disable();

        _moveRightAction.canceled -= MoveRight;
        _moveRightAction.Disable();

        _moveLeftAction.canceled -= MoveLeft;
        _moveLeftAction.Disable();

        _moveAction.canceled -= Move;
        _moveAction.Disable();

        //_shootBulletAction.canceled -= ShootBullet;
        //_shootBulletAction.Disable();

        _firstSkillAction.canceled -= FirstSkill;
        _firstSkillAction.Disable();

        _secondSkillAction.canceled -= SecondSkill;
        _secondSkillAction.Disable();

        _thirdSkillAction.canceled -= ThirdSkill;
        _thirdSkillAction.Disable();

        _autoShootAction.canceled -= AutoShoot;
        _autoShootAction.Disable();

        _pauseAction.canceled -= UsePause;
        _pauseAction.Disable();

        _boosterUIAction.canceled -= ShowBoosters;
        _boosterUIAction.Disable();
    }

    #region Coroutines
    private IEnumerator _moveKeys(InputAction.CallbackContext context)
    {
        var move = context.ReadValue<float>();

        while (move > 0.2f)
        {
            switch (_direction)
            {
                case Direction.up:
                    _player.MoveUp();
                    break;
                case Direction.down:
                    _player.MoveDown();
                    break;
                case Direction.right:
                    _player.MoveRight();
                    break;
                case Direction.left:
                    _player.MoveLeft();
                    break;
            }
            yield return null;
            move = context.ReadValue<float>();
        }
    }
    private IEnumerator _move(InputAction.CallbackContext context)
    {
        var move = context.ReadValue<Vector2>();

        while ((Mathf.Abs(move.x)) > 0.7f || (Mathf.Abs(move.y)) > 0.7f)
        {
            switch (_direction)
            {
                case Direction.up:
                    _player.MoveUp();
                    break;
                case Direction.down:
                    _player.MoveDown();
                    break;
                case Direction.right:
                    _player.MoveRight();
                    break;
                case Direction.left:
                    _player.MoveLeft();
                    break;
            }
            yield return null;
            move = context.ReadValue<Vector2>();
        }
    }

    private IEnumerator _shoot (InputAction.CallbackContext context)
    {
        var shoot = context.ReadValue<float>();

        while (shoot > 0.1f)
        {
            _player?.Shoot();
            yield return null;
            shoot = context.ReadValue<float>();
        }
    }
    #endregion

    private void Move(InputAction.CallbackContext context)
    {
        joystick = context.ReadValue<Vector2>();
       


        if (joystick.y > 0.7f)
        {
            if (_movementProcess != null)
                StopCoroutine(_movementProcess);
            _direction = Direction.up;
            _movementProcess = StartCoroutine(_move(context));
        }
        else if (joystick.y < -0.7f)
        {
            if (_movementProcess != null)
                StopCoroutine(_movementProcess);
            _direction = Direction.down;
            _movementProcess = StartCoroutine(_move(context));
        }
        else if (joystick.x > 0.7f)
        {
            if (_movementProcess != null)
                StopCoroutine(_movementProcess);
            _direction = Direction.right;
            _movementProcess = StartCoroutine(_move(context));
        }
        else if (joystick.x < -0.7f)
        {
            if (_movementProcess != null)
                StopCoroutine(_movementProcess);
            _direction = Direction.left;
            _movementProcess = StartCoroutine(_move(context));
        }
    }
    private void MoveUp(InputAction.CallbackContext context)
    {
        if (_moveUpAction.triggered)
        {
            if (_movementProcess != null) 
                StopCoroutine(_movementProcess);

            _direction = Direction.up;
            _movementProcess = StartCoroutine(_moveKeys(context));
        }
    }
    private void MoveDown(InputAction.CallbackContext context)
    {
        if (_moveDownAction.triggered)
        {
            if (_movementProcess != null)
                StopCoroutine(_movementProcess);

            _direction = Direction.down;
            _movementProcess = StartCoroutine(_moveKeys(context));
        }
    }

    private void MoveRight(InputAction.CallbackContext context)
    {
        if (_moveRightAction.triggered)
        {
            if (_movementProcess != null)
                StopCoroutine(_movementProcess);

            _direction = Direction.right;
            _movementProcess = StartCoroutine(_moveKeys(context));
        }
    }

    private void MoveLeft(InputAction.CallbackContext context)
    {
        if (_moveLeftAction.triggered)
        {
            if (_movementProcess != null)
                StopCoroutine(_movementProcess);

            _direction = Direction.left;
            _movementProcess = StartCoroutine(_moveKeys(context));
        } 
    }
    //private void ShootBullet(InputAction.CallbackContext context)
    //{
    //    _player?.Shoot();
    //}
    private void FirstSkill(InputAction.CallbackContext context)
    {
        _player?.UseFirstSkill();
    }
    private void SecondSkill(InputAction.CallbackContext context)
    {
        _player?.UseSecondSkill();
    }
    private void ThirdSkill(InputAction.CallbackContext context)
    {
        _player?.UseThirdSkill();
    }
    private void AutoShoot(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.1f)
        {
            if (_shootingProcess != null)
                StopCoroutine(_shootingProcess);

            _shootingProcess = StartCoroutine(_shoot(context));
        }
    }
    private void UsePause(InputAction.CallbackContext context)
    {
        _player?.UsePause();
    }
    private void ShowBoosters(InputAction.CallbackContext context)
    {
        _player?.UseBoosterUI();
    }

}
