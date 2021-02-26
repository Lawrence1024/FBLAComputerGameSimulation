using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FeatureButtonDetection : MonoBehaviour
{
    LevelManager levelManager;
    Loading loading;
    public GameObject tipButton;
    public GameObject gameCanvas;
    public GameObject[] buttons;

    GameObject showScoreBoardData;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        loading = levelManager.LoadingCanvas.transform.GetChild(0).GetComponent<Loading>();
        tipButton.SetActive(false);
        showScoreBoardData = levelManager.CelebratoryMessagesCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activateTips() {
        levelManager.TipsCanvas.SetActive(true);
    }
    
    public void executeExistButton(GameObject existObj)
    {
        existObj.SetActive(false);

    }


    public void selectAnswer(GameObject answerButton) {
        if (answerButton.GetComponent<ButtonRightOrWrong>().RightOrWrong == "wrong")
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playWrongSound();
            answerButton.GetComponent<Image>().color = new Vector4(1,0.39f,0.39f,1);
            answerButton.GetComponent<Button>().interactable = false;
            levelManager.minusHeart();
        }
        else {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playCorrectSound();
            answerButton.GetComponent<Image>().color = new Vector4(0.39f, 1, 0.39f, 1);
            StartCoroutine(buffer());
        }
    }
    public void lastStep()
    {
        if (GameObject.Find("Player").GetComponent<PlayerController>().movePointCloseEnough)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            gameCanvas.GetComponent<PiecePosition>().whenHitBackButton();
        }
        
    }
    public void resetBoard()
    {
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
        gameCanvas.GetComponent<PiecePosition>().whenHitResetButton();
    }

    public void goMap() {
        showScoreBoardData.SetActive(false);
        levelManager.LoadingCanvas.SetActive(true);
        loading.runLoading("Map");
    }
    public void nextScene(string nextSceneName) {
        showScoreBoardData.SetActive(false);
        levelManager.LoadingCanvas.SetActive(true);
        loading.runLoading(nextSceneName);
    }

    IEnumerator buffer() {
        levelManager.QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        levelManager.QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        levelManager.QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        levelManager.QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(.5f);
        levelManager.QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        levelManager.QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
        levelManager.QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
        levelManager.QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = true;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
        gameObject.GetComponent<LevelManager>().currentQuestionBox.GetComponent<BoxController>().answerCorrect();
        levelManager.QuestionCanvas.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
    }

    public void quitProgram()
    {
        Application.Quit();
    }

    public void resumeGame()
    {
        levelManager.PauseMenuCanvas.SetActive(false);
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("PointsValue").GetComponent<PointsCalculation>().gamePause = false;
        StartCoroutine(GameObject.Find("PointsValue").GetComponent<PointsCalculation>().pointsCountDown());
    }
    public void switchToMainMenu()
    {
        showScoreBoardData.SetActive(false);
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (GameObject.Find("AccountsManager") != null)
            {
                GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.potentialStarsList[levelManager.level[0] * 3 + levelManager.level[1] - 4] = 3;
            }
            levelManager.LoadingCanvas.SetActive(true);
            levelManager.PauseMenuCanvas.SetActive(false);
            levelManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
        }
        else
        {
            levelManager.PauseMenuCanvas.SetActive(false);
        }
    }
    public void switchToMap()
    {
        showScoreBoardData.SetActive(false);
        if (SceneManager.GetActiveScene().name != "Map")
        {
            if (GameObject.Find("AccountsManager") != null)
            {
                GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.potentialStarsList[levelManager.level[0] * 3 + levelManager.level[1] - 4] = 3;
            }
            levelManager.LoadingCanvas.SetActive(true);
            levelManager.PauseMenuCanvas.SetActive(false);
            levelManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
        }
        else
        {
            levelManager.PauseMenuCanvas.SetActive(false);
        }
    }

}
