using Godot;
using Network;

public class Lobby : Node
{
    public static Lobby main;
    public static Network.Network network;

    public Lobby()
    {
        main = this;
    }

    public void ConnectToNetwork()
    {
        network = new Network.Network(GetTree());
        AddChild(network);
        
        if(OS.GetEnvironment("DEDICATED_SERVER") == "true" || OS.GetName() == "Server")
        {
            GD.Print("Attempting to start server...");
            network.StartServer();
        }
        else
        {
            GD.Print("Attempting to start client...");
            network.StartClient();
        }
    }
}