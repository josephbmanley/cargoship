using Godot;
using Network;
using System;

public class Ship : NetworkEntity
{
	[Export]
	public float rotation_speed = Mathf.Pi / 2;
	[Export]
	public float move_speed = 200;
	[Export]
	public float max_speed = 10000;
	[Export]
	public float amount_of_friction = 75;
	public Vector2 velocity = Vector2.Zero;

	protected Vector2 Friction(Vector2 vel)
	{
		if(vel.x > 0)
			vel.x = Mathf.Clamp(vel.x - amount_of_friction, 0, max_speed);
		else
			vel.x = Mathf.Clamp(vel.x + amount_of_friction, -max_speed, 0);
		
		if(vel.y > 0)
			vel.y = Mathf.Clamp(vel.y - amount_of_friction, 0, max_speed);
		else
			vel.y = Mathf.Clamp(vel.y + amount_of_friction, -max_speed, 0);

		return vel;
	}

}