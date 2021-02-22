using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_TutorialFlowController : MonoBehaviour
{
    public GameObject gameCanvas;
    public T_PlayerController playerController;
    public T_BoxManager boxManager;
    public int currentStep = 0;
    public List<GameObject> answerButtons;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameCanvas.GetComponentInChildren<T_PlayerController>();
        boxManager = gameCanvas.GetComponentInChildren<T_BoxManager>();


    }

    // Update is called once per frame
    void Update()
    {
        if (currentStep == 19)
        {
            answerButtons[0].GetComponent<Button>().interactable = false;
            answerButtons[1].GetComponent<Button>().interactable = false;
            answerButtons[2].GetComponent<Button>().interactable = true;
            answerButtons[3].GetComponent<Button>().interactable = false;
        }
        else if (currentStep == 20)
        {
            answerButtons[0].GetComponent<Button>().interactable = true;
            answerButtons[1].GetComponent<Button>().interactable = false;
            answerButtons[2].GetComponent<Button>().interactable = false;
            answerButtons[3].GetComponent<Button>().interactable = false;
        }
    }
    public void nextStep()
    {
        currentStep++;
        Debug.Log("Step: " + currentStep);
    }
}
