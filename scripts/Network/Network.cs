using Godot;
using System;

namespace Network
{
    public class Network : Node
    {
        const string GAME_SCENE = "res://scenes/World.tscn";
        const string LOBBY_SCENE = "res://scenes/Lobby.tscn";

        public static Network main;
        public static SceneTree tree;
        public int port { get; }
        public int max_players { get; set; }
        public NetworkedMultiplayerENet peer { get; set; }
        public event EventHandler<ConnectionEventArgs> playerConnected;
        public event EventHandler<ConnectionEventArgs> playerDisconnected;

        public GameState state = new GameState();
        

        public Network(SceneTree _tree = null, int _port = 7777)
        {
            port = _port;
            tree = _tree;
        }

        public void StartServer(int _max_players = 32)
        {
            max_players = _max_players;

            peer = new NetworkedMultiplayerENet();
            peer.CreateServer(port, max_players);
            tree.NetworkPeer = peer;
            if(tree.NetworkPeer != null)
            {
                GD.Print("Successfully started server!");
            } else {
                throw new Exception("Failed to start server!");
            }
            main = this;

            //Intialize signal connects
            tree.Connect("network_peer_connected", this, "NetworkPeerConnected");
            tree.Connect("network_peer_disconnected", this, "NetworkPeerDisconnected");

            tree.ChangeScene(GAME_SCENE);
        }

        public void StartClient(string host = "127.0.0.1")
        {
            peer = new NetworkedMultiplayerENet();
            peer.CreateClient(host, port);
            tree.NetworkPeer = peer;

            if(tree.NetworkPeer == null)
            {
                ConnectionFailed();
                return;
            }
            main = this;

            //Intialize signal connects
            tree.Connect("connected_to_server", this, "ConnectedToServer");
            tree.Connect("connection_failed", this, "ConnectionFailed");
            tree.Connect("server_disconnected", this, "ServerDisconnected");

            tree.ChangeScene(GAME_SCENE);
        }

        public void NetworkPeerConnected(int id)
        {
            GD.Print("Player connected to the server!");
        }
        public void NetworkPeerDisconnected(int id)
        {
            string p = state.CleanupPlayer(id);
            if(p == null)
                p = $"Unknown Player ({id})";
            GD.Print($"{p} left the server!");
            
            if(playerDisconnected != null)
                playerDisconnected(this, new ConnectionEventArgs(){peerId = id, peerName = p});
        }
        public void ConnectedToServer()
        {
            GD.Print("Connected to server!");
            Rpc("RegisterPlayer", "Fred");
        }

        public void ConnectionFailed()
        {
            GD.Print("Failed to connect to server!");
            tree.Quit();
        }

        public void ServerDisconnected()
        {
            tree.NetworkPeer = null;
            GD.Print("Server disconnected!");
            tree.ChangeScene(LOBBY_SCENE);
        }

        [Remote]
        public void RegisterPlayer(string name)
        {
            int sender = tree.GetRpcSenderId();
            if(playerConnected != null)
                playerConnected(this, new ConnectionEventArgs(){peerId = sender, peerName = name});
            state.playerNames.Add(sender, name);
            GD.Print($"Loaded {name}");
            
        }

        public void Close()
        {
            tree.NetworkPeer = null;
        }
    }
    public class ConnectionEventArgs : EventArgs
    {
        public int peerId { get; set; }
        public string peerName { get; set; }
    }
}