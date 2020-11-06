using Godot;

public class Turret : Weapon
{
	Node2D gun;
	public override void _Ready()
	{
		gun = GetNode<Node2D>("Gun");
		base._Ready();
	}
	public override void _Process(float delta)
	{
		if(IsNetworkMaster())
		{
			float newRot = Rotation + gun.GetAngleTo(GetGlobalMousePosition());
			if(newRot != Rotation);
				RpcUnreliable("UpateGunRotation", newRot);
		}
		base._Process(delta);
	}

	[RemoteSync]
	public void UpateGunRotation(float rad)
	{
		Rotation = rad;
	}
}
