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
    public GameObject instructionText;
    public GameObject arrow;
    public Vector3 arrowGoToPosition;
    public bool arrowMovingToggle;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameCanvas.GetComponentInChildren<T_PlayerController>();
        boxManager = gameCanvas.GetComponentInChildren<T_BoxManager>();
        keysControl = GetComponentInChildren<T_UI_KeysControl>();
        arrow.SetActive(false);
        arrowGoToPosition = new Vector3(0f, 0f, 0f);
        arrow.transform.position = new Vector3(0f, 0f, 0f);
        arrowMovingToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        string s = "Please follow the keyboard\nPRESS ";
        if (currentStep == 0)
        {
            //instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = s + "W";
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Follow keyboard.\nPractice basic maneuver!";
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 18 || currentStep == 19 || currentStep == 20 || currentStep == 22 || currentStep == 29)
        {
            moveArrowLeftRight();
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, arrowGoToPosition, 2f * Time.deltaTime);
        }
        else if (currentStep == 23)
        {
            moveArrowUpDown();
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, arrowGoToPosition, 2f * Time.deltaTime);
        }
    }
    public void moveArrowLeftRight()
    {
        if (Mathf.Abs(arrow.transform.position.x - arrowGoToPosition.x) < 0.05)
        {
            arrowMovingToggle = !arrowMovingToggle;
            if (arrowMovingToggle)
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(1f, 0f, 0f);
            }
            else
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(-1f, 0f, 0f);
            }
        }
    }
    public void moveArrowUpDown()
    {
        if (Mathf.Abs(arrow.transform.position.y - arrowGoToPosition.y) < 0.05)
        {
            arrowMovingToggle = !arrowMovingToggle;
            if (arrowMovingToggle)
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(0f, 1f, 0f);
            }
            else
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(0f, -1f, 0f);
            }
        }
    }
    public void nextStep()
    {
        currentStep++;
        Debug.Log("Step: " + currentStep);
        respondToNext();
    }
    public void respondToNext()
    {
        if (currentStep == 1)
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
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Great! Now try run through the wall to the right";
            keysControl.darkAllKeys();
            keysControl.glow("right");
        }
        else if (currentStep == 5)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can't pass walls, try run through bottom border";
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 6)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can't leave border, let's go around the wall";
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
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's push the box to the left";
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 10)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Great! Let's try push two box at the same time";
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 11)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, you can't push two box at the same time. Go around the box";
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
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's push the box to upper boarder";
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 14)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Try push the box out of boarder";
            keysControl.darkAllKeys();
            keysControl.glow("up");
        }
        else if (currentStep == 15)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, that didn't work.\n Let's go around.";
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
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Push the box into question area";
            keysControl.darkAllKeys();
            keysControl.glow("left");
        }
        else if (currentStep == 18)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "A question will display.\n Click on a wrong answer.";
            arrow.SetActive(true);
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(-2.901f, -1.733f, 0f);
            arrowGoToPosition = arrow.transform.position;
        }
        else if (currentStep == 19)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You will loose a heart everytime you answer wrong.\nClick on another wrong answer.";
            answerButtons[0].GetComponent<Button>().interactable = false;
            answerButtons[1].GetComponent<Button>().interactable = false;
            answerButtons[2].GetComponent<Button>().interactable = true;
            answerButtons[3].GetComponent<Button>().interactable = false;
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(-2.901f, -2.966f, 0f);
            arrowGoToPosition = arrow.transform.position;
        }
        else if (currentStep == 20)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "1 heart left, let's click the right answer";
            answerButtons[0].GetComponent<Button>().interactable = true;
            answerButtons[1].GetComponent<Button>().interactable = false;
            answerButtons[2].GetComponent<Button>().interactable = false;
            answerButtons[3].GetComponent<Button>().interactable = false;
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(2.146f, -0.463f, 0f);
            arrowGoToPosition = arrow.transform.position;
        }
        else if (currentStep == 21)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Great! The box turned green! Let's push the other box to bottom border.";
            arrow.SetActive(false);
            answerButtons[0].GetComponent<Button>().interactable = true;
            answerButtons[1].GetComponent<Button>().interactable = true;
            answerButtons[2].GetComponent<Button>().interactable = true;
            answerButtons[3].GetComponent<Button>().interactable = true;
            keysControl.darkAllKeys();
            keysControl.glow("down");
        }
        else if (currentStep == 22)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, we went to far. Click the button to resume last move.";
            arrowMovingToggle = true;
            arrow.transform.position = new Vector3(-5.059f, 3.318f, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrow.SetActive(true);
            keysControl.darkAllKeys();
        }
        else if (currentStep == 23)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's reset the board with the other button.";
            arrow.transform.position = new Vector3(-8.212f, 1.544f, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            arrowMovingToggle = true;
            arrowGoToPosition = arrow.transform.position;
            keysControl.darkAllKeys();
        }
        else if (currentStep == 24)
        {
            arrow.SetActive(false);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Push the brown box into the question area";
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
        else if (currentStep == 29)
        {
            arrow.transform.position = new Vector3(-2.901f, -4.199f, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            arrow.SetActive(true);
        }
        else if (currentStep == 30)
        {
            Debug.Log("Show POINTS");
            nextStep();
        }
        else if (currentStep == 31)
        {
            Debug.Log("LEVEL RESTART");
        }
    }
}
