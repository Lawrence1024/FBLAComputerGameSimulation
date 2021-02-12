using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject MainPageCanvas;
    public GameObject InstructionCanvas;
    public GameObject LeaderBoardCanvas;
    public GameObject[] InstructionPages;
    // Start is called before the first frame update
    void Start()
    {
        LoadingCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        LeaderBoardCanvas.SetActive(false);
        MainPageCanvas.SetActive(true);
        for (int i = 0; i < InstructionPages.Length; i++) {
            InstructionPages[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            activatePauseMenu();
        }
    }
    void activatePauseMenu() {
        PauseMenuCanvas.SetActive(!PauseMenuCanvas.activeSelf);
    }
    public void changeInstrucitonPage(int pageNum) {
        for (int i = 0; i < InstructionPages.Length; i++)
        {
            InstructionPages[i].SetActive(false);
        }
        InstructionPages[pageNum].SetActive(true);
    }
   
}
