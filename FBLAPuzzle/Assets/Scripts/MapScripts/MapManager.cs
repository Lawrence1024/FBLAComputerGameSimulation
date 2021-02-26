using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public GameObject PauseMenuCanvas;
    public GameObject MapCanvas;
    public GameObject currentUserName;
    public GameObject currentStarCount;
    public GameObject userColor;
    public GameObject[] allLevelButtons;
    public GameObject[] starPanels;
    private Account activeAccount;
    void Start()
    {
        PauseMenuCanvas.SetActive(false);
        LoadingCanvas.SetActive(false);
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        for (int i=0; i<allLevelButtons.Length; i++) {
            allLevelButtons[i].GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i < starPanels.Length; i++) {
            starPanels[i].transform.GetChild(0).gameObject.SetActive(false);
            starPanels[i].transform.GetChild(1).gameObject.SetActive(false);
            starPanels[i].transform.GetChild(2).gameObject.SetActive(false);
            starPanels[i].SetActive(false);
        }
        allLevelButtons[0].GetComponent<Button>().interactable = true;
        for (int i = 1; i < activeAccount.starsList.Count; i++)
        {
            if (activeAccount.starsList[i-1]>=0) {
                allLevelButtons[i].GetComponent<Button>().interactable = true;
            }
        }
        for (int i=0; i<activeAccount.starsList.Count;i++) {
            if (activeAccount.starsList[i]>=0) {
                for (int j=0; j<activeAccount.starsList[i]; j++) {
                    starPanels[i].transform.GetChild(j).gameObject.SetActive(true);
                }
                starPanels[i].SetActive(true);
            }
        }

        currentUserName.GetComponent<TMPro.TextMeshProUGUI>().text=activeAccount.userName;
        currentStarCount.GetComponent<TMPro.TextMeshProUGUI>().text = activeAccount.getTotalStar().ToString();
        userColor.GetComponent<Image>().color = activeAccount.avatarColor;
    }
    void deactivateStars() { 
        
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
        PauseMenuCanvas.transform.GetChild(2).gameObject.GetComponent<Slider>().value = GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().sliderValue;

        if (PauseMenuCanvas.activeSelf)
        {
            //GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(0.25f);
        }
        else {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        }
    }
}
