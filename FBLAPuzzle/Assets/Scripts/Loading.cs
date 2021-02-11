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

    void runLoading() {
        loadingCounter = 9;
        StartCoroutine(RunningStart());        
    }

    IEnumerator RunningStart()
    {
        loadingCounter--;
        Debug.Log(loadingCounter);
        //parser.nextScene(sceneCounter, sceneNum);
        yield return new WaitForSeconds(.8f);
        if (loadingText.Length < 10)
        {
            loadingText = loadingText + ".";
            loadingGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = loadingText;
            Debug.Log(loadingText);
        }
        else {
            loadingText = "Loading.";
            loadingGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = loadingText;
            Debug.Log(loadingText);
        }
        if (loadingCounter > 0) {
            StartCoroutine(RunningStart());
        }
        if (loadingCounter <= 0)
        {
            //Scene Transition
            SceneManager.LoadScene("SampleScene");
        }
    }
}

