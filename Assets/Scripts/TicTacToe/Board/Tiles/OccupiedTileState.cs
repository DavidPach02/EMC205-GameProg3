using UnityEngine;

public class OccupiedTileState : ITileState {

    public bool canBeClicked => false;
    public string displaySymbol => tileSymbol;
    private string tileSymbol;
    private int turnsOccupied = 0;
    private int maxTurnsOccupied = 3;
    private float randomEjectChance = 0.10f;

    public void OnTurnPassed(TileContext tile) {
        turnsOccupied++;

        // 10% random chance per turn after holding mark for at least 3 turns
        if (turnsOccupied >= maxTurnsOccupied && Random.value < randomEjectChance) {
            EjectPlayer(tile);
        }
    }

    public void OnClicked(TileContext tile, string symbol) { }

    private void EjectPlayer(TileContext tile) {
        Debug.Log($"[Tile Event] Cell ({tile.row}, {tile.col}) EJECTED symbol '{tile.occupyingSymbol}'!");
        tile.occupyingSymbol = null;

        // Return back to Normal empty state (or transition to Unclickable/Hazard state!)
        tile.TransitionTo(new NormalTileState());
    }
}
