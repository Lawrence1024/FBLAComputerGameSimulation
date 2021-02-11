using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ButtonDetection : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject InstructionButton;
    public GameObject LeaderBoardButton;
    //public GameObject PlayButton;
    //public GameObject PlayButton;
    //public GameObject PlayButton;
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.GetComponent<Button>().onClick.AddListener(executePlayButton);
        InstructionButton.GetComponent<Button>().onClick.AddListener(executeInstructionButton);
        LeaderBoardButton.GetComponent<Button>().onClick.AddListener(executeLeaderBoardButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void executePlayButton() {
        Debug.Log("Play");
    }
    void executeInstructionButton() {
        Debug.Log("Instruction");
    }
    void executeLeaderBoardButton() {
        Debug.Log("Leader Board");

    }
}
