using Godot;

public class Weapon : Node2D
{
    [Export]
    public ulong reloadTime = 1;
    [Export]
    public Vector2 bulletSpawnOffset = Vector2.Zero;
    bool weaponLoaded = true;
    ulong lastShootTime;

    public bool IsLoaded()
    {
        return OS.GetUnixTime() > lastShootTime + reloadTime;
    }

    public override void _Process(float delta)
    {
        if(IsNetworkMaster())
        {
            if(Input.IsActionPressed("shoot") && IsLoaded())
            {
                lastShootTime = OS.GetUnixTime();
                RandomNumberGenerator r = new RandomNumberGenerator();
                r.Randomize();
                Rpc(nameof(Shoot), $"{Name}-{r.Randi()}", GlobalRotation);
            }
        }
    }
    [RemoteSync]
    public void Shoot(string bId, float rotation)
    {
        Bullet bullet = ResourceLoader.Load<PackedScene>("res://nodes/Bullet.tscn").Instance() as Bullet;
        bullet.Name = bId;
        bullet.GlobalRotation = rotation + (Mathf.Pi / 2);
        bullet.GlobalPosition = this.GlobalPosition + bulletSpawnOffset.Rotated(GlobalRotation);
        bullet.SetNetworkMaster(1);
        Game.main.AddChild(bullet);
    }
}