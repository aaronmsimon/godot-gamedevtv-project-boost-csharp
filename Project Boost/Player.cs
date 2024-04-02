using Godot;

public partial class Player : Node3D
{
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_accept"))
		{
			Position += Vector3.Up * (float)delta;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			RotateZ((float)delta);
		}
		if (Input.IsActionPressed("ui_right"))
		{
			RotateZ((float)-delta);
		}
	}
}
