using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FeatureButtonDetection : MonoBehaviour
{
    LevelManager levelManager;
    public GameObject tipButton;
    public GameObject gameCanvas;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        tipButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activateTips() {
        Debug.Log("Tips"); 
        levelManager.TipsCanvas.SetActive(true);
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
            levelManager.minusHeart();
        }
        else {
            Debug.Log("selectAnswer right");
            //levelManager.hideCanvas(levelManager.QuestionCanvas);
            gameObject.GetComponent<LevelManager>().currentQuestionBox.GetComponent<BoxController>().answerCorrect();
            levelManager.QuestionCanvas.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;

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

    public void goMap() {
        SceneManager.LoadScene("Map");
    }


}
