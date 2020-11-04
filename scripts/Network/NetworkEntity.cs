using Godot;

namespace Network
{
    public class NetworkEntity : KinematicBody2D
    {
        [Master]
        public void NetMovePosition(Vector2 velocity)
        {
            MoveAndSlide(velocity);
            RpcUnreliable("NetSetPosition", Position);
        }
        [RemoteSync]
        public void NetSetPosition(Vector2 pos)
        {
            Position = pos;
        }

        [Master]
        public void NetChangeRotation(float change)
        {
            Rotate(change);
            RpcUnreliable("NetSetRotation", Rotation);
        }
        [RemoteSync]
        public void NetSetRotation(float rotation)
        {
            Rotation = rotation;
        }

    }
}