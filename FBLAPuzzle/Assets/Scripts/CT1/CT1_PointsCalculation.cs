using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT1_PointsCalculation : MonoBehaviour
{
    // Start is called before the first frame update
    CT1_LevelManager levelManager;
    CT1_FeatureButtonDetection featureCT1_ButtonDetection;
    int points = 1000;
    public bool levelComplete = false;
    public bool gamePause = false;
    void Start()
    {
        levelManager= GameObject.Find("LevelManager").GetComponent<CT1_LevelManager>();
        featureCT1_ButtonDetection = GameObject.Find("LevelManager").GetComponent<CT1_FeatureButtonDetection>();
        points = 1000;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        StartCoroutine(pointsCountDown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator pointsCountDown()
    {
        if (!levelComplete && !gamePause) {
            points--;
        }
        //parser.nextScene(sceneCounter, sceneNum);
        yield return new WaitForSeconds(.8f);
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =points.ToString();
        if (points > 0&& !levelComplete&&!gamePause)
        {
            StartCoroutine(pointsCountDown());
        }else if (points<=0) {
            Debug.Log("Points calc");
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
            featureCT1_ButtonDetection.tipButton.SetActive(true);
        }

    }
}
