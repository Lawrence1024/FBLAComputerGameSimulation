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
    public List<List<int>> positionHistory=new List<List<int>>();

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        movePoint.position = transform.position;
        positionHistory.Add(new List<int> {xPos,yPos});
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) && !thereIsObstacle() && canMove)
        {
            makeMovement();
            canMove = false;
            StartCoroutine(resumeMove(0.5f));
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
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal")*0.99f, 0f, 0f);
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
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f);
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
    void makeMovement()
    {
        if ((Input.GetAxisRaw("Horizontal")) == 1f)
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f);
            movementHistory.Add("right");
            xPos += 1;
        }else if ((Input.GetAxisRaw("Horizontal")) == -1f)
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f);
            movementHistory.Add("left");
            xPos -= 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == 1f)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f);
            movementHistory.Add("up");
            yPos += 1;
        }
        else if ((Input.GetAxisRaw("Vertical")) == -1f)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f);
            movementHistory.Add("down");
            yPos -= 1;
        }
        positionHistory.Add(new List<int> { xPos,yPos});
        //Debug.Log("Movement: ["+positionHistory[positionHistory.Count-1][0]+","+ positionHistory[positionHistory.Count - 1][1]+"]");
        //Debug.Log("Movement: " + movementHistory[movementHistory.Count - 1]);
        printArray(movementHistory,"Movement: ");
        
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
    public void rebound()
    {
        string lastMove = movementHistory[movementHistory.Count - 1].ToString();
        canMove = false;
        if (lastMove == "up")
        {
            movePoint.position += new Vector3(0f, -0.99f, 0f);
            yPos -= 1;
        }
        else if (lastMove == "down")
        {
            movePoint.position += new Vector3(0f, 0.99f, 0f);
            yPos += 1;
        }
        else if (lastMove == "left")
        {
            movePoint.position += new Vector3(0.99f, 0f, 0f);
            xPos += 1;
        }
        else if (lastMove == "right")
        {
            movePoint.position += new Vector3(-0.99f, 0f, 0f);
            xPos -= 1;
        }
        positionHistory.RemoveAt(positionHistory.Count - 1);
        movementHistory.RemoveAt(movementHistory.Count - 1);
        StartCoroutine(resumeMove(2f));
        //Debug.Log("Rebound: [" + positionHistory[positionHistory.Count - 1][0] + "," + positionHistory[positionHistory.Count - 1][1] + "]");
        printArray(movementHistory,"Rebound: ");
        
    }

    IEnumerator resumeMove(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
