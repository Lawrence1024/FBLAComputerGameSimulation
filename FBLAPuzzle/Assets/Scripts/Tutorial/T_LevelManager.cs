using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class T_LevelManager : MonoBehaviour
{
    T_PointsCalculation pointsCalculation;
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject LevelCanvas;
    //public GameObject InstructionCanvas;
    public GameObject TipsCanvas;
    public GameObject ScoreboardCanvas;
    public GameObject QuestionCanvas;
    public GameObject WarningCanvas;
    public GameObject FeatureCanvas;
    public GameObject currentQuestionBox;
    public GameObject playerSprite;
    public GameObject[] TipPages;

    public List<int> level;
    private Account activeAccount;
    private bool freezeState = false;
    private GameObject[] buttons;

    public GameObject TCanvas;
    public T_TutorialFlowController TFController;

    public bool garunteeCorrect = false;


    // Start is called before the first frame update
    void Start()
    {
        LoadingCanvas.SetActive(false);
        // PauseMenuCanvas.SetActive(false);
        TipsCanvas.SetActive(false);
        ScoreboardCanvas.SetActive(false);
        QuestionCanvas.SetActive(false);
        WarningCanvas.SetActive(false);
        LevelCanvas.SetActive(true);
        PauseMenuCanvas.SetActive(false);
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        //This is the max star
        //activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];
        pointsCalculation = GameObject.Find("PointsValue").GetComponent<T_PointsCalculation>();
        buttons = GameObject.FindGameObjectsWithTag("Buttons");
        TFController = TCanvas.GetComponent<T_TutorialFlowController>();
        displayStars("Stars");
        playerSprite.GetComponent<SpriteRenderer>().color = activeAccount.avatarColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            activatePauseMenu();
        }
    }
    void activatePauseMenu()
    {
        PauseMenuCanvas.SetActive(!PauseMenuCanvas.activeSelf);
        if (PauseMenuCanvas.activeSelf)
        {
            GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = false;
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(0.25f);
            pointsCalculation.stopTime = true;
        }
        else
        {
            GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = true;
            pointsCalculation.stopTime = false;
            StartCoroutine(GameObject.Find("PointsValue").GetComponent<PointsCalculation>().pointsCountDown());
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        }
    }
    public void changeInstrucitonPage(int pageNum)
    {
        for (int i = 0; i < TipPages.Length; i++)
        {
            TipPages[i].SetActive(false);
        }
        TipPages[pageNum].SetActive(true);
    }
    public void minusHeart()
    {
        //Debug.Log("hearts " + GameObject.Find("Hearts").transform.GetChild(0).gameObject.activeSelf);
        if (GameObject.Find("Hearts").transform.childCount > 1)
        {
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);
            activeAccount.tutorialFeatures[0]--;
            if (activeAccount.tutorialFeatures[1] == 1 && activeAccount.tutorialFeatures[0]==1)
            {
                garunteeCorrect = true;
                doGarunteeCorrect();
                TFController.instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "For the purpose of the tutorial, we will eliminate the wrong answers for you!";
            }
        }
        else if (GameObject.Find("Hearts").transform.childCount <= 1)
        {
            //if (0 > activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] - 1)
            //{
            //    activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] = 0;
            //}
            //else
            //{
            //    activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] - 1;
            //}
            //activeAccount.saveAccount();
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);
            
            if (activeAccount.tutorialFeatures[1] > 0)
            {
                activeAccount.tutorialFeatures[1] --;
            }
            if (TFController.currentStep > 40)
            {
                if (activeAccount.tutorialFeatures[1] == 2)
                {
                    string s="You would only gain a maximum of 2 star in a real game, try to complete level without answering wrong!";
                    TFController.instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = s;
                    reloadScenePrep();
                    StartCoroutine(loadWarning("Level restart in", 3));
                }
                else if (activeAccount.tutorialFeatures[1] == 1)
                {
                    string s = "You would only gain a maximum of 1 star in a real game, try to complete level without answering wrong!";
                    TFController.instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = s; 
                    reloadScenePrep();
                    StartCoroutine(loadWarning("Level restart in", 3));
                }
                else if (activeAccount.tutorialFeatures[1] == 0)
                {
                    Debug.Log("Died after the tutorial?");
                }
            }
            else
            {
                if (activeAccount.tutorialFeatures[1] == 2)
                {
                    activeAccount.tutorialProgress[0] = true;
                }
            }
            activeAccount.tutorialFeatures[0] = 3;
            reloadScenePrep();

            //if (starsRemain == 2 && TFController.currentStep==29)
            //{
            //    Debug.Log("SHOW THE MINUS STAR");
            //    TFController.nextStep();
            //}

            //        StartCoroutine(loadWarning("Level restart in", 3));


        }

    }
    //scene reload preperation
    public void reloadScenePrep()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
            Debug.Log("button uninteractable");
        }
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        pointsCalculation.enabled = false;
    }
    public void displayScoreboard()
    {
        //FeatureCanvas.active=false;
        pointsCalculation.levelComplete = true;
        ScoreboardCanvas.SetActive(true);
    }
    public void displayScore()
    {
        //GameObject.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text();
        Debug.Log(GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text);
        GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text = "Points: " + GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text;
        displayStars("FinalStarDisplay");
    }
    public void displayStars(string holderName)
    {
        int starsRemain = activeAccount.tutorialFeatures[1];
        GameObject.Find(holderName).transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(2).gameObject.SetActive(false);
        for (int i = 0; i < starsRemain; i++)
        {
            GameObject.Find(holderName).transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public IEnumerator buffer()
    {
        pointsCalculation.levelComplete = true;
        activeAccount.tutorialProgress[1] = true;
        activeAccount.tutorialProgress[0] = false;
        Debug.Log("SHOW CONGRATS FINISH TUTORIAL");
        //activeAccount.starsList[level[0] * 3 + level[1] - 4] = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];
        //if (activeAccount.pointsList[level[0] * 3 + level[1] - 4] < int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text))
        //{
        //    activeAccount.pointsList[level[0] * 3 + level[1] - 4] = int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text);
        //}
        //activeAccount.saveAccount();


        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1f);
        //    displayScoreboard();
        //    displayScore();
        //getAccountsPoints
        //ScoreboardCanvas.GetComponent<ScoreBoardDisplay>().getAccountsPoints(level);
        //ScoreboardCanvas.GetComponent<ScoreBoardDispaly>().getAccountsPoints(level);
        //    ScoreboardCanvas.GetComponent<ShowScoreBoardData>().getAccountsPoints(level);
        StartCoroutine(loadWarning("You are ready to move on! Loading Main Menu in ", 5));
    }

    public IEnumerator loadWarning(string warningMessage, float sec)
    {
        if (sec < 1 && sec != 0)
        {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage;

        }
        else
        {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage + " " + sec;

        }
        WarningCanvas.SetActive(true);
        if (sec < 1 && sec != 0)
        {
            yield return new WaitForSeconds(sec);
            WarningCanvas.SetActive(false);
        }
        else if (sec >= 1 || sec <= 0)
        {
            yield return new WaitForSeconds(1);
            sec--;
            Debug.Log("sec " + sec);
            if (sec <= 0 && warningMessage == "Level restart in")
            {

                WarningCanvas.SetActive(false);
                reloadScene();
            } else if (sec <= 0) {
                LoadingCanvas.SetActive(true);
                GameObject.Find("LoadingBackground").GetComponent<Loading>().runLoading("MainMenu");

            }
            else if (sec > 0)
            {
                StartCoroutine(loadWarning(warningMessage, sec));
            }

        }

    }
    void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        /*for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        pointsCalculation.enabled = false;*/
        SceneManager.LoadScene(scene.name);
    }
    public void doGarunteeCorrect()
    {
        QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
    }
}
