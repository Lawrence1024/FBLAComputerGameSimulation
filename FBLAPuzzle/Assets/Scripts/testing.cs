using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public int testingCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        testingCounter++;
        StartCoroutine(test());
    }
}
