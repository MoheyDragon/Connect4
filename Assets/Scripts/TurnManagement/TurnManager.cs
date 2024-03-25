using UnityEngine;
using TMPro;
public class TurnManager : Singletons<TurnManager>
{
    [SerializeField] Player[] players;
    [HideInInspector]
    public Player currentPlayer;
    [SerializeField] TextMeshProUGUI currentPlayerUi,ResultUi;
    const string startingText="-------";
    private void Start()
    {
        ResetTurn();
    }
    public void ResetTurn()
    {
        currentPlayer = players[0];
        ResultUi.text = startingText;
        ResultUi.color = Color.white;
        UpdateCurrentPlayerUi();
    }
    public void NextTurn()
    {
        if (currentPlayer == players[0])
            currentPlayer = players[1];
        else
            currentPlayer = players[0];
        UpdateCurrentPlayerUi();
    }
    private void UpdateCurrentPlayerUi()
    {
        currentPlayerUi.color = currentPlayer.playerColor;
        currentPlayerUi.text = currentPlayer.name+" Turn";
    }
    public void PlayerWon()
    {
        ResultUi.color = currentPlayer.playerColor;
        ResultUi.text = currentPlayer.name+" Is The Winner";
    }
    public void Draw()
    {
        ResultUi.text = "Draw";
        currentPlayerUi.color = Color.white;
        currentPlayerUi.text = startingText;
    }
}
