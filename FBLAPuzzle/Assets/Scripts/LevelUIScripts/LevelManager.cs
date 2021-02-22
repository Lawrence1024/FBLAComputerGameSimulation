using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    PointsCalculation pointsCalculation;
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject LevelCanvas;
    //public GameObject InstructionCanvas;
    public GameObject TipsCanvas;
    public GameObject ScoreboardCanvas;
    public GameObject QuestionCanvas;
    public GameObject WarningCanvas;
    public GameObject FeatureCanvas;
    public GameObject CelebratoryMessagesCanvas;
    public GameObject currentQuestionBox;
    public GameObject playerSprite;
    public GameObject[] TipPages;

    public List<int> level;
    private Account activeAccount;
    private bool freezeState = false;
    private GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        LoadingCanvas.SetActive(false);
        // PauseMenuCanvas.SetActive(false);
        TipsCanvas.SetActive(false);
        ScoreboardCanvas.SetActive(false);
        QuestionCanvas.SetActive(false);
        WarningCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        LevelCanvas.SetActive(true);
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        //This is the max star
        //activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];
        pointsCalculation = GameObject.Find("PointsValue").GetComponent<PointsCalculation>();
        buttons = GameObject.FindGameObjectsWithTag("Buttons");
        displayStars("Stars");
        playerSprite.GetComponent<SpriteRenderer>().color = activeAccount.avatarColor;
    }

    // Update is called once per frame
    //Add this part--------------------------------------------------------------------
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
        if (PauseMenuCanvas.activeSelf) {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(0.25f);
            pointsCalculation.gamePause = true;
        }
        else{
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            pointsCalculation.gamePause = false;
            StartCoroutine(GameObject.Find("PointsValue").GetComponent<PointsCalculation>().pointsCountDown());
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        }
    }
    //End here--------------------------------------------------------------------
    public void changeInstrucitonPage(int pageNum)
    {
        for (int i = 0; i < TipPages.Length; i++)
        {
            TipPages[i].SetActive(false);
        }
        TipPages[pageNum].SetActive(true);
    }
    public void minusHeart() {
        //Debug.Log("hearts " + GameObject.Find("Hearts").transform.GetChild(0).gameObject.activeSelf);
        if (GameObject.Find("Hearts").transform.childCount>1) {
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);
        }
        else if(GameObject.Find("Hearts").transform.childCount <=1)
        {
            if(0 > activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] - 1)
            {
                activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] = 0;
            }
            else
            {
                activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4] - 1;
            }
            activeAccount.saveAccount();
            //SceneManager.LoadScene(SceneManager.GetActiveScene());
            GameObject.Find("Hearts").transform.GetChild(0).gameObject.SetActive(false);
            Destroy(GameObject.Find("Hearts").transform.GetChild(0).gameObject);

            //scene reload
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
                Debug.Log("button uninteractable");
            }
            QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
            QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
            QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
            QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
            pointsCalculation.gamePause = true;
            StartCoroutine(loadWarning("Level restart in", 3));
            
            
        }
        
    }
    public void displayScoreboard()
    {
        //FeatureCanvas.active=false;
        //Time.timeScale = 0;
        pointsCalculation.levelComplete = true;
        ScoreboardCanvas.SetActive(true);
    }
    public void displayScore() {
        //GameObject.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text();
        Debug.Log(GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text);
        GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text = "Points: "+GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text;
        displayStars("FinalStarDisplay");
    }
    public void displayStars(string holderName) {
        int starsRemain = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];
        GameObject.Find(holderName).transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find(holderName).transform.GetChild(2).gameObject.SetActive(false);
        for (int i = 0; i < starsRemain; i++) {
            GameObject.Find(holderName).transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public IEnumerator buffer()
    {
        pointsCalculation.levelComplete = true;
        activeAccount.starsList[level[0] * 3 + level[1] - 4] = activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];

        //hard code -1
        if (activeAccount.pointsList[level[0] * 3 + level[1] - 4]< int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text))
        {
            activeAccount.pointsList[level[0] * 3 + level[1] - 4] = int.Parse(GameObject.Find("PointsValue").GetComponent<TMPro.TextMeshProUGUI>().text)-1;
        }
        activeAccount.saveAccount();
        
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1f);
        displayScoreboard();
        displayScore();
        //getAccountsPoints
        //ScoreboardCanvas.GetComponent<ScoreBoardDisplay>().getAccountsPoints(level);
        //ScoreboardCanvas.GetComponent<ScoreBoardDispaly>().getAccountsPoints(level);
        ScoreboardCanvas.GetComponent<ShowScoreBoardData>().getAccountsPoints(level);
    }

    public IEnumerator loadWarning(string warningMessage, float sec)
    {
        if (sec < 1&&sec!=0)
        {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage;

        }
        else {
            WarningCanvas.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = warningMessage+" "+sec;

        }
        WarningCanvas.SetActive(true);
        if (sec < 1 && sec != 0)
        {
            yield return new WaitForSeconds(sec);
            WarningCanvas.SetActive(false);
        }
        else if (sec >= 1||sec<=0) {
            yield return new WaitForSeconds(1);
            sec--;
            Debug.Log("sec " + sec);
            if (sec <= 0) {
                
                WarningCanvas.SetActive(false);
                reloadScene();
            } else if(sec>0){
                StartCoroutine(loadWarning(warningMessage, sec));
            }
            
        }
        
    }
    void reloadScene() {
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


}
