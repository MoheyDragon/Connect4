using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell:MonoBehaviour
{
    public CellCoordinates coordinates;
    private int currentPlayerDiskOccupying;
    [SerializeField] Button button;
    [SerializeField] Image diskImage;
    public bool HasDisk => currentPlayerDiskOccupying >-1;
    public bool HasSamePlayerIndex(int playerIndex) => playerIndex == currentPlayerDiskOccupying;
    public void SetupCell(int x,int y)
    {
        coordinates.x = x;
        coordinates.y = y;
        currentPlayerDiskOccupying = -1;
        button.onClick.AddListener(ClickedOnColumn);
    }
    public void InsertDisk(Player player)
    {
        currentPlayerDiskOccupying = player.Index;
    }
    private void ClickedOnColumn()
    {
        CellsOccupancyManager.Singleton.ClickingOnColumn(coordinates.x);
    }
    public void VisuallyInsertingDisk(Color playerColor)
    {
        diskImage.enabled = true;
        diskImage.color = playerColor;
    }
}
