using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject loadingGameObject;
    private string loadingText="Loading";
    private int loadingCounter = 5;
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
        //audioObject.GetComponent<AudioSource>().Pause();
        //not disabling the audio
        //GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().pauseAudio();
//disable all audioplayer
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(0.25f);
        loadingCounter = Random.Range(4, 7);
        Debug.Log("load scene "+sceneName);
        StartCoroutine(Buffer(sceneName));        
    }

    IEnumerator Buffer(string sceneName)
    {
        loadingCounter--;
        Debug.Log("Loading text: " + loadingText);
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
            StartCoroutine(Buffer(sceneName));
        }
        if (loadingCounter <= 0)
        {
            //Scene Transition
            //Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
            //not disabling the audio
            //GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().startPlayingAudio();
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().changeVolume(1f);
            SceneManager.LoadScene(sceneName);
            //SceneManager.MoveGameObjectToScene(GameObject.Find("AccountsManager"), sceneToLoad);
        }
    }
}

