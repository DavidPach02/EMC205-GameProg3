using UnityEngine;
using System.Collections.Generic;

public interface ITurnManager {
    void QueuePlayer(IPlayer player);
    void SetPlayerQueue(Queue<IPlayer> playerQueue);
    IPlayer GetCurrentPlayer();
    IPlayer GetNextPlayer();
    void AdvanceTurn();
}
