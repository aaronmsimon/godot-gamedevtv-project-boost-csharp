using System;
using Godot;

public partial class Player : RigidBody3D
{
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_accept"))
		{
			ApplyCentralForce(Basis.Y * (float)delta * 1000f);
		}
		if (Input.IsActionPressed("ui_left"))
		{
			ApplyTorque(Vector3.Back * (float)delta * 100f);
		}
		if (Input.IsActionPressed("ui_right"))
		{
			ApplyTorque(Vector3.Forward * (float)delta * 100f);
		}
	}
}
