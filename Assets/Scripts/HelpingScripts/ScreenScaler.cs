using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
    [SerializeField] int width, height;
    private void Awake()
    {
        Screen.SetResolution(width, height, true);
    }
}
