using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCalculation : MonoBehaviour
{
    // Start is called before the first frame update
    LevelManager levelManager;
    FeatureButtonDetection featureButtonDetection;
    int points = 1000;
    public bool levelComplete = false;
    public bool gamePause = false;
    void Start()
    {
        levelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        featureButtonDetection = GameObject.Find("LevelManager").GetComponent<FeatureButtonDetection>();
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
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =points.ToString();
        if (points > 0&& !levelComplete&&!gamePause)
        {
            StartCoroutine(pointsCountDown());
        }else if (points<=0) {
            Debug.Log("Points calc");
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
            featureButtonDetection.tipButton.SetActive(true);
        }

    }
}
