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

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        movePoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (getPushed)
            {
                if (!thereIsObstacle())
                {
                    move();
                }
            }

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
    void move()
    {
        makeMovement();
        getPushed = false;
    }
    void makeMovement()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.99f, 0f);
        }
    }
    bool thereIsBox()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.99f, 0f, 0f), 0.2f, boxLayer))
            {
                return true;
            }
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
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
        getPushed = true;
    }
}
