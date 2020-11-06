using Godot;
using System;

public class Bullet : Area2D
{
	public float bullet_speed = 50;
	Vector2 velocity = Vector2.Zero;
	const ulong TOTAL_LIFETIME = 2;
	ulong death_time;
	public override void _Ready()
	{
		death_time = OS.GetUnixTime() + TOTAL_LIFETIME;
		velocity = new Vector2(0, -bullet_speed).Rotated(GlobalRotation);
	}

	public override void _Process(float delta)
	{
		if(OS.GetUnixTime() > death_time)
		{
			Rpc(nameof(DestroySelf));
			return;
		}

		RpcUnreliable("UpdatePosition", Position + velocity * delta);

	}

	[RemoteSync]
	public void UpdatePosition(Vector2 pos)
	{
		Position = pos;
	}
	

	[RemoteSync]
	public void DestroySelf()
	{
		QueueFree();
	}
}
