public class IdlingState : GroundedState
{
    public IdlingState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider) : base(switcher, data, character, inputProvider) { }

    public override void Enter()
    {
        base.Enter();

        Data.Speed = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            return;

        Switcher.SwitchState<RunningState>();
    }
}
