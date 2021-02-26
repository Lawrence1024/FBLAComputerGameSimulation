using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_BoxController : MonoBehaviour
{
    T_LevelManager levelManager;
    public float moveSpeed = 20f;
    public Transform movePoint;
    public GameObject player;

    public LayerMask whatStopsMovement;
    public LayerMask boxLayer;
    public LayerMask playerLayer;
    public LayerMask questionLayer;
    public bool getPushed = false;
    private string lastPlayerMovement;

    public int xPos;
    public int yPos;
    public List<int> startingPosition;
    public List<List<int>> positionHistory = new List<List<int>>();
    public Vector3 startingVectPosition;
    public ArrayList movementHistory = new ArrayList();

    public GameObject gameCanvas;
    private PiecePosition piecePosition;

    public bool answered = false;
    public Sprite correctSprite;

    public float convertingScale;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<T_LevelManager>();
        movePoint.position = transform.position;
        positionHistory.Add(new List<int> { xPos, yPos });
        startingPosition = new List<int> { xPos, yPos };
        startingVectPosition = transform.position;
        piecePosition = gameCanvas.GetComponent<PiecePosition>();
        convertingScale = player.GetComponent<T_PlayerController>().findConvertingScale();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        ifOverLap();


    }
    bool ifOverLap()
    {
        if (xPos == player.GetComponent<T_PlayerController>().xPos && yPos == player.GetComponent<T_PlayerController>().yPos)
        {
            return true;
        }
        return false;
    }
    bool thereIsObstacle()
    {
        bool obstUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, convertingScale, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, convertingScale, 0f), 0.2f* convertingScale, boxLayer);
        bool obstDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -convertingScale, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -convertingScale, 0f), 0.2f* convertingScale, boxLayer);
        bool obstLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-convertingScale, 0f, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-convertingScale, 0f, 0f), 0.2f* convertingScale, boxLayer);
        bool obstRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(convertingScale, 0f, 0f), 0.2f* convertingScale, whatStopsMovement);
        bool boxRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(convertingScale, 0f, 0f), 0.2f* convertingScale, boxLayer);
        if (lastPlayerMovement == "up" && (obstUp || boxUp))
        {
            return true;
        }
        else if (lastPlayerMovement == "down" && (obstDown || boxDown))
        {
            return true;
        }
        else if (lastPlayerMovement == "left" && (obstLeft || boxLeft))
        {
            return true;
        }
        else if (lastPlayerMovement == "right" && (obstRight || boxRight))
        {
            return true;
        }
        return false;
    }
    bool moveWhenNoObstacle()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * convertingScale, 0f, 0f), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * convertingScale, 0f, 0f);
            }
            else
            {
                return false;
            }
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * convertingScale, 0f), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * convertingScale, 0f);
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    void move()
    {
        makeMovement();
        getPushed = false;
    }
    void makeMovement()
    {
        if (lastPlayerMovement == "up")
        {
            movePoint.position += new Vector3(0f, convertingScale, 0f);
            yPos += 1;
        }
        else if (lastPlayerMovement == "down")
        {
            movePoint.position += new Vector3(0f, -1 * convertingScale, 0f);
            yPos -= 1;
        }
        else if (lastPlayerMovement == "left")
        {
            movePoint.position += new Vector3(-1 * convertingScale, 0f, 0f);
            xPos -= 1;
        }
        else if (lastPlayerMovement == "right")
        {
            movePoint.position += new Vector3(convertingScale, 0f, 0f);
            xPos += 1;
        }
        GetComponentInParent<T_BoxManager>().checkIfWin();
    }
    bool thereIsBox()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * convertingScale, 0f, 0f), 0.2f, boxLayer))
            {
                return true;
            }
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * convertingScale, 0f), 0.2f, boxLayer))
            {
                return true;
            }
        }
        return false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        lastPlayerMovement = player.GetComponent<T_PlayerController>().attemptMovement;
        getPushed = true;
        if (thereIsObstacle())
        {
            player.GetComponent<T_PlayerController>().rebound();
        }
        else
        {
            move();
            positionHistory[positionHistory.Count - 1] = new List<int> { xPos, yPos };
            movementHistory[movementHistory.Count - 1] = lastPlayerMovement;
        }
        if (checkIfEnterQuestion() && !answered)
        {
            gameObject.GetComponentInParent<T_BoxManager>().checkIfWin();
            answerQuestion();
        }
    }
    IEnumerator checkIfBug()
    {
        yield return new WaitForSeconds(0.2f);
        bool condition1 = Physics2D.OverlapCircle(movePoint.position, 0.02f, playerLayer);
        bool condition2 = xPos == player.GetComponent<T_PlayerController>().xPos && yPos == player.GetComponent<T_PlayerController>().yPos;
        if (condition2)
        {
            player.GetComponent<T_PlayerController>().rebound();
        }
    }
    public bool checkIfEnterQuestion()
    {
        if (Physics2D.OverlapCircle(movePoint.position, 0.2f, questionLayer))
        {
            return true;
        }
        return false;
    }
    public void answerQuestion()
    {
        gameObject.GetComponent<T_QuestionBoxCondition>().checkBoxQuestionStatus();
        levelManager.currentQuestionBox = gameObject;
        StartCoroutine(buffer());


    }
    public void reverseBoxMove()
    {
        string lastMove = movementHistory[movementHistory.Count - 1].ToString();
        if (lastMove == "up")
        {
            movePoint.position += new Vector3(0f, -convertingScale, 0f);
            yPos -= 1;
        }
        else if (lastMove == "down")
        {
            movePoint.position += new Vector3(0f, convertingScale, 0f);
            yPos += 1;
        }
        else if (lastMove == "left")
        {
            movePoint.position += new Vector3(convertingScale, 0f, 0f);
            xPos += 1;
        }
        else if (lastMove == "right")
        {
            movePoint.position += new Vector3(-convertingScale, 0f, 0f);
            xPos -= 1;
        }
        else if (lastMove == "-")
        {

        }
        else
        {
            Debug.Log("Error in reverseBoxMove in BoxController");
        }
    }
    public void resetBox()
    {
        positionHistory = new List<List<int>>();
        positionHistory.Add(startingPosition);
        movementHistory = new ArrayList();
        transform.position = startingVectPosition;
        movePoint.position = startingVectPosition;
    }
    public void answerCorrect()
    {
        answered = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
        gameObject.GetComponentInParent<T_BoxManager>().checkIfWin();
    }

    IEnumerator buffer()
    {
        yield return new WaitForSeconds(.2f);
        GameObject.Find("Player").GetComponent<T_PlayerController>().enabled = false;
    }
}
