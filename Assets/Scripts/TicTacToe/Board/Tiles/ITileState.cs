using UnityEngine;

public interface ITileState {

    bool canBeClicked { get; }
    string displaySymbol { get; }
    void OnEnterState(TileContext tile);
    void OnTurnPassed(TileContext tile);
    void OnClicked(TileContext tile, string symbol);
}
