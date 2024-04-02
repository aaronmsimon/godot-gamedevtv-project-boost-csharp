using Godot;

public partial class Player : RigidBody3D
{
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
	{
		if (Input.IsActionPressed("boost"))
		{
			ApplyCentralForce(Basis.Y * (float)delta * 1000f);
		}
		if (Input.IsActionPressed("rotate_left"))
		{
			ApplyTorque(Vector3.Back * (float)delta * 100f);
		}
		if (Input.IsActionPressed("rotate_right"))
		{
			ApplyTorque(Vector3.Forward * (float)delta * 100f);
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
