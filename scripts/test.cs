using Godot;
using System;

public class test : Node2D
{
	public override void _Ready()
	{
		Lobby.main.ConnectToNetwork();
	}

}
