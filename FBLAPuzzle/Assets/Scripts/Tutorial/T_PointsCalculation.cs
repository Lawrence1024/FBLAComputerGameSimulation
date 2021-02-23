using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_PointsCalculation : MonoBehaviour
{
    // Start is called before the first frame update
    T_LevelManager levelManager;
    T_FeatureButtonDetection featureButtonDetection;
    int points = 1000;
    public bool levelComplete = false;
    public bool stopTime = true;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        featureButtonDetection = GameObject.Find("LevelManager").GetComponent<T_FeatureButtonDetection>();
        points = 1000;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        StartCoroutine(pointsCountDown());
        Debug.Log(GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.tutorialProgress[0]);
        if (GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount.tutorialProgress[0])
        {
            stopTime = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator pointsCountDown()
    {
        if (!stopTime)
        {
            points--;
        }
        //parser.nextScene(sceneCounter, sceneNum);
        yield return new WaitForSeconds(.9f);
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        if (points > 0 && !levelComplete)
        {
            StartCoroutine(pointsCountDown());
        }
        else if (points <= 0)
        {
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
            Debug.Log("Time=0, tip button is disabled in tutorial");
            //featureButtonDetection.tipButton.SetActive(true);
        }

    }
}
