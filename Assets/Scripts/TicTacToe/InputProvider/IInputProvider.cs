using UnityEngine;
using System;

public interface IInputProvider {
    event Action<int, int> OnCellSelected;
    void HandleCellClicked(int row, int col);
}
