using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class T_BoxManager : MonoBehaviour
{
    public List<GameObject> allBoxes;
    T_LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void checkIfWin()
    {
        bool win = true;
        foreach (GameObject box in allBoxes)
        {
            if (!box.GetComponent<T_BoxController>().answered || !box.GetComponent<T_BoxController>().checkIfEnterQuestion())
            {
                win = false;
            }
        }
        if (win)
        {
            Debug.Log("The Level Is Passed!");

            StartCoroutine(levelManager.buffer());
        }
    }



}
