using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCalculation : MonoBehaviour
{
    // Start is called before the first frame update
    LevelManager levelManager;
    FeatureButtonDetection featureButtonDetection;
    int points = 1000;
    void Start()
    {
        levelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        featureButtonDetection = GameObject.Find("LevelManager").GetComponent<FeatureButtonDetection>();
        points = 1000;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Points: " + points;
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
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Points: " + points;
        if (points > 0)
        {
            StartCoroutine(pointsCountDown());
        }
        if (points<=0) {
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Points: 0";
            featureButtonDetection.tipButton.SetActive(true);
        }

    }
}
