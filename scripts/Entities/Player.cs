using Godot;
using Network;
using System;


public class Player : Ship
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Camera2D cam = GetNode<Camera2D>("Camera2D");
		if(IsNetworkMaster())
		{
			if(cam != null)
			{
				cam.Current = true;
			}
			else
			{
				GD.PrintErr("Camera missing from player!");
			}
		}
	}

	public override void _Process(float delta)
	{
		var moveByX = 0;
		var moveByY = 0;
		if(IsNetworkMaster())
		{
			
			float rotation_input = RotationInput(delta);
			if(rotation_input != 0)
				RpcUnreliable("NetChangeRotation", rotation_input);
			
			velocity = Friction(velocity + MovementInput(delta));
			
			RpcUnreliable("NetMovePosition", velocity * delta);
		}
	}

	Vector2 MovementInput(float delta)
	{
		Vector2 movement_input = Vector2.Zero;

		if(Input.IsKeyPressed((int)KeyList.Up))
			movement_input = movement_input + new Vector2(0, -move_speed).Rotated(Rotation);
		
		return movement_input;
	}

	float RotationInput(float delta)
	{
		float rotation_change = 0;

		if(Input.IsKeyPressed((int)KeyList.Right))
			rotation_change += rotation_speed * delta;
		if(Input.IsKeyPressed((int)KeyList.Left))
			rotation_change -= rotation_speed * delta;
		
		return rotation_change;
	}


}
