using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxManager : MonoBehaviour
{
    public List<GameObject> allBoxes;
    LevelManager levelManager;
    PointsCalculation pointsCalculation;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        pointsCalculation = GameObject.Find("Points").GetComponent<PointsCalculation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkIfWin()
    {
        bool win = true;
        foreach(GameObject box in allBoxes)
        {
            if (!box.GetComponent<BoxController>().answered||!box.GetComponent<BoxController>().checkIfEnterQuestion())
            {
                win = false;
            }
        }
        if (win)
        {
            Debug.Log("The Level Is Passed!");
            StartCoroutine(buffer());
        }
    }


    IEnumerator buffer() {
        pointsCalculation.levelComplete = true;
        GameObject[] buttons;
        buttons = GameObject.FindGameObjectsWithTag("Buttons");
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1f);
        levelManager.displayScoreboard();
    }
}
