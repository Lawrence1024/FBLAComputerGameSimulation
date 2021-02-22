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
    public T_UI_KeysControl keysControl;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameCanvas.GetComponentInChildren<T_PlayerController>();
        boxManager = gameCanvas.GetComponentInChildren<T_BoxManager>();
        keysControl = GetComponentInChildren<T_UI_KeysControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStep == 0)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 1)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 2)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 3)
        {
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 4)
        {
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 5)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 6)
        {
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 7)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 8)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 9)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 10)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 11)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 12)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 13)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 14)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 15)
        {
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 16)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 17)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 19)
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
        else if (currentStep == 21)
        {
            answerButtons[0].GetComponent<Button>().interactable = true;
            answerButtons[1].GetComponent<Button>().interactable = true;
            answerButtons[2].GetComponent<Button>().interactable = true;
            answerButtons[3].GetComponent<Button>().interactable = true;
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 22)
        {
            keysControl.darkAllKeys();
        }
        else if (currentStep == 24)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 25)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 26)
        {
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 27)
        {
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 28)
        {
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
    }
    public void nextStep()
    {
        currentStep++;
        Debug.Log("Step: " + currentStep);
    }
}
