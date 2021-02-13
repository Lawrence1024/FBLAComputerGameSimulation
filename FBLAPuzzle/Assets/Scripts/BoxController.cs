using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public GameObject player;

    public LayerMask whatStopsMovement;
    public LayerMask boxLayer;
    public bool getPushed = false;
    private string lastPlayerMovement;

    public int xPos;
    public int yPos;
    public List<List<int>> positionHistory = new List<List<int>>();


    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        movePoint.position = transform.position;
        positionHistory.Add(new List<int> { xPos, yPos });
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
    }
    bool thereIsObstacle()
    {
        if(lastPlayerMovement=="up"&& Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 0.99f, 0f), 0.2f, whatStopsMovement))
        {
            return true;
        }else if (lastPlayerMovement == "down" && Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -0.99f, 0f), 0.2f, whatStopsMovement))
        {
            return true;
        }
        else if (lastPlayerMovement == "left" && Physics2D.OverlapCircle(movePoint.position + new Vector3(-0.99f, 0f, 0f), 0.2f, whatStopsMovement))
        {
            return true;
        }
        else if (lastPlayerMovement == "right" && Physics2D.OverlapCircle(movePoint.position + new Vector3(0.99f, 0f, 0f), 0.2f, whatStopsMovement))
        {
            return true;
        }
      //  if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f), 0.2f, whatStopsMovement))
      //  {
      //      return true;
      //  }
      //  if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f), 0.2f, whatStopsMovement))
      //  {
      //      return true;
      //  }
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
        if (lastPlayerMovement=="up")
        {
            movePoint.position += new Vector3(0f, 0.99f, 0f);
            xPos += 1;
        }else if (lastPlayerMovement == "down")
        {
            movePoint.position += new Vector3(0f, -0.99f, 0f);
            xPos -= 1;
        }else if (lastPlayerMovement == "left")
        {
            movePoint.position += new Vector3(-0.99f, 0f, 0f);
            yPos += 1;
        }else if (lastPlayerMovement == "right")
        {
            movePoint.position += new Vector3(0.99f, 0f, 0f);
            yPos -= 1;
        }
        positionHistory.Add(new List<int> { xPos, yPos });
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
        lastPlayerMovement = player.GetComponent<PlayerController>().movementHistory[player.GetComponent<PlayerController>().movementHistory.Count - 1].ToString();
        getPushed = true;
        if (thereIsObstacle())
        {
            player.GetComponent<PlayerController>().rebound();
        }
        else
        {
            move();
        }
    }
}
