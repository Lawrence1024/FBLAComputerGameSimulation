using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxManager : MonoBehaviour
{
    public List<GameObject> allBoxes;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
            StartCoroutine(levelManager.buffer());
        }
    }


    
}
