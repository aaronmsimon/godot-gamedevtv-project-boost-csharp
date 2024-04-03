using System;
using Godot;

public partial class Player : RigidBody3D
{
	[Export(PropertyHint.Range,"750,3000")] private float thrust = 1000f;
	[Export] private float torque = 100f;
	[Export] private float sceneReloadTime = 1f;
	[Export] private float levelCompleteTime = 1f;

	private bool isTransitioning = false;

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
		if (!isTransitioning)
		{
			if (body.GetGroups().Contains("Goal"))
			{
				LandingPad lp = (LandingPad)body;
				CompleteLevel(lp.FilePath);
			}
			if (body.GetGroups().Contains("Hazard"))
			{
				CrashSequence();
			}
		}
	}

	private async void CrashSequence()
	{
		GD.Print("KABOOM!");
		SetProcess(false);
		isTransitioning = true;
		await ToSignal(GetTree().CreateTimer(sceneReloadTime), Timer.SignalName.Timeout);
		GetTree().CallDeferred("reload_current_scene");
	}

	private async void CompleteLevel(String nextLevelFile)
	{
		GD.Print("Level Complete");
		SetProcess(false);
		isTransitioning = true;
		await ToSignal(GetTree().CreateTimer(levelCompleteTime), Timer.SignalName.Timeout);
		GetTree().ChangeSceneToFile(nextLevelFile);
	}
}
