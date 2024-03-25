using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DynamicGameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rowsText, columnText, winningCountText;
    [SerializeField] Toggle allowDiagonalChecking;
    private void Awake()
    {
        allowDiagonalChecking.onValueChanged.AddListener(AllowDiagonal);
    }
    public void Restart()
    {
        int rows = GetIntValue(rowsText);
        int columns = GetIntValue(columnText);
        int winningCount = GetIntValue(winningCountText);
        GridBuilder.Singleton.GenerateGridByUserValues(rows, columns, winningCount);
        TurnManager.Singleton.ResetTurn();
    }
    private void AllowDiagonal(bool allow)
    {
        CellsOccupancyManager.Singleton.AllowDiagonalChecking(allow);
    }
    private int GetIntValue(TextMeshProUGUI tmText)
    {
        return int.Parse(tmText.text);
    }
}
