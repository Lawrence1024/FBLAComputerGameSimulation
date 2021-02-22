using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levelButtonDetection : MonoBehaviour
{
    
    MapManager mapManager;
    // Start is called before the first frame update
    void Start()
    {
        mapManager= GameObject.Find("MapManager").GetComponent<MapManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeLevel(GameObject levelButton) {
        Debug.Log(levelButton.name);
        //SceneManager.LoadScene(levelButton.name);
        mapManager.LoadingCanvas.SetActive(true);
        mapManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading(levelButton.name);
    }


    public void quitProgram()
    {
        Debug.Log("clicked");
        Application.Quit();
    }
    
    public void resumeGame()
    {
        mapManager.PauseMenuCanvas.SetActive(false);
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
    }
    public void switchToMainMenu()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            mapManager.LoadingCanvas.SetActive(true);
            mapManager.PauseMenuCanvas.SetActive(false);
            mapManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("MainMenu");
        }
        else
        {
            mapManager.PauseMenuCanvas.SetActive(false);
        }
    }
    public void switchToMap()
    {
        if (SceneManager.GetActiveScene().name != "Map")
        {
            mapManager.LoadingCanvas.SetActive(true);
            mapManager.PauseMenuCanvas.SetActive(false);
            mapManager.LoadingCanvas.transform.GetChild(0).gameObject.GetComponent<Loading>().runLoading("Map");
        }
        else
        {
            mapManager.PauseMenuCanvas.SetActive(false);
        }
    }
}
