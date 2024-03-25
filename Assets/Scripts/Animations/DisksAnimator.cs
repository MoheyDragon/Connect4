using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisksAnimator : Singletons<DisksAnimator>
{
    [SerializeField] Transform[] playersDisk;
    [SerializeField] AK.Wwise.Event dropDiskSound;
    public void DropDiskAnimation(Cell targetCell)
    {
        Vector3 targetPosition = targetCell.transform.position;
        int targetRow = targetCell.coordinates.y;
        Transform disk = playersDisk[TurnManager.Singleton.currentPlayer.Index];
        disk.position = targetPosition + (targetRow + 1) * 100 * Vector3.up;
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

}
