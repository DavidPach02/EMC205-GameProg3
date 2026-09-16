
using UnityEngine;

public interface IBoard : IBoardReader {
    void SetCell(int row, int col, string symbol);
    void ClearBoard();
}
