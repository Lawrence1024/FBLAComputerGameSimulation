using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        /*PlayButton.GetComponent<Button>().onClick.AddListener(executePlayButton);
        InstructionButton.GetComponent<Button>().onClick.AddListener(executeInstructionButton);
        LeaderBoardButton.GetComponent<Button>().onClick.AddListener(executeLeaderBoardButton);
        ExistButton.GetComponent<Button>().onClick.AddListener(executeExistButton);
        NextPageButton.GetComponent<Button>().onClick.AddListener(executeNextPageButton);
        LastPageButton.GetComponent<Button>().onClick.AddListener(executeLastPageButton);*/

        if (pageCounter <= 0) {
            LastPageButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void executePlayButton() {
        Debug.Log("Play");
        sceneManager.LoadingCanvas.SetActive(true);
        //sceneManager.LoadingCanvas.GetComponent<"LoadingBackground">.GetComponent<Loading>().runLoading("Map");
        sceneManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
        //SceneManager.LoadScene();
    }
    public void executeInstructionButton() {
        Debug.Log("Instruction");
        pageCounter = 0;
        LastPageButton.SetActive(false);
        sceneManager.changeInstrucitonPage(pageCounter);
        sceneManager.InstructionCanvas.SetActive(true);

    }
    public void executeLeaderBoardButton() {
        Debug.Log("Leader Board");

    }
    public void executeExistButton()
    {
        Debug.Log("Exist");
        sceneManager.InstructionCanvas.SetActive(false);

    }
    public void executeNextPageButton()
    {
        pageCounter++;
        Debug.Log("page Counter" + pageCounter + " instructionpages " + sceneManager.InstructionPages.Length);
        if (pageCounter > 0)
        {
            LastPageButton.SetActive(true);
        }
        if (pageCounter== (sceneManager.InstructionPages.Length-1)) {
            NextPageButton.SetActive(false);
        }
        sceneManager.changeInstrucitonPage(pageCounter);
        Debug.Log("Next");

    }
    public void executeLastPageButton()
    {
        pageCounter--;
        if (pageCounter <= 0)
        {
            LastPageButton.SetActive(false);
        }
        if (pageCounter < sceneManager.InstructionPages.Length-1)
        {
            NextPageButton.SetActive(true);
        }
        sceneManager.changeInstrucitonPage(pageCounter);

        Debug.Log("Last");

    }
    public void quitProgram()
    {
        Debug.Log("clicked");
        Application.Quit();
    }

    public void resumeGame() {
        sceneManager.PauseMenuCanvas.SetActive(false);
    }
}
