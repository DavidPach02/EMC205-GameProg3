using UnityEngine;

public class NormalTileState : ITileState {
    public bool canBeClicked => true;
    public string displaySymbol => "";

    public void OnTurnPassed(TileContext tile) { }

    public void OnClicked(TileContext tile, string symbol) {
        tile.occupyingSymbol = symbol;
        tile.TransitionTo(new OccupiedTileState());
    }
}
