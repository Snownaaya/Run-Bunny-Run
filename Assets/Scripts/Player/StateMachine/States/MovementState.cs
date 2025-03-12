using UnityEngine;

public abstract class MovementState : IStates, IInputState
{
    private IInputProvider _inputProvider;
    private ISwitcher _switcher;
    private StateMachineData _data;
    private Character _character;

    protected MovementState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider)
    {
        _switcher = switcher;
        _data = data;
        _character = character;
        _inputProvider = inputProvider;
    }

    public CharacterView CharacterView => _character.CharacterView;
    public PlayerAudio PlayerAudio => _character.PlayerAudio;
    public StateMachineData Data => _data;
    public PlayerInput PlayerInput => _character.PlayerInput;
    public ISwitcher Switcher => _switcher;
    public IInputProvider InputProvider => _inputProvider;

    public virtual void Enter() =>
        Debug.Log(GetType());

    public virtual void Exit() { }

    public virtual void HandleInput()
    {
        if (InputProvider.MoveRight?.WasPressedThisFrame()  == true && Data.CurrentLane < 2)
            Data.ChangeLane(1);
        else if (InputProvider.MoveLeft?.WasPressedThisFrame() == true && Data.CurrentLane > 0)
            Data.ChangeLane(-1);
    }

    public virtual void Update()
    {
        float targetX = (Data.CurrentLane - 1) * _character.LaneDistance; 
        float currentX = _character.CharacterController.transform.position.x;

        Vector3 moveDirection = new Vector3(targetX, Data.YVelocity, 0f);

        _character.CharacterController.Move(moveDirection * Data.Speed * Time.deltaTime);
        currentX = targetX;
        _character.CharacterController.center = _character.CharacterController.center;
    }

    protected bool IsHorizontalInputZero() =>
        Data.Speed == 0;
}