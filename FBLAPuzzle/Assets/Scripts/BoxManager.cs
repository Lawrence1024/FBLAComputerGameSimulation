using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public List<GameObject> allBoxes;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (!box.GetComponent<BoxController>().answered)
            {
                win = false;
            }
        }
        if (win)
        {
            Debug.Log("The Level Is Passed!");
        }
    }
}
