using UnityEngine;
using UnityEngine.UI;
public class GridBuilder : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] Cell prefab;
    int rows = 6, columns = 7;
    void Start()
    {
        GenerateGridAndOccupyCellsData();
    }
    public void GenerateGridAndOccupyCellsData()
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
        CellsOccupancyManager.Singleton.SetCellsData(cells, rows, columns);
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
