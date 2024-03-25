using UnityEngine;
using UnityEngine.UI;
public class GridBuilder : Singletons<GridBuilder>
{
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] Cell prefab;
    int rows = 6, columns = 7;
    void Start()
    {
        GenerateGridAndOccupyCellsData();
    }
    public void GenerateGridByUserValues(int rows,int columns,int winningCount)
    {
        this.rows = rows;
        this.columns = columns;
        GenerateGridAndOccupyCellsData(winningCount);
    }
    private void GenerateGridAndOccupyCellsData(int winningCount=4)
    {
        _DeletPreviousGrid();

        gridLayout.constraintCount = rows;
        Cell[,] cells = new Cell[columns, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Cell newCell = Instantiate(prefab, gridLayout.transform);
                cells[j, i] = newCell;
                newCell.SetupCell(j, i);
            }
        }
        CellsOccupancyManager.Singleton.SetCellsData(cells, rows, columns,winningCount);
    }
    private void _DeletPreviousGrid()
    {
        if (gridLayout.transform.childCount > 0)
        {
            foreach (Transform cell in gridLayout.transform)
            {
                Destroy(cell.gameObject);
            }
        }
    }
}
