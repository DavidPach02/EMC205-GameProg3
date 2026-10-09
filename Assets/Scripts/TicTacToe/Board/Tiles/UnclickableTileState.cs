using UnityEngine;

public class UnclickableTileState : ITileState {
    public bool canBeClicked => false;
    public string displaySymbol => "";

    public void OnEnterState(TileContext tile) {
        tile.occupyingSymbol = displaySymbol;
    }

    public void OnTurnPassed(TileContext tile) { }
    public void OnClicked(TileContext tile, string symbol) { }
}
