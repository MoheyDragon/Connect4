using UnityEngine;
using TMPro;
public class TurnManager : Singletons<TurnManager>
{
    [SerializeField] Player[] players;
    [HideInInspector]
    public Player currentPlayer;
    [SerializeField] TextMeshProUGUI currentPlayerUi,ResultUi;
    const string startingText="-------";
    int fillGridCounter;
    int totalGridCellsCount;
    [Space]
    [SerializeField] AK.Wwise.Event drawSound;
    [SerializeField] AK.Wwise.Event winSound;
    public void ResetTurn(int cellsCount)
    {
        currentPlayer = players[0];
        ResultUi.text = startingText;
        ResultUi.color = Color.white;
        totalGridCellsCount=cellsCount;
        fillGridCounter = 0;
        UpdateCurrentPlayerUi();
    }
    public void ResultOfTurn(CellCoordinates cellCoordinates)
    {
        fillGridCounter++;
        if (fillGridCounter == totalGridCellsCount)
        {
            Draw();
        }
        else if (CellsOccupancyManager.Singleton.CheckWin(cellCoordinates.x,cellCoordinates.y, currentPlayer.Index))
        {
            PlayerWon();
        }
        else
        {
            CellsOccupancyManager.Singleton.ResumeGame();
            NextTurn();
        }
    }
    public void NextTurn()
    {
        if (currentPlayer == players[0])
            currentPlayer = players[1];
        else
            currentPlayer = players[0];
        UpdateCurrentPlayerUi();
    }
    public void PlayerWon()
    {
        ResultUi.color = currentPlayer.playerColor;
        ResultUi.text = currentPlayer.name+" Is The Winner";
        winSound.Post(gameObject);
    }
    public void Draw()
    {
        ResultUi.text = "Draw";
        currentPlayerUi.color = Color.white;
        currentPlayerUi.text = startingText;
        drawSound.Post(gameObject);
    }
    private void UpdateCurrentPlayerUi()
    {
        currentPlayerUi.color = currentPlayer.playerColor;
        currentPlayerUi.text = currentPlayer.name+" Turn";
    }
}
