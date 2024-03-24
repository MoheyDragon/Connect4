public class CellsOccupancyManager : Singletons<CellsOccupancyManager>
{
    Cell[,] cells;
    int rows, columns;
    public void SetCellsData(Cell[,] cells,int rows,int columns)
    {
        this.rows = rows;
        this.columns = columns;
        this.cells = cells;
    }
    public void ClickingOnColumn(int x)
    {
        int firstEmptyColumn;
        if (CheckIfColumnHasEmptyCells(x,out firstEmptyColumn))
        {
            AddDiskToCell(x,firstEmptyColumn, TurnManager.Singleton.currentPlayer);
        }

    }
    private void AddDiskToCell(int x,int y,Player player)
    {
        cells[x, y].InsertDisk(player);
        if (!CheckWin())
            TurnManager.Singleton.EndTurn();
    }
    private bool CheckWin()
    {
        return false;
    }
    bool CheckIfColumnHasEmptyCells(int x,out int firstEmptyColumn)
    {
        for (int i = rows-1; i > -1; i--)
        {
            if (!cells[x, i].HasDisk)
            {
                firstEmptyColumn = i;
                return true;
            }
        }
        firstEmptyColumn = -1;
        return false;
    }
}
