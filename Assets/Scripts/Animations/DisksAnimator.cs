using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class DisksAnimator : Singletons<DisksAnimator>
{
    [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] Transform[] playersDisk;
    [SerializeField] AK.Wwise.Event dropDiskSound;
    public void DropDiskAnimation(Cell targetCell)
    {
        Vector3 targetPosition = targetCell.transform.position;
        Transform disk = playersDisk[TurnManager.Singleton.currentPlayer.Index];
        disk.position = CalculateDropStartPosition(targetCell);
        disk.gameObject.SetActive(true);
        StartCoroutine(CO_DropDiskAnimation(disk,targetPosition,targetCell));
    }

    [SerializeField] float speed = 5;
    IEnumerator CO_DropDiskAnimation(Transform disk, Vector3 targetPosition,Cell cell)
    {
        dropDiskSound.Post(gameObject);
        float distance = Vector3.Distance(disk.position, targetPosition);
        while (distance > 1)
        {
            disk.position = Vector3.MoveTowards(disk.position, targetPosition, speed);
            yield return null;
            distance = Vector3.Distance(disk.position, targetPosition);
        }
        cell.VisuallyInsertingDisk(TurnManager.Singleton.currentPlayer.playerColor);
        disk.gameObject.SetActive(false);
        TurnManager.Singleton.ResultOfTurn(cell.coordinates);
    }
    private Vector3 CalculateDropStartPosition(Cell cell)
    {
        Vector3 topCellPosition = gridLayout.transform.GetChild(0).position;
        float cellHeight = gridLayout.cellSize.y;
        topCellPosition.x = cell.transform.position.x;
        return topCellPosition + Vector3.up* cellHeight;
    }
}
