using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePositionRecord : MonoBehaviour
{
    public GameObject player;
    public GameObject boxManager;
    private PlayerController playerScript;
    private BoxManager boxManagerScript;
    private List<GameObject> boxes;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
        boxManagerScript = boxManager.GetComponent<BoxManager>();
        boxes = boxManagerScript.allBoxes;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateObjectPosition2(string lastMove)
    {
        Debug.Log(lastMove);
    }

}
