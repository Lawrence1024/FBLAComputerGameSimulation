using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class T_FeatureButtonDetection : MonoBehaviour
{
    T_LevelManager levelManager;
    Loading loading;
    public GameObject tipButton;
    public GameObject gameCanvas;
    public GameObject[] buttons;

    public GameObject TCanvas;
    public T_TutorialFlowController TFController;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        loading = levelManager.LoadingCanvas.transform.GetChild(0).GetComponent<Loading>();
        tipButton.SetActive(false);
        TFController = TCanvas.GetComponent<T_TutorialFlowController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void activateTips()
    {
        StartCoroutine(levelManager.displayMessage("There is no solution video for the tutorial",1.5f));
    }

    public void executeExistButton(GameObject existObj)
    {
        existObj.SetActive(false);
    }


    public void selectAnswer(GameObject answerButton)
    {
        if(TFController.currentStep>=18 && TFController.currentStep <= 20 || TFController.currentStep==44)
        {
            TFController.nextStep();
        }
        if (answerButton.GetComponent<ButtonRightOrWrong>().RightOrWrong == "wrong")
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playWrongSound();
            answerButton.GetComponent<Image>().color = new Vector4(1, 0.39f, 0.39f, 1);
            answerButton.GetComponent<Button>().interactable = false;
            levelManager.minusHeart();
        }
        else
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playCorrectSound();
            answerButton.GetComponent<Image>().color = new Vector4(0.39f, 1, 0.39f, 1);
            StartCoroutine(buffer());
        }
    }
    public void lastStep()
    {
        gameCanvas.GetComponent<T_PiecePosition>().whenHitBackButton();
        if (TFController.currentStep == 22)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            TFController.nextStep();
        }
    }
    public void resetBoard()
    {
        gameCanvas.GetComponent<T_PiecePosition>().whenHitResetButton();
        if (TFController.currentStep == 24)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            TFController.nextStep();
        }
    }

    public void goMap()
    {
        Time.timeScale = 1;
        levelManager.LoadingCanvas.SetActive(true);
        loading.runLoading("Map");
    }
    public void nextScene(string nextSceneName)
    {
        Time.timeScale = 1;
        levelManager.LoadingCanvas.SetActive(true);
        loading.runLoading(nextSceneName);

    }

    IEnumerator buffer()
    {
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
        //idk what happened to detecting butten with tag but it's broken so i'll just hard code
        gameObject.GetComponent<T_LevelManager>().currentQuestionBox.GetComponent<T_BoxController>().answerCorrect();
        levelManager.QuestionCanvas.SetActive(false);
        GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = true;
    }

    public void quitProgram()
    {
        Application.Quit();
    }

    public void resumeGame()
    {
        levelManager.PauseMenuCanvas.SetActive(false);
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = true;
        GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>().stopTime = false;
    }
    public void switchToMainMenu()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (GameObject.Find("AccountsManager") != null)
            {
                GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.tutorialProgress[0] = false;
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
        if (SceneManager.GetActiveScene().name != "Map")
        {
            if (GameObject.Find("AccountsManager") != null)
            {
                GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.tutorialProgress[0] = false;
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
