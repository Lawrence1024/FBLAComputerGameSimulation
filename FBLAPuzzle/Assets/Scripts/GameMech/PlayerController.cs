using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 3f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;
    public LayerMask boxLayer;

    public ArrayList movementHistory = new ArrayList();

    //public int[] startingPosition=new int[2];
    public int xPos;
    public int yPos;
    public List<int> startingPosition;
    public Vector3 startingVectPosition;
    public List<List<int>> positionHistory=new List<List<int>>();

    public GameObject gameCanvas;
    private PiecePosition piecePosition;
    public string attemptMovement;

    public bool canMove = true;
    private int testCounter = 0;
    private float newTime;
    private float oldTime=0f;
    

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        movePoint.position = transform.position;
        positionHistory.Add(new List<int> {xPos,yPos});
        startingPosition = new List<int> { xPos, yPos };
        startingVectPosition = transform.position;
        piecePosition = gameCanvas.GetComponent<PiecePosition>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        newTime = Time.time;
        bool state1 = Vector3.Distance(transform.position, movePoint.position) <= 0.05f;
        bool state2 = (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f);
        bool state3 = canMove;
        bool state4 = newTime - oldTime > 0.35f;
        if (state1 && state2 && state3 && state4)
        {
            //if (thereIsObstacle())
            //{
            //    //makeMovement();
            //}
            //else
            //{
                makeMovement();
                piecePosition.addPlayerPos(attemptMovement);
                piecePosition.addBoxPos(attemptMovement);
            //    //    printArray(positionHistory, "Movement: ");
            //}
            //canMove = false;
            //StartCoroutine(resumeMove(0.5f));
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("StopMovement"))
        {
            rebound();
        }
    }
        bool thereIsObstacle()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f), 0.2f, whatStopsMovement))
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f), 0.2f, whatStopsMovement))
        {
            return true;
        }
        return false;
    }
    bool moveWhenNoObstacle() {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal")*1f, 0f, 0f);
            }
            else
            {
                return false;
            }
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f), 0.2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 1f, 0f);
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    void printArray(List<int> temp)
    {
        string msg = "[";
        for(int i = 0; i < temp.Count; i++)
        {
            msg += temp[i] + ",";
        }
        msg += "]";
        Debug.Log(msg);
    }
    void printArray(ArrayList temp,string s)
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
        string msg = s+"[";
        foreach(List<int> myL in temp)
        {
            msg+=("["+myL[0] + "," + myL[1]+"],");
        }
        msg += "]";
        Debug.Log(msg);
    }
    void makeMovement()
    {
        float scale = 1f;
        if ((Input.GetAxisRaw("Horizontal")) == 1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * scale, 0f, 0f);
            attemptMovement="right";
            xPos += 1;
        }else if ((Input.GetAxisRaw("Horizontal")) == -1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * scale, 0f, 0f);
            attemptMovement = "left";
            xPos -= 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == 1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * scale, 0f);
            attemptMovement = "up";
            yPos += 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == -1f)
        {
            GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().playMovementSound();
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * scale, 0f);
            attemptMovement = "down";
            yPos -= 1;
        }
        //Debug.Log("Movement: ["+positionHistory[positionHistory.Count-1][0]+","+ positionHistory[positionHistory.Count - 1][1]+"]");
        //Debug.Log("Movement: " + movementHistory[movementHistory.Count - 1]);
        //   piecePosition.updatePos(movementHistory[movementHistory.Count-1].ToString());
        oldTime = Time.time;
    }
    bool thereIsBox()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f), 0.2f, boxLayer))
        {
            return true;
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f), 0.2f, boxLayer))
        {
            return true;
        }
        return false;
    }
    public void rebound()
    {
        string lastMove = attemptMovement;
        //canMove = false;
        reversePlayerMove(lastMove);
        piecePosition.backBoxPos();
        //StartCoroutine(resumeMove(2f));
        //Debug.Log("Rebound: [" + positionHistory[positionHistory.Count - 1][0] + "," + positionHistory[positionHistory.Count - 1][1] + "]");
    //    printArray(positionHistory,"Rebound: ");
        
    }
    public void reversePlayerMove(string lastMove)
    {
        float scale = 1f;
        if (lastMove == "up")
        {
            movePoint.position += new Vector3(0f, -scale, 0f);
            yPos -= 1;
        }
        else if (lastMove == "down")
        {
            movePoint.position += new Vector3(0f, scale, 0f);
            yPos += 1;
        }
        else if (lastMove == "left")
        {
            movePoint.position += new Vector3(scale, 0f, 0f);
            xPos += 1;
        }
        else if (lastMove == "right")
        {
            movePoint.position += new Vector3(-scale, 0f, 0f);
            xPos -= 1;
        }
        piecePosition.backPlayerPos();
        //piecePosition.backBoxPos();
    }
    IEnumerator resumeMove(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    public void resetPlayer()
    {
        positionHistory = new List<List<int>>();
        positionHistory.Add(startingPosition);
        movementHistory = new ArrayList();
        transform.position = startingVectPosition;
        movePoint.position = startingVectPosition;
    }
    IEnumerator checkIfBug()
    {
        yield return new WaitForSeconds(0.2f);
        bool condition1 = Physics2D.OverlapCircle(movePoint.position, 0.02f, whatStopsMovement);
        //bool condition2 = xPos == player.GetComponent<PlayerController>().xPos && yPos == player.GetComponent<PlayerController>().yPos;
        if (condition1)
        {
            //Debug.Log("Box Position: ["+xPos+","+yPos+"]");
            //Debug.Log("Player Position: [" + player.GetComponent<PlayerController>().xPos + "," + player.GetComponent<PlayerController>().yPos + "]");
            rebound();
        }
    }
}
