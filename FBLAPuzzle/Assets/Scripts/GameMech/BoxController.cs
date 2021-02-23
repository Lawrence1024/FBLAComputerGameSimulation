using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    LevelManager levelManager;
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

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        movePoint.parent = null;
        movePoint.position = transform.position;
        //Debug.Log("MovePoint POsition: " + movePoint.position);
        positionHistory.Add(new List<int> { xPos, yPos });
        startingPosition = new List<int> { xPos, yPos };
        startingVectPosition = transform.position;
        piecePosition = gameCanvas.GetComponent<PiecePosition>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        ifOverLap();


    }
    bool ifOverLap()
    {
        if(xPos == player.GetComponent<PlayerController>().xPos && yPos == player.GetComponent<PlayerController>().yPos)
        {
            return true;
        }
        return false;
    }
    bool thereIsObstacle()
    {
        bool obstUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 0.99f, 0f), 0.2f, whatStopsMovement);
        bool boxUp = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 0.99f, 0f), 0.2f, boxLayer);
        bool obstDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -0.99f, 0f), 0.2f, whatStopsMovement);
        bool boxDown = Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -0.99f, 0f), 0.2f, boxLayer);
        bool obstLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-0.99f, 0f, 0f), 0.2f, whatStopsMovement);
        bool boxLeft = Physics2D.OverlapCircle(movePoint.position + new Vector3(-0.99f, 0f, 0f), 0.2f, boxLayer);
        bool obstRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(0.99f, 0f, 0f), 0.2f, whatStopsMovement);
        bool boxRight = Physics2D.OverlapCircle(movePoint.position + new Vector3(0.99f, 0f, 0f), 0.2f, boxLayer);
        if (lastPlayerMovement=="up"&& (obstUp || boxUp))
        {
            return true;
        }else if (lastPlayerMovement == "down" && (obstDown || boxDown))
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
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f);
            }
            else
            {
                return false;
            }
        }else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f);
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
        float scale = 1f;
        if (lastPlayerMovement=="up")
        {
            movePoint.position += new Vector3(0f, scale, 0f);
            yPos += 1;
        }else if (lastPlayerMovement == "down")
        {
            movePoint.position += new Vector3(0f, -1*scale, 0f);
            yPos -= 1;
        }else if (lastPlayerMovement == "left")
        {
            movePoint.position += new Vector3(-1*scale, 0f, 0f);
            xPos -= 1;
        }else if (lastPlayerMovement == "right")
        {
            movePoint.position += new Vector3(scale, 0f, 0f);
            xPos += 1;
        }
        GetComponentInParent<BoxManager>().checkIfWin();
    //    positionHistory[positionHistory.Count-1]=(new List<int> { xPos, yPos });
        foreach(List<int> temp in positionHistory)
        {
     //       Debug.Log("[" + temp[0] + "," + temp[1] + "]");
        }
     //   movementHistory[movementHistory.Count-1]=lastPlayerMovement;
        //Debug.Log("[" + positionHistory[positionHistory.Count - 1][0] + "," + positionHistory[positionHistory.Count - 1][1] + "]");
    }
    bool thereIsBox()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f), 0.2f, boxLayer))
            {
                return true;
            }
        }else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f), 0.2f, boxLayer))
            {
                return true;
            }
        }
        return false;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        lastPlayerMovement = player.GetComponent<PlayerController>().attemptMovement;
        getPushed = true;
        if (thereIsObstacle())
        {
            player.GetComponent<PlayerController>().rebound();
        }
        else
        {
            move();
            //    piecePosition.addBoxPos(player.GetComponent<PlayerController>().attemptMovement);
            positionHistory[positionHistory.Count - 1] = new List<int> { xPos, yPos };
            movementHistory[movementHistory.Count - 1] = lastPlayerMovement;
        }
        //StartCoroutine(checkIfBug());
        if (checkIfEnterQuestion()&&!answered)
        {
            gameObject.GetComponentInParent<BoxManager>().checkIfWin();
            answerQuestion();
        }
    //    printArray(movementHistory,"Movement History: ");
    }
    IEnumerator checkIfBug()
    {
        yield return new WaitForSeconds(0.2f);
        bool condition1 = Physics2D.OverlapCircle(movePoint.position, 0.02f, playerLayer);
        bool condition2 = xPos == player.GetComponent<PlayerController>().xPos && yPos == player.GetComponent<PlayerController>().yPos;
        if (condition2)
        {
            //Debug.Log("Box Position: ["+xPos+","+yPos+"]");
            //Debug.Log("Player Position: [" + player.GetComponent<PlayerController>().xPos + "," + player.GetComponent<PlayerController>().yPos + "]");
            player.GetComponent<PlayerController>().rebound();
        }
    }
    public bool checkIfEnterQuestion()
    {
        if(Physics2D.OverlapCircle(movePoint.position, 0.2f, questionLayer))
        {
            return true;
        }
        return false;
    }
    public void answerQuestion()
    {
        Debug.Log("You Entered A Question Box");
        gameObject.GetComponent<QuestionBoxCondition>().checkBoxQuestionStatus();
        levelManager.currentQuestionBox = gameObject;
        StartCoroutine(buffer());
        

    }
    public void reverseBoxMove()
    {
    //    printArray(movementHistory, "Box Movement History: ");
        string lastMove = movementHistory[movementHistory.Count - 1].ToString();
        if (lastMove == "up")
        {
            movePoint.position += new Vector3(0f, -1f, 0f);
            yPos -= 1;
        }
        else if (lastMove == "down")
        {
            movePoint.position += new Vector3(0f, 1f, 0f);
            yPos += 1;
        }
        else if (lastMove == "left")
        {
            movePoint.position += new Vector3(1f, 0f, 0f);
            xPos += 1;
        }
        else if (lastMove == "right")
        {
            movePoint.position += new Vector3(-1f, 0f, 0f);
            xPos -= 1;
        }else if (lastMove == "-")
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
        //Debug.Log("test");
        //Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite);
        gameObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
        gameObject.GetComponentInParent<BoxManager>().checkIfWin();
    }
    void printArray(List<int> temp)
    {
        string msg = "[";
        for (int i = 0; i < temp.Count; i++)
        {
            msg += temp[i] + ",";
        }
        msg += "]";
        Debug.Log(msg);
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
        string msg = s + "[";
        foreach (List<int> myL in temp)
        {
            msg += ("[" + myL[0] + "," + myL[1] + "],");
        }
        msg += "]";
        Debug.Log(msg);
    }

    IEnumerator buffer()
    {
        yield return new WaitForSeconds(.2f);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
    }
}
