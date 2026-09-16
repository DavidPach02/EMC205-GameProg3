using UnityEngine;
using System.Collections.Generic;

public class StandardTurnManager : ITurnManager
{
    private Queue<IPlayer> playerQueue = new Queue<IPlayer>();
    private IPlayer currentPlayer;
    private IPlayer previousPlayer;

    public void AdvanceTurn() {
        previousPlayer = currentPlayer;

        // Dequeue current player
        currentPlayer = playerQueue.Dequeue();
        // Next player should be player.Peek()
        // Enqueue the previous player again
        if (previousPlayer != null) {
            playerQueue.Enqueue(previousPlayer);
        }

        Debug.Log($"Current Player:{currentPlayer.GetPlayerName()}");
    }

    public IPlayer GetCurrentPlayer() {
        return currentPlayer;
    }

    public IPlayer GetNextPlayer() {
        return playerQueue.Peek();
    }

    public void QueuePlayer(IPlayer player) {
        playerQueue.Enqueue(player);
    }
}
