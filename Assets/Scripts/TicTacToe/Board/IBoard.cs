
using UnityEngine;

public interface IBoard : IBoardReader {
    bool SetCell(int row, int col, string symbol);
    void ClearBoard();

    bool ForceClearCell(int row, int col);

    void AdvanceTileTurn();
}
