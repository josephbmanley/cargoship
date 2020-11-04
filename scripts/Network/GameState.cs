using System;
using System.Collections.Generic;

namespace Network
{
    public class GameState
    {
        public Dictionary<int, string> playerNames;

        public GameState() {
            playerNames = new Dictionary<int, string>();
        }

        public string CleanupPlayer(int peerId)
        {
            string playerName = null;

            if(playerNames.ContainsKey(peerId))
            {
                playerName = playerNames[peerId];
                playerNames.Remove(peerId);
            }
            
            return playerName;
        }
    }
}