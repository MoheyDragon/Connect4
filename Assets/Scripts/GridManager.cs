using UnityEngine;
using UnityEngine.UI;
public class GridManager : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] EmptyCell prefab; 
    [SerializeField] int rows, coulmns;
    void Start()
    {
        GenerateGrid();
    }
    public int totalCellsCount;
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
        totalCellsCount = rows * coulmns;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coulmns; j++)
            {
                EmptyCell newCell= Instantiate(prefab, gridLayout.transform);
                newCell.AssignCoordinates(i, j);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateGrid();
        }
    }
}
