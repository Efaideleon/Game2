using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNeedleMovementAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent; 
    [SerializeField] LayerMask cottonLayerMask;
    [SerializeField] LayerMask wallsLayerMask;
    private Vector3 destPoint; 
    private bool moving = false;
    private enum Direction
    {
        Horizontal,
        Vertical
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            FindFinalDestination();
            destPoint.z = -1.46f;
            Debug.Log("Destination: " + destPoint);
            agent.SetDestination(destPoint);
            moving = true;
        }
        if (IsDestinationReached())
        {
            Debug.Log("Destination Reached");
            moving = false;
        }
    }
    void FindFinalDestination()
    {
        FindRandomPoint();
        while(!IsPointAvailable((int)destPoint.x, (int)destPoint.y))
        {
            FindRandomPoint();
        }
    }

    void FindRandomPoint()
    {
        Direction direction = PickRandomDirection();
        switch(direction)
        {
            case Direction.Horizontal:
                destPoint.x = FindRandomX();
                destPoint.y = transform.position.y;
                break;
            case Direction.Vertical:
                destPoint.x = transform.position.x;
                destPoint.y = FindRandomY();
                break;
        }
    }

    int FindRandomX()
    {
        int x = Random.Range(-37, 1);
        return x;
    }

    int FindRandomY()
    {
        int y = Random.Range(2, 25);
        return y;
    }    

    bool IsPointAvailable(int x, int y)
    {
        Vector3 raycastPoint = new Vector3(x, y, 5);

        RaycastHit hit;
        if (Physics.Raycast(raycastPoint, Vector3.back, out hit, 10, cottonLayerMask)
            || Physics.Raycast(raycastPoint, Vector3.back, out hit, 10, wallsLayerMask))
        {
            Debug.Log("Point " + x + " " + y + "is not available");
            Debug.DrawLine(raycastPoint, hit.point, Color.white, 10);
            return false;
        }
        else 
        {
            Debug.Log("Point " + x + " " + y + " available");
            Debug.DrawLine(raycastPoint, hit.point, Color.red, 10);
            return true;
        }
    }

    Direction PickRandomDirection()
    {
        int direction = Random.Range(0, 2);
        if (direction == 0)
        {
            return Direction.Horizontal;
        }
        else
        {
            return Direction.Vertical;
        }
    }

    bool IsDestinationReached()
    {
        float dist = Vector3.Distance(destPoint, transform.position);
        if (dist < 2f)
        {
            return true;
        }
        return false;
    }
}
