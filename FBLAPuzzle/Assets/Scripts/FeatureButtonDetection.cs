using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureButtonDetection : MonoBehaviour
{
    LevelManager levelManager;
    public GameObject tipButton;
    public GameObject NextPageButton;
    public GameObject LastPageButton;
    public GameObject gameCanvas;
    int pageCounter = 0;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        tipButton.SetActive(false);
        if (pageCounter <= 0)
        {
            LastPageButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activateTips() {
        Debug.Log("Tips"); 
        pageCounter = 0;
        LastPageButton.SetActive(false);
        levelManager.changeInstrucitonPage(pageCounter);
        levelManager.InstructionCanvas.SetActive(true);
    }
    public void executeNextPageButton()
    {
        pageCounter++;
        Debug.Log("page Counter" + pageCounter + " instructionpages " + levelManager.TipPages.Length);
        if (pageCounter > 0)
        {
            LastPageButton.SetActive(true);
        }
        if (pageCounter == (levelManager.TipPages.Length - 1))
        {
            NextPageButton.SetActive(false);
        }
        levelManager.changeInstrucitonPage(pageCounter);
        Debug.Log("Next");

    }
    public void executeLastPageButton()
    {
        pageCounter--;
        if (pageCounter <= 0)
        {
            LastPageButton.SetActive(false);
        }
        if (pageCounter < levelManager.TipPages.Length - 1)
        {
            NextPageButton.SetActive(true);
        }
        levelManager.changeInstrucitonPage(pageCounter);

        Debug.Log("Last");

    }
    public void executeExistButton(GameObject existObj)
    {
        Debug.Log("Exist");
        existObj.SetActive(false);

    }


    public void selectAnswer(GameObject answerButton) {
        if (answerButton.GetComponent<ButtonRightOrWrong>().RightOrWrong == "wrong")
        {
            Debug.Log("selectAnswer wrong");
        }
        else {
            Debug.Log("selectAnswer right");
            //levelManager.hideCanvas(levelManager.QuestionCanvas);
            levelManager.QuestionCanvas.SetActive(false);
            //Time.timeScale = 1;
        }
    }
    public void lastStep()
    {
        gameCanvas.GetComponent<PiecePosition>().whenHitBackButton();
    }
    public void resetBoard()
    {
        gameCanvas.GetComponent<PiecePosition>().whenHitResetButton();
    }
}
