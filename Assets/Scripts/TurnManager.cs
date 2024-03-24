using UnityEngine;
public class TurnManager : Singletons<TurnManager>
{
    [SerializeField] Player[] players;
    [HideInInspector]
    public  Player currentPlayer;
    // Start is called before the first frame update
    private void Start()
    {
        ResetTurn();
    }
    private void ResetTurn()
    {
        for (int i = 0; i < 2; i++)
            players[i].Index = i;
        currentPlayer = players[0];

    }
    public void EndTurn()
    {
        if (currentPlayer == players[0])
            currentPlayer = players[1];
        else
            currentPlayer = players[0];
    }

}
