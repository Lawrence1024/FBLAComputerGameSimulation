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
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        featureButtonDetection = GameObject.Find("LevelManager").GetComponent<T_FeatureButtonDetection>();
        points = 1000;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        StartCoroutine(pointsCountDown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator pointsCountDown()
    {
        points--;
        //parser.nextScene(sceneCounter, sceneNum);
        yield return new WaitForSeconds(.01f);
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        if (points > 0 && !levelComplete)
        {
            StartCoroutine(pointsCountDown());
        }
        if (points <= 0)
        {
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
            featureButtonDetection.tipButton.SetActive(true);
        }

    }
}
