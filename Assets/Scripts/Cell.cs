using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell:MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button button;
    public int x, y;
    private int currentPlayerDiskOccupying;
    [SerializeField] Image diskImage;
    public bool HasDisk => currentPlayerDiskOccupying >-1;
    public void InsertDisk(Player player)
    {
        currentPlayerDiskOccupying = player.Index;
        VisuallyInsertingDisk(player.playerColor);
    }
    private void VisuallyInsertingDisk(Color playerColor)
    {
        diskImage.enabled = true;
        diskImage.color = playerColor;
    }
    public void AssignCoordinates(int x,int y)
    {
        this.x = x;
        this.y = y;
        text.text = x + " , " + y;
        currentPlayerDiskOccupying = -1;
        button.onClick.AddListener(ClickedOnColumn);
    }
    private void ClickedOnColumn()
    {
        CellsOccupancyManager.Singleton.ClickingOnColumn(x);
    }
}
