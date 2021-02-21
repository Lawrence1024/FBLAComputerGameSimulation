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
    //public string nextSceneName;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        loading = levelManager.LoadingCanvas.transform.GetChild(0).GetComponent<Loading>();
        tipButton.SetActive(false);
        //buttons = GameObject.FindGameObjectsWithTag("Buttons");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void activateTips()
    {
        Debug.Log("Tips");
        levelManager.TipsCanvas.SetActive(true);
    }

    public void executeExistButton(GameObject existObj)
    {
        Debug.Log("Exist");
        existObj.SetActive(false);

    }


    public void selectAnswer(GameObject answerButton)
    {
        if (answerButton.GetComponent<ButtonRightOrWrong>().RightOrWrong == "wrong")
        {
            Debug.Log("selectAnswer wrong");
            answerButton.GetComponent<Image>().color = new Vector4(1, 0.39f, 0.39f, 1);
            answerButton.GetComponent<Button>().interactable = false;
            levelManager.minusHeart();
        }
        else
        {
            Debug.Log("selectAnswer right");
            //levelManager.hideCanvas(levelManager.QuestionCanvas);
            answerButton.GetComponent<Image>().color = new Vector4(0.39f, 1, 0.39f, 1);
            StartCoroutine(buffer());

            //Time.timeScale = 1;
        }
    }
    public void lastStep()
    {
        gameCanvas.GetComponent<T_PiecePosition>().whenHitBackButton();
    }
    public void resetBoard()
    {
        gameCanvas.GetComponent<T_PiecePosition>().whenHitResetButton();
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
        Debug.Log("Buffer");
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
            Debug.Log("button interactable");
        }
        //idk what happened to detecting butten with tag but it's broken so i'll just hard code
        //GameObject.Find("TipButton").GetComponent<Button>().interactable = true;
        gameObject.GetComponent<T_LevelManager>().currentQuestionBox.GetComponent<T_BoxController>().answerCorrect();
        levelManager.QuestionCanvas.SetActive(false);
        GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = true;
    }
}
