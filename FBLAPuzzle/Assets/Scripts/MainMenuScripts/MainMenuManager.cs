using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject MainPageCanvas;
    public GameObject InstructionCanvas;
    public GameObject LeaderBoardCanvas;
    public GameObject UserNameInputBoxCanvas;
    public GameObject LogOutButton;
    public GameObject[] InstructionPages;
    public GameObject[] buttonsToDisableOnWarning;
    // Start is called before the first frame update
    void Start()
    {
        LoadingCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        LeaderBoardCanvas.SetActive(false);
        MainPageCanvas.SetActive(true);
        //get the new account box and warnings and deactivate 
        UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(3).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(4).gameObject.SetActive(false);
        /*UserNameInputBoxCanvas.transform.GetChild(4).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(5).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(6).gameObject.SetActive(false);*/
        for (int i = 0; i < InstructionPages.Length; i++)
        {
            InstructionPages[i].SetActive(false);
        }
        GameObject.Find("Counter").GetComponent<EnterMainMenuCounter>().IncreaseMainMenuCounter();
        if (GameObject.Find("Counter").GetComponent<EnterMainMenuCounter>().mainMenuCounter > 1)
        {
            Debug.Log("Counter " + GameObject.Find("Counter").GetComponent<EnterMainMenuCounter>().mainMenuCounter);
            LogOutButton.SetActive(true);
        }
        else {
            LogOutButton.SetActive(false);
        }
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
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(0.25f);
        }
        else
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        }
    }
    public void changeInstrucitonPage(int pageNum)
    {
        for (int i = 0; i < InstructionPages.Length; i++)
        {
            InstructionPages[i].SetActive(false);
        }
        InstructionPages[pageNum].SetActive(true);
    }
    //reference in button
    public void createAccount()
    {
        Debug.Log("Click on hyperlink");
        UserNameInputBoxCanvas.transform.GetChild(2).gameObject.SetActive(true);
        UserNameInputBoxCanvas.transform.GetChild(0).gameObject.SetActive(false);
        UserNameInputBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);

    }
}
