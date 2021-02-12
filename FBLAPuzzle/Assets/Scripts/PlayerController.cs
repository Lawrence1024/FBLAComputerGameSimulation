using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

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
            if (Input.GetAxisRaw("Horizontal") == 1f)
            {
        //        Debug.Log("New----------------------------------");
         //       Debug.Log("old" + movePoint.position);
         //       Debug.Log("new" + movePoint.position + new Vector3(0.99f, 0f, 0f));
                
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0.99f, 0f, 0f), 0.2f, whatStopsMovement)) ;
                {
                    Debug.Log("New----------------------------------");
                    Debug.Log("old" + movePoint.position);
                    Debug.Log("new" + (movePoint.position + new Vector3(0.99f, 0f, 0f)));
                    movePoint.position += new Vector3(0.99f, 0f, 0f);
                }
            }
            if (Input.GetAxisRaw("Horizontal") == -1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-0.99f, 0f, 0f), 0.2f, whatStopsMovement)) ;
                {
                    movePoint.position += new Vector3(-0.99f, 0f, 0f);
                }
            }
            if (Input.GetAxisRaw("Vertical") == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 0.99f, 0f), 0.2f, whatStopsMovement)) ;
                {
                    movePoint.position += new Vector3(0f, 0.99f, 0f);
                }
            }
            if (Input.GetAxisRaw("Vertical") == -1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -0.99f, 0f), 0.2f, whatStopsMovement)) ;
                {
                    movePoint.position += new Vector3(0f, -0.99f, 0f);
                }
            }
        }
    }
}
