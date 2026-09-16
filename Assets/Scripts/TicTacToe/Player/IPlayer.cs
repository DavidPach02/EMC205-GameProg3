using UnityEngine;
using System;

public interface IPlayer {
    string GetPlayerName();
    string GetSymbol();
    void MakeMove(IBoardReader board, Action<int, int> onMoveSelected);
}
