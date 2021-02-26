﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonDetection : MonoBehaviour
{
    
    MainMenuManager mainMenuManager;
    public GameObject NextPageButton;
    public GameObject LastPageButton;
    public GameObject PageCount;


    public ScrollRect ScrollRect;
    int pageCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        mainMenuManager = GameObject.Find("SceneManager").GetComponent<MainMenuManager>();
        ScrollRect.verticalNormalizedPosition = 1f;
        if (pageCounter <= 0) {
            PageCount.SetActive(true);
            LastPageButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void executePlayButton() {
        mainMenuManager.LoadingCanvas.SetActive(true);
        mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
    }
    public void executeInstructionButton() {
        pageCounter = 0;
        LastPageButton.SetActive(false);
        mainMenuManager.changeInstrucitonPage(pageCounter);
        NextPageButton.SetActive(true);
        PageCount.GetComponent<TMPro.TextMeshProUGUI>().text = (pageCounter + 1) + "/"+ mainMenuManager.InstructionPages.Length;

        PageCount.SetActive(true);
        
        mainMenuManager.InstructionCanvas.SetActive(true);

    }
    public void executeLeaderBoardButton() {
        ScrollRect.verticalNormalizedPosition = 1f;
        mainMenuManager.LeaderBoardCanvas.SetActive(true);
        mainMenuManager.LeaderBoardCanvas.GetComponent<LeaderBoardDisplay>().getAccountsTotalStars();
        

    }
    public void executeExistButton(GameObject existObj)
    {
        existObj.SetActive(false);

    }
    public void executeNextPageButton()
    {
        pageCounter++;
        PageCount.SetActive(true);
        if (pageCounter > 0)
        {
            LastPageButton.SetActive(true);
        }
        if (pageCounter== (mainMenuManager.InstructionPages.Length-1)) {
            NextPageButton.SetActive(false);
        }
        mainMenuManager.changeInstrucitonPage(pageCounter);
        PageCount.GetComponent<TMPro.TextMeshProUGUI>().text = (pageCounter+1)+ "/" +mainMenuManager.InstructionPages.Length;

    }
    public void executeLastPageButton()
    {
        pageCounter--;
        PageCount.SetActive(true);
        if (pageCounter <= 0)
        {
            LastPageButton.SetActive(false);
        }
        if (pageCounter < mainMenuManager.InstructionPages.Length-1)
        {
            NextPageButton.SetActive(true);
        }
        mainMenuManager.changeInstrucitonPage(pageCounter);
        PageCount.GetComponent<TMPro.TextMeshProUGUI>().text = (pageCounter + 1) + "/" + mainMenuManager.InstructionPages.Length;

    }
    public void quitProgram()
    {
        Application.Quit();
    }

    public void resumeGame() {
        mainMenuManager.PauseMenuCanvas.SetActive(false);
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
    }
    public void switchToMainMenu() {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            mainMenuManager.LoadingCanvas.SetActive(true);
            mainMenuManager.PauseMenuCanvas.SetActive(false);
            mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
        }
        else {
            mainMenuManager.PauseMenuCanvas.SetActive(false);
        }
    }
    public void switchToMap()
    {
        if (SceneManager.GetActiveScene().name != "Map")
        {
            mainMenuManager.LoadingCanvas.SetActive(true);
            mainMenuManager.PauseMenuCanvas.SetActive(false);
            mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
        }
        else
        {
            mainMenuManager.PauseMenuCanvas.SetActive(false);
        }
    }

    public void LogOut(GameObject LOBut) {
        mainMenuManager.PauseMenuCanvas.SetActive(false);
        mainMenuManager.InstructionCanvas.SetActive(false);
        mainMenuManager.LoadingCanvas.SetActive(false);
        mainMenuManager.LeaderBoardCanvas.SetActive(false);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("AccountsManager").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Button>().interactable=true;
        LOBut.SetActive(false);
    }
    public void TutorialTransition() {
        mainMenuManager.LoadingCanvas.SetActive(true);
        mainMenuManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Tutorial");
    }
}
