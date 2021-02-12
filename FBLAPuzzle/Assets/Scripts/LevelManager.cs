using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject LevelCanvas;
    public GameObject InstructionCanvas;
    public GameObject ScoreboardCanvas;
    public GameObject[] TipPages;
    // Start is called before the first frame update
    void Start()
    {
        LoadingCanvas.SetActive(false);
       // PauseMenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        ScoreboardCanvas.SetActive(false);
        LevelCanvas.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeInstrucitonPage(int pageNum)
    {
        for (int i = 0; i < TipPages.Length; i++)
        {
            TipPages[i].SetActive(false);
        }
        TipPages[pageNum].SetActive(true);
    }
}
