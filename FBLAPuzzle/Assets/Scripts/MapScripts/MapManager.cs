using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject MapCanvas;
    void Start()
    {
        LoadingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
