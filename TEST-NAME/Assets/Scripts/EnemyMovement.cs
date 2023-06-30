using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private float leftLimit = 0;
    private float rightLimit = -35;
    private static Vector3 horizontal = new Vector3(1.0f, 0, 0);
    private static Vector3 vertical = new Vector3(0, 1.0f, 0);
    private Vector3 directionVector;
    private int directionValue;
    enum DirectionState
    {
        Forwards,
        Backwards
    }
    enum DirectionAxis
    {
        Horizontal,
        Vertical
    }
    private DirectionState directionState;
    private DirectionAxis directionAxis;
    // Start is called before the first frame update
    void Start()
    {   
        directionState = DirectionState.Forwards;
        directionAxis = DirectionAxis.Horizontal;
        StartCoroutine(ChangeDirectionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        Move();
    }

    void Move()
    {
        BounceOfBoundaries();
        transform.position += directionVector * Time.deltaTime * speed * directionValue;        
    }

    void BounceOfBoundaries()
    {
        if (transform.position.x <= rightLimit)
            directionState = DirectionState.Forwards;
        else if (transform.position.x >= leftLimit)
            directionState = DirectionState.Backwards;
    }

    void UpdateDirection()
    {
        switch (directionAxis)
        {
            case DirectionAxis.Horizontal:
                directionVector = horizontal;
                break;
            case DirectionAxis.Vertical:
                directionVector = vertical;
                break;
        }

        switch(directionState)
        {
            case DirectionState.Forwards:
                directionValue = 1;
                break;
            case DirectionState.Backwards:
                directionValue = -1;
                break;
        }
    }

    IEnumerator ChangeDirectionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            PickRandomDirection();
        }
    }

    void PickRandomDirection()
    {
        directionAxis = (DirectionAxis)Random.Range(0, 2);
        directionState = (DirectionState)Random.Range(0, 2);
    }

    void OneCollsionEnter(Collision collision)
    {
        Debug.Log("Collision");
        PickRandomDirection();
    }
}
