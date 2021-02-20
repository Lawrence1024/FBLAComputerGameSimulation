using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
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
    public GameObject[] TipPages;

    private Account activeAccount;
    public List<int> level;

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
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        //This is the max star
        //activeAccount.potentialStarsList[level[0] * 3 + level[1] - 4];
        displayStars("Stars");
    }

    // Update is called once per frame
    void Update()
    {

        //testing------------
        if (Input.GetKeyDown("space")) {
            QuestionCanvas.SetActive(!QuestionCanvas.activeSelf);
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
            StartCoroutine(resetLevel());
            
        }
        
    }
    public void displayScoreboard()
    {
        //FeatureCanvas.active=false;
        Time.timeScale = 0;
        ScoreboardCanvas.SetActive(true);
    }
    public void displayScore() {
        //GameObject.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text();
        Debug.Log(GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text);
        GameObject.Find("FinalPoints").GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("Points").GetComponent<TMPro.TextMeshProUGUI>().text;
        displayStars("FinalStarDisplay");
    }
    IEnumerator resetLevel() {

        yield return new WaitForSeconds(.5f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
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

}
