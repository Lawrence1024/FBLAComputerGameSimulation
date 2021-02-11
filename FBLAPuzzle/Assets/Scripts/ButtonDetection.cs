using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ButtonDetection : MonoBehaviour
{
    sceneManager sceneManager;
    public GameObject PlayButton;
    public GameObject InstructionButton;
    public GameObject LeaderBoardButton;
    public GameObject ExistButton;
    public GameObject NextPageButton;
    public GameObject LastPageButton;

    int pageCounter = 0;
    //public GameObject PlayButton;
    //public GameObject PlayButton;
    //public GameObject PlayButton;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<sceneManager>();
        PlayButton.GetComponent<Button>().onClick.AddListener(executePlayButton);
        InstructionButton.GetComponent<Button>().onClick.AddListener(executeInstructionButton);
        LeaderBoardButton.GetComponent<Button>().onClick.AddListener(executeLeaderBoardButton);
        ExistButton.GetComponent<Button>().onClick.AddListener(executeExistButton);
        NextPageButton.GetComponent<Button>().onClick.AddListener(executeNextPageButton);
        LastPageButton.GetComponent<Button>().onClick.AddListener(executeLastPageButton);

        if (pageCounter <= 0) {
            LastPageButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void executePlayButton() {
        Debug.Log("Play");
    }
    void executeInstructionButton() {
        Debug.Log("Instruction");
        pageCounter = 0;
        sceneManager.changeInstrucitonPage(pageCounter);
        sceneManager.InstructionCanvas.SetActive(true);

    }
    void executeLeaderBoardButton() {
        Debug.Log("Leader Board");

    }
    void executeExistButton()
    {
        Debug.Log("Exist");
        sceneManager.InstructionCanvas.SetActive(false);

    }
    void executeNextPageButton()
    {
        pageCounter++;
        Debug.Log("page Counter" + pageCounter + " instructionpages " + sceneManager.InstructionPages.Length);
        if (pageCounter > 0)
        {
            LastPageButton.SetActive(true);
        } else if (pageCounter>= (sceneManager.InstructionPages.Length-2)) {
            NextPageButton.SetActive(false);
        }
        sceneManager.changeInstrucitonPage(pageCounter);
        Debug.Log("Next");

    }
    void executeLastPageButton()
    {
        pageCounter--;
        if (pageCounter <= 0)
        {
            LastPageButton.SetActive(false);
        }
        else if (pageCounter < sceneManager.InstructionPages.Length-2)
        {
            NextPageButton.SetActive(true);
        }
        sceneManager.changeInstrucitonPage(pageCounter);

        Debug.Log("Last");

    }

}
