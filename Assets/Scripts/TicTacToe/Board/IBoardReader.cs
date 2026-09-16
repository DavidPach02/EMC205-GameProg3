using UnityEngine;

public interface IBoardReader {
    string GetCell(int row, int col);
    bool IsCellEmpty(int row, int col);
    int GetLength(int dimension);
}
