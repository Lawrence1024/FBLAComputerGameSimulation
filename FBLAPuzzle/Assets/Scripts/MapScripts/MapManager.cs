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
    public GameObject[] allLevelButtons;
    private Account activeAccount;
    void Start()
    {
        PauseMenuCanvas.SetActive(false);
        LoadingCanvas.SetActive(false);
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        for (int i=0; i<allLevelButtons.Length; i++) {
            allLevelButtons[i].GetComponent<Button>().interactable = false;
        }
        allLevelButtons[0].GetComponent<Button>().interactable = true;
        for (int i = 1; i < activeAccount.starsList.Count; i++)
        {
            //Debug.Log(i+" "+activeAccount.starsList[i]);
            Debug.Log(i+" "+activeAccount.starsList[i-1]);
            if (activeAccount.starsList[i-1]>=0) {
                allLevelButtons[i].GetComponent<Button>().interactable = true;
            }
        }
        currentUserName.GetComponent<TMPro.TextMeshProUGUI>().text=activeAccount.userName;
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
        else {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
        }
    }
}
