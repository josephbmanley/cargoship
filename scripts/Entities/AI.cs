using Godot;
using System;
using System.Collections.Generic;
using Network;

public class AI : AudioStreamPlayer
{
	Queue<string> voiceQueue = new Queue<string>();
	public override void _Ready()
	{
		if(IsNetworkMaster())
		{
			Game.main.Connect("PlayerJoinedGame", this, nameof(OnNewPlayer));
			voiceQueue.Enqueue("welcome_to_galistorm.ogg");

			// Alert players if other players are connected to the server
			if(Network.Network.main.Multiplayer.GetNetworkConnectedPeers().Length > 1)
			{
				voiceQueue.Enqueue("rival_vessels_detected.ogg");
			}
			else
			{
				voiceQueue.Enqueue("monoply_assured.ogg");
			}
		}
	}

	public void OnNewPlayer()
	{
		voiceQueue.Enqueue("new_rival.ogg");
	}

	public override void _Process(float delta)
	{
		if(voiceQueue.Count > 0 && !Playing)
		{
			string audio_ogg = voiceQueue.Dequeue();
			Stream = ResourceLoader.Load<AudioStreamOGGVorbis>($"res://sound/voices/{audio_ogg}");
			Play();
		}
	}
}
