using Godot;

public partial class MovingHazard : AnimatableBody3D
{
    [Export] private Vector3 destination;
    [Export] private float duration = 1f;

    public override void _Ready()
    {
        Tween tween = CreateTween();
        tween.SetLoops();
        tween.SetTrans(Tween.TransitionType.Sine);
        tween.TweenProperty(this, "global_position", GlobalPosition + destination, duration);
        tween.TweenProperty(this, "global_position", GlobalPosition, duration);
    }
}