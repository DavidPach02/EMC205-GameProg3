using UnityEngine;

public class TileContext {
    public int row { get; }
    public int col { get; }
    public string occupyingSymbol { get; set; }
    public ITileState currentState { get; private set; }

    public TileContext(int row, int col) {
        this.row = row;
        this.col = col;
        TransitionTo(new NormalTileState());
    }

    public void TransitionTo(ITileState newState){
        currentState = newState;
    }

    public void PassTurn() {
        currentState?.OnTurnPassed(this);
    }

    public bool TryClick(string symbol) {
        if (currentState == null || !currentState.canBeClicked)
            return false;

        currentState.OnClicked(this, symbol);
        return true;
    }
}
