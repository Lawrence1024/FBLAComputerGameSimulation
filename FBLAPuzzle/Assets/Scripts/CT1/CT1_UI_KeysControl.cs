using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT1_UI_KeysControl : MonoBehaviour
{
    //The keys are in the order of "WSAD" 
    public List<GameObject> UIKeys;
    private float oldTime;
    private float newTime;

    // Start is called before the first frame update
    void Start()
    {
        oldTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        newTime = Time.time;
    }
    public void glow(string direction)
    {
        int index=-1;
        if (direction == "up")
        {
            index = 0;
        }
        else if (direction == "down")
        {
            index = 1;
        }
        else if (direction == "left")
        {
            index = 2;
        }
        else if (direction == "right")
        {
            index = 3;
        }
        else
        {
            Debug.Log("There is an error in CT1_UI_KeysControl glow function. Error: invalid input");
        }
        UIKeys[index].GetComponentInChildren<SpriteRenderer>().color = new Vector4(0.62f, 0.93f, 1f, 1f);
    }
    public void dark(string direction)
    {
        int index = -1;
        if (direction == "up")
        {
            index = 0;
        }
        else if (direction == "down")
        {
            index = 1;
        }
        else if (direction == "left")
        {
            index = 2;
        }
        else if (direction == "right")
        {
            index = 3;
        }
        else
        {
            Debug.Log("There is an error in CT1_UI_KeysControl glow function. Error: invalid input");
        }
        UIKeys[index].GetComponentInChildren<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
    }
    public void darkAllKeys()
    {
        foreach(GameObject key in UIKeys)
        {
            key.GetComponentInChildren<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
        }
    }
}
