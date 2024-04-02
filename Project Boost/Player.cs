using Godot;

public partial class Player : RigidBody3D
{
	[Export(PropertyHint.Range,"750,3000")] private float thrust = 1000f;
	[Export] private float torque = 100f;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
	{
		if (Input.IsActionPressed("boost"))
		{
			ApplyCentralForce(Basis.Y * (float)delta * thrust);
		}
		if (Input.IsActionPressed("rotate_left"))
		{
			ApplyTorque(Vector3.Back * (float)delta * torque);
		}
		if (Input.IsActionPressed("rotate_right"))
		{
			ApplyTorque(Vector3.Forward * (float)delta * torque);
		}
	}

	private void OnBodyEntered(Node body)
	{
		if (body.GetGroups().Contains("Goal"))
		{
			GD.Print("You win!");
		}
		if (body.GetGroups().Contains("Hazard"))
		{
			GD.Print("You missed the goal.");
		}
	}
}
