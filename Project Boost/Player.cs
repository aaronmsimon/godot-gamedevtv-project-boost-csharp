using Godot;

public partial class Player : Node3D
{
	private float timer = 0f;
	private int spaceCount = 0;

	public override void _Ready()
	{
		int myNumber = 10;
		GD.Print(myNumber + 1);
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			spaceCount += 1;
			GD.Print(spaceCount);
		}
	}
}
