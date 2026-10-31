using UnityEngine;
using System;

public interface IPlayer {
    string GetPlayerName();
    string GetSymbol();

    int powerUpCharges { get; }
    bool TryUsePowerUp();
    void AddPowerUpCharge();

    void MakeMove(IBoardReader board, Action<int, int> onMoveSelected);
}
