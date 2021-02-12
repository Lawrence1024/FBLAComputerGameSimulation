using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject loadingGameObject;
    private string loadingText="Loading";
    private int loadingCounter = 9;
    // Start is called before the first frame update
    void Start()
    {
        //runLoading();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void runLoading(string sceneName) {
        loadingCounter = Random.Range(4, 7);
        Debug.Log(loadingCounter);
        StartCoroutine(RunningStart(sceneName));        
    }

    IEnumerator RunningStart(string sceneName)
    {
        loadingCounter--;
        //parser.nextScene(sceneCounter, sceneNum);
        yield return new WaitForSeconds(.5f);
        if (loadingText.Length < 10)
        {
            loadingText = loadingText + ".";
            loadingGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = loadingText;
        }
        else {
            loadingText = "Loading.";
            loadingGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = loadingText;
        }
        if (loadingCounter > 0) {
            StartCoroutine(RunningStart(sceneName));
        }
        if (loadingCounter <= 0)
        {
            //Scene Transition
            SceneManager.LoadScene(sceneName);
        }
    }
}

