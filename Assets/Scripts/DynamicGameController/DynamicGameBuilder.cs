using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DynamicGameBuilder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rowsText, columnText, winningCountText;
    [SerializeField] Toggle allowDiagonalChecking;
    [SerializeField] AK.Wwise.Event music;
    private void Awake()
    {
        allowDiagonalChecking.onValueChanged.AddListener(AllowDiagonal);
    }
    private void Start()
    {
        music.Post(gameObject);
        NewGame();
    }
    public void NewGame()
    {
        int rows = GetIntValue(rowsText);
        int columns = GetIntValue(columnText);
        int winningCount = GetIntValue(winningCountText);
        GridBuilder.Singleton.GenerateGridByUserValues(rows, columns, winningCount);
        TurnManager.Singleton.ResetTurn(rows*columns);
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
