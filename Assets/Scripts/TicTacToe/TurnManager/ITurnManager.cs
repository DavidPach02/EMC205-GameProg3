using UnityEngine;

public interface ITurnManager {
    void QueuePlayer(IPlayer player);
    IPlayer GetCurrentPlayer();
    IPlayer GetNextPlayer();
    void AdvanceTurn();
}
