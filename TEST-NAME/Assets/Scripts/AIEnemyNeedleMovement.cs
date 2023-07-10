using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIEnemyNeedleMovement : MonoBehaviour
{
    Ray movePosition; 
    bool walkpointSet;
    Vector3 destPoint;
    [SerializeField] float range = 10f;
    private Direction lastDirectionTaken;
    private float speed = 5;
    private Rigidbody rb;
    private Vector3 moveRange;
    enum Direction
    {
        Horizontal,
        Vertical
    }

    struct Point
    {
        public float x;
        public float y;
        public bool isCrossSection;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!walkpointSet)
        {
            SearchForDest();
        }
        if (walkpointSet)
        {
            moveRange.x = (float)(moveRange.x/(1/.75));
            moveRange.y = (float)(moveRange.y/(1/.75));
            rb.MovePosition(transform.position + moveRange);
        }
        if (Vector3.Distance(transform.position, destPoint) < 0.5f)
        {
            Debug.Log("Reached");
            walkpointSet = false;
        }
    }

    void SearchForDest()
    {
        Point point = CalculateRange();
        
        //float x_p = Random.Range(0f, point.x);
        //float y_p = Random.Range(0f, point.y);
        Debug.Log("x: " + point.x + " y: " + point.y);
        float x = point.x;
        float y = point.y;
        float x_temp = x;
        float y_temp = y;
        float randomDir = Random.Range(0, 2);
        if (randomDir == 1 && y != 0)
        {
            x = 0;
        }
        else
        {
            y = 0;
        }

        if (lastDirectionTaken == Direction.Horizontal && point.isCrossSection)
        {
            x = 0;
            y = y_temp;
            Debug.Log("Cross Section: " + lastDirectionTaken);
            lastDirectionTaken = Direction.Vertical;
            Debug.Log("At Cross Section: " + point.isCrossSection);
        }
        else if (lastDirectionTaken == Direction.Vertical && point.isCrossSection)
        {
            y = 0;
            x = x_temp;
            Debug.Log("Cross Section: " + lastDirectionTaken);
            lastDirectionTaken = Direction.Horizontal;
            Debug.Log("At Cross Section: " + point.isCrossSection);
        }

        if (x == 0)
        {
            lastDirectionTaken = Direction.Vertical;
        }
        else if(y == 0)
        {
            lastDirectionTaken = Direction.Horizontal;
        }

        walkpointSet = true;
        Debug.Log("Current: " + transform.position.x + " y: " + transform.position.y);
        moveRange = new Vector3(x, y, 0);
        destPoint = transform.position + moveRange;
        moveRange = moveRange.normalized;
        Debug.Log("Dest: x: " + destPoint.x + " y: " + destPoint.y);
        Debug.Log("Move Range: x: " + moveRange.x + " y: " + moveRange.y);
    }

    Point CalculateRange()
    {
        Point point = new Point();
        float rangeX = 0; 
        float rangeX_n =0;
        float rangeY = 0;
        float rangeY_n = 0;

        float max = 0;   
        int value = 1;
        bool first = true;
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 40; j++) 
            {
                max = j;
                if (Physics.Raycast(transform.position, new Vector3(0.0f, value, 0.0f), max))
                {
                    if(value == 1 && first)
                    {
                        rangeY = j;
                        first = false;
                        break;
                    }
                    else if(value == -1 && first)
                    {
                        rangeY_n = -j;
                        first = false; 
                        break;
                    }
                }
            }
            value = -value;
            first = true;
        }

        first = true;
        value = 1;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                max = j;
                if (Physics.Raycast(transform.position, new Vector3(value, 0.0f, 0.0f), max))
                {
                    if (value == 1 && first)
                    {
                        Debug.Log("Hit");
                        rangeX = j;
                        first = false;
                        break;
                    }
                    else if (value == -1 && first)
                    {
                        rangeX_n = -j;
                        first = false;
                        break;
                    }
                }
            }
            value = -value;
            first = true;
        }

        float largestX = findMax(rangeX, rangeX_n); 
        float largestY = findMax(rangeY, rangeY_n);
        
        if (Mathf.Abs(largestX) <= 3)
        {
            point.x = 0;
        }
        else {
            point.x = Random.Range(0, largestX); //the range it moves
        }

        if (Mathf.Abs(largestY) <= 3)
        {
            point.y = 0;
        }
        else
        {
            point.y = Random.Range(0, largestY);
        }

        Debug.Log("Range Y: " + rangeY);
        Debug.Log("Range Y_n: " + rangeY_n);
        Debug.Log("Range X: " + rangeX);
        Debug.Log("Range X_n: " + rangeX_n);

        if (IsAtCrossSection(rangeX, rangeX_n, rangeY, rangeY_n))
        {
            point.isCrossSection = true;
        }
        else
        {
            point.isCrossSection = false;
        }
        return point;
    }

    float findMax(float x, float y)
    {
        return Mathf.Abs(x) < Mathf.Abs(y) ? y : x;
    }

    bool IsAtCrossSection(float x, float x_n, float y, float y_n)
    {
        x = Mathf.Abs(x);
        x_n = Mathf.Abs(x_n);
        y = Mathf.Abs(y);
        y_n = Mathf.Abs(y_n);
        return (x > 3 || x_n > 3) && (y > 3 || y_n > 3); 
    }
}
