using UnityEngine;
public class CellsOccupancyManager : Singletons<CellsOccupancyManager>
{
    Cell[,] cells;
    int rows, columns;
    public void SetCellsData(Cell[,] cells,int rows,int columns,int winningCount)
    {
        this.rows = rows;
        this.columns = columns;
        this.cells = cells;
        this.winningCount = winningCount;
        totalGridCellsCount = rows * columns;
        fillGridCounter = 0;
        isGameActive = true;
    }
    bool isGameActive;
    public void ClickingOnColumn(int columnIndex)
    {
        if (!isGameActive) return;
        int firstEmptyRow;
        if (CheckIfColumnHasEmptyCells(columnIndex,out firstEmptyRow))
        {
            AddDiskToCell(columnIndex,firstEmptyRow);
        }

    }
    int fillGridCounter, totalGridCellsCount;
    private void AddDiskToCell(int x,int y)
    {
        Player currentPlayer = TurnManager.Singleton.currentPlayer;
        cells[x, y].InsertDisk(currentPlayer);
        fillGridCounter++;
        if (fillGridCounter == totalGridCellsCount)
        {
            TurnManager.Singleton.Draw();
        }
        else if (CheckWin(x, y, currentPlayer.Index))
        {
            isGameActive = false;
            TurnManager.Singleton.PlayerWon();
        }
        else
            TurnManager.Singleton.NextTurn();
    }
    bool CheckIfColumnHasEmptyCells(int x, out int firstEmptyRow)
    {
        for (int i = rows - 1; i > -1; i--)
        {
            if (!cells[x, i].HasDisk)
            {
                firstEmptyRow = i;
                return true;
            }
        }
        firstEmptyRow = -1;
        return false;
    }

    #region WinCalculations

    int winningCount=4;
    bool allowDiagonal=true;
    int[,] directions = { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 1, -1 } };
    public void AllowDiagonalChecking(bool allow)
    {
        allowDiagonal = allow;
    }
    private bool CheckWin(int x, int y, int playerIndex)
    {
        int allowedDirections = allowDiagonal ? 4 : 2;
        for (int i = 0; i < allowedDirections; i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];

            int count = 1 + CountInDirection(x, y, playerIndex, dx, dy)
                          + CountInDirection(x, y, playerIndex, -dx, -dy);

            if (count >= winningCount)
            {
                return true;
            }
        }

        return false;
    }


    private int CountInDirection(int x, int y, int playerIndex, int dx, int dy)
    {
        int count = 0;

        for (int i = 1; i < winningCount; i++)
        {
            int nx = x + i * dx;
            int ny = y + i * dy;

            if (IsCellInGridBoundaries(nx, ny) && cells[nx, ny].HasSamePlayerIndex(playerIndex))
            {
                count++;
            }
            else
            {
                break;
            }
        }
        return count;
    }

    private bool IsCellInGridBoundaries(int x, int y)
    {
        return x >= 0 && x < columns && y >= 0 && y < rows;
    }

    #endregion


    
}
