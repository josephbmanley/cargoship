using Godot;
using Network;
using System;

public class Game : Node
{
	public static Game main;

	[Signal]
	public delegate void PlayerJoinedGame();

	public override void _Ready()
	{
		main = this;
		Network.Network.main.playerConnected += OnPlayerJoin;
		Network.Network.main.playerDisconnected += OnPlayerExit;
	}

	public void OnPlayerJoin(object source, ConnectionEventArgs e)
	{
		Rpc("SpawnPlayer", e.peerId);
		foreach (var item in Network.Network.main.state.playerNames)
		{
			RpcId(e.peerId, "SpawnPlayer", item.Key);
		}
	}

	public void OnPlayerExit(object source, ConnectionEventArgs e)
	{
		Rpc("UnspawnPlayer", e.peerId);
	}

	[RemoteSync]
	public void UnspawnPlayer(int peerId)
	{
		Node n = GetNode(peerId.ToString());
		if(n != null)
			n.QueueFree();
	}

	[RemoteSync]
	public void SpawnPlayer(int peerId)
	{
		EmitSignal("PlayerJoinedGame");
		Player p = ResourceLoader.Load<PackedScene>("res://nodes/entities/Player.tscn").Instance() as Player;
		p.Name = peerId.ToString();
		p.SetNetworkMaster(peerId);
		AddChild(p);
	}
}
