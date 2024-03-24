using UnityEngine;
using TMPro;

public class EmptyCell:MonoBehaviour
{
    public TextMeshProUGUI text;
    public int x, y;
    public void AssignCoordinates(int x,int y)
    {
        
        this.x = x;
        this.y = y;
        text.text = x + " , " + y;
    }
}
