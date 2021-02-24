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
    public GameObject keysControlObj;
    public T_UI_KeysControl keysControl;
    public GameObject instructionText;
    public GameObject arrow;
    public Vector3 arrowGoToPosition;
    public bool arrowMovingToggle;
    public GameObject okButton;
    public T_LevelManager levelManager;
    public Account activeAccount;
    private float convertingScale;
    // Start is called before the first frame update
    void Start()
    {
        activeAccount = GameObject.Find("AccountsManager").GetComponent<AccountsManager>().activeAccount;
        playerController = gameCanvas.GetComponentInChildren<T_PlayerController>();
        boxManager = gameCanvas.GetComponentInChildren<T_BoxManager>();
        keysControl = keysControlObj.GetComponent<T_UI_KeysControl>();
        arrow.SetActive(false);
        arrowGoToPosition = new Vector3(0f, 0f, 0f);
        arrow.transform.position = new Vector3(0f, 0f, 0f);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        arrowMovingToggle = true;
        okButton.SetActive(false);
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        convertingScale=playerController.findConvertingScale();
        if (activeAccount.tutorialProgress[0])
        {
            currentStep = 41;
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Push the boxes into question areas and complete questions!";
            if (activeAccount.tutorialFeatures[1] == 2)
            {
                instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You would only gain a maximum of 2 star in a real game, try to complete level without answering wrong!";
            }
            else if (activeAccount.tutorialFeatures[1] == 1)
            {
                instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You would only gain a maximum of 1 star in a real game, try to complete level without answering wrong!";
            }
        }

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
        else if (currentStep == 18 || currentStep == 19 || currentStep == 20 || currentStep == 22 || currentStep == 29 || currentStep == 30 || (currentStep >= 31 && currentStep <= 37))
        {
            moveArrowLeftRight();
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, arrowGoToPosition, 2f * Time.deltaTime* convertingScale);
        }
        else if (currentStep == 23)
        {
            moveArrowUpDown();
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, arrowGoToPosition, 2f * Time.deltaTime* convertingScale);
        }
    }
    public void moveArrowLeftRight()
    {
        if (Mathf.Abs(arrow.transform.position.x - arrowGoToPosition.x) < 0.05)
        {
            arrowMovingToggle = !arrowMovingToggle;
            if (arrowMovingToggle)
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(convertingScale, 0f, 0f);
            }
            else
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(-convertingScale, 0f, 0f);
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
                arrowGoToPosition = arrow.transform.position + new Vector3(0f, convertingScale, 0f);
            }
            else
            {
                arrowGoToPosition = arrow.transform.position + new Vector3(0f, -convertingScale, 0f);
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
            arrow.transform.position = new Vector3(-2.901f* convertingScale, -1.733f* convertingScale, 0f);
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
            arrow.transform.position = new Vector3(-2.901f* convertingScale, -2.966f* convertingScale, 0f);
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
            arrow.transform.position = new Vector3(2.146f* convertingScale, -0.463f* convertingScale, 0f);
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
            arrow.transform.position = new Vector3(-5.059f* convertingScale, 3.318f* convertingScale, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrow.SetActive(true);
            keysControl.darkAllKeys();
        }
        else if (currentStep == 23)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's reset the board with the other button.";
            arrow.transform.position = new Vector3(-8.212f* convertingScale, 1.744f* convertingScale, 0f);
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
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Click on a wrong answer";
            arrow.transform.position = new Vector3(-2.901f* convertingScale, -4.199f* convertingScale, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            arrow.SetActive(true);
            keysControl.darkAllKeys();
        }
        else if (currentStep == 30)
        {
            okButton.SetActive(true);
            arrow.transform.position = new Vector3(4.087f* convertingScale, 4.477f* convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oops, you just lost your last heart. The level will reset.";
            levelManager.QuestionCanvas.SetActive(false);
        }
        else if (currentStep == 31)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "When you loose all three hearts, you will loose a star";
            arrow.transform.position = new Vector3(4.087f* convertingScale, 3.401f* convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            levelManager.displayStars("Stars");
        }
        else if (currentStep == 32)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "The first tiral of each level will determine the stars you get";
        }
        else if (currentStep == 33)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can't replay a level to get more stars (since you would already know the answers)";
        }
        else if (currentStep == 34)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Stars will be used to determine your ranking on the leader board (on home page)";
        }
        else if (currentStep == 35)
        {
            arrow.transform.position = new Vector3(-3.422f* convertingScale, 4.414f* convertingScale, 0f);
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            arrowGoToPosition = arrow.transform.position;
            arrowMovingToggle = true;
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "In a normal game, pass levels faster to get higher points.";
        }
        else if (currentStep == 36)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Points will determine your ranking in that level.";
        }
        else if (currentStep == 37)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "You can replay to score higher (It really all comes down to who can move their boxes faster)";
        }
        else if (currentStep == 38)
        {
            arrow.SetActive(false);
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let's see if you can complete the tutorial leve on your own.";
        }
        else if (currentStep == 39)
        {
            instructionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Good Luck And Enjoy!";
        }
        else if (currentStep == 40)
        {
            okButton.SetActive(false);
            activeAccount.tutorialFeatures[0] = 3;
            activeAccount.tutorialFeatures[1] = 3;
            StartCoroutine(levelManager.loadWarning("Level restart in", 3));
        }
    }
    public void moveArrow(Vector3 pos, float rot)
    {

    }
}
