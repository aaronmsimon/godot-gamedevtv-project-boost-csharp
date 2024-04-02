using Godot;

public partial class Player : Node3D
{
	public override void _Ready()
	{
		GD.Print("Hello world");
		GD.Print("Don't panic");
		GD.Print("42");
	}

	public override void _Process(double delta)
	{
	}
}
