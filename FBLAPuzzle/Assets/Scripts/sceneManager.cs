using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject MainPageCanvas;
    public GameObject InstructionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        LoadingCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        MainPageCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void activatePauseMenu() { 
        
    }

}
