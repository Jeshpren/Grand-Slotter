using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Fullscreen")]
    public bool fullScreen = true;

    [Header("Resolution")]
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    
    void Start()
    {
        // if (fullScreen)
        //     Screen.SetResolution(resolutionHeight, resolutionWidth, true);
        // else
        //     Screen.SetResolution(resolutionHeight, resolutionWidth, false);

    }
}
