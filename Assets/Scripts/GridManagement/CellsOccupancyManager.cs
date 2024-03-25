using UnityEngine;
using System.Collections;
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
        isGameActive = true;
    }
    bool isGameActive;
    public void ResumeGame() => isGameActive = true;
    [SerializeField] AK.Wwise.Event notClickAbleSound;
    public void ClickingOnColumn(int columnIndex)
    {
        if (!isGameActive)
        {
            notClickAbleSound.Post(gameObject);
            return;
        }
        if (CheckIfColumnHasEmptyCells(columnIndex,out int firstEmptyRow))
        {
            AddDiskToCell(columnIndex,firstEmptyRow);
        }
        else
            notClickAbleSound.Post(gameObject);

    }
    private void AddDiskToCell(int x, int y)
    {
        isGameActive = false;
        cells[x, y].InsertDisk(TurnManager.Singleton.currentPlayer);
        DisksAnimator.Singleton.DropDiskAnimation(cells[x, y]);
    }
    private bool CheckIfColumnHasEmptyCells(int x, out int firstEmptyRow)
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
    public bool CheckWin(int x, int y, int playerIndex)
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

            if (IsCoordinatesValid(nx, ny) && cells[nx, ny].HasSamePlayerIndex(playerIndex))
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

    private bool IsCoordinatesValid(int x, int y)
    {
        return x >= 0 && x < columns && y >= 0 && y < rows;
    }

    #endregion


    
}
