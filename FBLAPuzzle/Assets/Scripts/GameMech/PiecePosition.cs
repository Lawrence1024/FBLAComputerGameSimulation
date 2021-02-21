using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePosition : MonoBehaviour
{
    LevelManager levelManager;
    public GameObject player;
    public GameObject boxManager;
    private PlayerController playerScript;
    private BoxManager boxManagerScript;
    private List<GameObject> boxes;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        playerScript = player.GetComponent<PlayerController>();
        boxManagerScript = boxManager.GetComponent<BoxManager>();
        boxes = boxManagerScript.allBoxes;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void printArray(ArrayList temp, string s)
    {
        string msg = "[";
        for (int i = 0; i < temp.Count; i++)
        {
            msg += temp[i] + ",";
        }
        msg += "]";
        Debug.Log(s + msg);
    }
    void printArray(List<List<int>> temp, string s)
    {
        string msg = "[";
        for(int j = 0; j < temp.Count; j++)
        {
            msg += "[";
            for (int i = 0; i < 2; i++)
            {
                msg += temp[i] + ",";
            }
            msg += "], ";
        }
        msg += "]";
        Debug.Log(s + msg);
    }
    public void addBoxPos(string lastMove)
    {
        foreach(GameObject box in boxes){
            BoxController tempController=box.GetComponent<BoxController>();
            tempController.positionHistory.Add(new List<int> { tempController.xPos, tempController.yPos });
            tempController.movementHistory.Add("-");
            //printArray(tempController.positionHistory, "Box Position History: ");
            //printArray(tempController.movementHistory, "Box Movement History: ");
        }

    }
    public void addPlayerPos(string lastMove)
    {
        playerScript.movementHistory.Add(lastMove);
        playerScript.positionHistory.Add(new List<int> { playerScript.xPos, playerScript.yPos });
    }
    public void backBoxPos()
    {
        foreach (GameObject box in boxes)
        {
            BoxController tempController = box.GetComponent<BoxController>();
            tempController.positionHistory.RemoveAt(tempController.positionHistory.Count - 1);
            tempController.movementHistory.RemoveAt(tempController.movementHistory.Count - 1);
        }
        
    }
    public void backPlayerPos()
    {
        playerScript.positionHistory.RemoveAt(playerScript.positionHistory.Count - 1);
        playerScript.movementHistory.RemoveAt(playerScript.movementHistory.Count - 1);
    }
    public void whenHitBackButton()
    {
        if (playerScript.movementHistory.Count != 0)
        {
            string lastSuccessMove = playerScript.movementHistory[playerScript.movementHistory.Count - 1].ToString();
            playerScript.reversePlayerMove(lastSuccessMove);
            foreach (GameObject box in boxes)
            {
                BoxController tempController = box.GetComponent<BoxController>();
                tempController.reverseBoxMove();
            }
            backBoxPos();
        }
        else
        {
            Debug.Log("You are at the first step!");
            StartCoroutine(levelManager.loadWarning("You are at the first step!", 0.8f));

        }
    }
    public void whenHitResetButton()
    {
        if (playerScript.movementHistory.Count != 0)
        {
            playerScript.resetPlayer();
            foreach (GameObject box in boxes)
            {
                BoxController tempController = box.GetComponent<BoxController>();
                tempController.resetBox();
            }
            //backBoxPos();
        }
        else
        {
            Debug.Log("You are at the first step!");
            StartCoroutine(levelManager.loadWarning("You are at the first step!", 0.8f));
        }
    }

    
}

