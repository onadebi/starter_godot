using Godot;
using System.Diagnostics;

public partial class Ocean : Node2D
{
	private Sprite2D _planeSprite;
	private Stopwatch sw = new();
	private int frameCount = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print($"Ocean started. Running on Godot {Engine.GetVersionInfo()}. Frame rate {Engine.GetFramesPerSecond()} FPS");
		_planeSprite = GetNode<Sprite2D>("Plane"); // Nested node path

		// Position of Plane
		GD.Print($"Plane position: {_planeSprite.Position}");
		GD.Print($"Plane Global position: {_planeSprite.GlobalPosition}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Move the plane (frame-rate independent)
		if (_planeSprite.GlobalPosition.X <= 870)
		{
			float speedVel = _planeSprite.GlobalPosition.X <= 800 
			? (_planeSprite.GlobalPosition.X <= 600 ? 100f : 70f) 
			: 40f;
			_planeSprite.Position += new Vector2(speedVel * (float)delta, 0);
		}
		if (_planeSprite.GlobalPosition.X >= 870 && _planeSprite.RotationDegrees < 45f)
		{
			float rotateSpeed = 20f; // degrees per second (lower is slower and smoother)
			_planeSprite.RotationDegrees = Mathf.MoveToward(_planeSprite.RotationDegrees, 45f, rotateSpeed * (float)delta);
		}

		frameCount++;

		// Start timing at the first frame of the batch
		if (frameCount % 60 == 1)
			sw.Restart();

		// At the end of the 60-frame batch, stop and report
		if (frameCount % 60 == 0)
		{
			sw.Stop();
			GD.Print($"Delta: {delta} s, FPS: {Engine.GetFramesPerSecond()}");
			GD.Print($"Total time for 60 frames: {sw.Elapsed.TotalMilliseconds} ms — avg {sw.Elapsed.TotalMilliseconds / 60.0} ms/frame");
			sw.Reset();
		}
	}

	public override void _ExitTree()
	{
		GD.Print("Ocean ended");
	}
}
