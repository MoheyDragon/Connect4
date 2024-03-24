using UnityEngine;
using UnityEngine.UI;
public class GridBuilder : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] Cell prefab; 
    [SerializeField] int rows, columns;
    void Start()
    {
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        if (gridLayout.transform.childCount>0)
        {
            foreach (Transform cell in gridLayout.transform)
            {
                Destroy(cell.gameObject);
            }
        }
        gridLayout.constraintCount = rows;
        Cell[,] cells = new Cell[columns,rows];
        for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            Cell newCell = Instantiate(prefab, gridLayout.transform);
            cells[j, i] = newCell;
            newCell.AssignCoordinates(j, i);
        }
    }
        CellsOccupancyManager.Singleton.SetCellsData(cells,rows,columns);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateGrid();
        }
    }
}
