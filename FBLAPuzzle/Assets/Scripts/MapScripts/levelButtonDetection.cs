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
}
