using System;
using UnityEngine;

public class DynamicInputProvider : IInputProvider {

    public event Action<int, int> OnCellSelected;

    public void HandleCellClicked(int row, int col)
    {
        Debug.Log($"Clicked [{row}, {col}]");
        OnCellSelected?.Invoke(row, col);
    }
}
