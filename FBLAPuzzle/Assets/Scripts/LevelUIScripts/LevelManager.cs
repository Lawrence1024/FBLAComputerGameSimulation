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
    IEnumerator resetLevel() {

        yield return new WaitForSeconds(.5f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
