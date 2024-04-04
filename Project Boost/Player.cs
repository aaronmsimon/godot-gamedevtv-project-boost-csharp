using System;
using Godot;

public partial class Player : RigidBody3D
{
	[Export(PropertyHint.Range,"750,3000")] private float thrust = 1000f;
	[Export] private float torque = 100f;
	[Export] private float sceneReloadTime = 1f;
	[Export] private float levelCompleteTime = 1f;

	private AudioStreamPlayer explosionAudio;
	private AudioStreamPlayer successAudio;
	private AudioStreamPlayer3D rocketAudio;
	private GpuParticles3D boosterParticles;
	private GpuParticles3D rightBoosterParticles;
	private GpuParticles3D leftBoosterParticles;
	private GpuParticles3D explosionParticles;
	private GpuParticles3D successParticles;

	private bool isTransitioning = false;

    public override void _Ready()
    {
		explosionAudio = GetNode<AudioStreamPlayer>("ExplosionAudio");
		successAudio = GetNode<AudioStreamPlayer>("SuccessAudio");
		rocketAudio = GetNode<AudioStreamPlayer3D>("RocketAudio");
		boosterParticles = GetNode<GpuParticles3D>("BoosterParticles");
		rightBoosterParticles = GetNode<GpuParticles3D>("RightBoosterParticles");
		leftBoosterParticles = GetNode<GpuParticles3D>("LeftBoosterParticles");
		explosionParticles = GetNode<GpuParticles3D>("ExplosionParticles");
		successParticles = GetNode<GpuParticles3D>("SuccessParticles");

        BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
	{
		if (Input.IsActionPressed("boost"))
		{
			ApplyCentralForce(Basis.Y * (float)delta * thrust);
			if (!rocketAudio.Playing)
			{
				rocketAudio.Play();
				boosterParticles.Emitting = true;
			}
		}

		if (Input.IsActionJustReleased("boost"))
		{
			rocketAudio.Stop();
			boosterParticles.Emitting = false;
		}

		if (Input.IsActionPressed("rotate_left"))
		{
			ApplyTorque(Vector3.Back * (float)delta * torque);
			rightBoosterParticles.Emitting = true;
		}

		if (Input.IsActionJustReleased("rotate_left"))
		{
			rightBoosterParticles.Emitting = false;
		}

		if (Input.IsActionPressed("rotate_right"))
		{
			ApplyTorque(Vector3.Forward * (float)delta * torque);
			leftBoosterParticles.Emitting = true;
		}
		
		if (Input.IsActionJustReleased("rotate_right"))
		{
			leftBoosterParticles.Emitting = false;
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
		explosionAudio.Play();
		explosionParticles.Emitting = true;
		SetProcess(false);
		isTransitioning = true;
		await ToSignal(GetTree().CreateTimer(sceneReloadTime), Timer.SignalName.Timeout);
		GetTree().CallDeferred("reload_current_scene");
	}

	private async void CompleteLevel(String nextLevelFile)
	{
		GD.Print("Level Complete");
		successAudio.Play();
		successParticles.Emitting = true;
		SetProcess(false);
		isTransitioning = true;
		await ToSignal(GetTree().CreateTimer(levelCompleteTime), Timer.SignalName.Timeout);
		GetTree().ChangeSceneToFile(nextLevelFile);
	}
}
