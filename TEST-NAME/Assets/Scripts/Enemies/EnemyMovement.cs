using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private float leftLimit = 0;
    private float rightLimit = -35;
    private float topLimit = 22;
    private static Vector3 Up = new Vector3(0, 1.0f, 0);
    private static Vector3 Down = new Vector3(0, -1.0f, 0);
    private static Vector3 left = new Vector3(1.0f, 0, 0);
    private static Vector3 right = new Vector3(-1.0f, 0, 0);
    private Vector3 directionVector;
    private Vector3 collisionNormal;
    enum Directions
    {
        Up,
        Down,
        Left,
        Right,
    }

    private Directions direction;
    private List<Directions> directionsAvailable = new List<Directions> { Directions.Up, Directions.Down, Directions.Left, Directions.Right };

    private bool leftAvailable = true;
    private bool rightAvailable = true;
    private bool upAvailable = true;
    private bool downAvailable = true;
    // Start is called before the first frame update
    void Start()
    {   
        direction = Directions.Left;
        StartCoroutine(PickRandomDirectionCoroutine());
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
        transform.position += directionVector * Time.deltaTime * speed;        
    }

    void BounceOfBoundaries()
    {
        if (transform.position.x <= rightLimit)
           direction = Directions.Left;
        else if (transform.position.x >= leftLimit)
            direction = Directions.Right;
    }

    void UpdateDirection()
    {
        switch (direction)
        {
            case Directions.Up:
                directionVector = Up;
                break;
            case Directions.Down:
                directionVector = Down;
                break;
            case Directions.Left:
                directionVector = left;
                break;
            case Directions.Right:
                directionVector = right;
                break;
        }
    }


    IEnumerator PickRandomDirectionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            PickRandomDirection();
        }
    }

    void PickRandomDirection()
    {
        IfEnemyIsAtTopOfMap();
        PrintAvailableDirections(); 

        int randomNum = Random.Range(0, directionsAvailable.Count);
        direction = GetRandomDirectionAvailable(randomNum);

        Debug.Log("Chosen direction: " + direction);
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision Exit");
        Directions tempDir = GetDirectionFromCollisionNormal(GetNormalFromCollision(collision)); 
        Debug.Log("Temp Direction: " + tempDir);
        SetDirectionsAvailableTrueBasedOnDirection(tempDir);
        PrintAvailableDirections();
    }

    void SetDirectionsAvailableTrueBasedOnDirection(Directions dir)
    {
        switch (dir)
        {
            case Directions.Up:
                upAvailable = true;
                break;
            case Directions.Down:
                downAvailable = true;
                break;
            case Directions.Left:
                leftAvailable = true;
                break;
            case Directions.Right:
                rightAvailable = true;
                break;
        }
    }

    Vector3 GetNormalFromCollision(Collision collision)
    {
        Vector3 directionNormal = transform.position - collision.transform.position;
        Debug.Log("Collision Direction Normal Without Rounding: " + directionNormal);
        float angle = Mathf.Acos(directionNormal.y / directionNormal.x);
        Debug.Log("Angle: " + angle);    
        directionNormal = directionNormal.normalized;
        directionNormal.x = Mathf.Round(directionNormal.x);
        directionNormal.y = Mathf.Round(directionNormal.y);
        directionNormal.z = Mathf.Round(directionNormal.z);
        Debug.Log("Get Normal From Collision Direction Normal: " + directionNormal.normalized);
        return directionNormal.normalized; 
    }

    Directions GetDirectionFromCollisionNormal(Vector3 collisionNormal)
    {
        if (collisionNormal == Vector3.up)
            return Directions.Up;
        else if (collisionNormal == Vector3.down)
            return Directions.Down;
        else if (collisionNormal == Vector3.right)
            return Directions.Right;
        else if (collisionNormal == Vector3.left)
            return Directions.Left;
        else
            return Directions.Up;
    } 

    Directions GetRandomDirectionAvailable(int randomNum)
    {
        if (randomNum == 0 && upAvailable)
            return Directions.Up;
        else 
            randomNum = 1;

        if (randomNum == 1 && downAvailable)
            return Directions.Down;
        else
            randomNum = 2;

        if (randomNum == 2 && leftAvailable)
            return Directions.Left;
        else 
            randomNum = 3;

        if (randomNum == 3 && rightAvailable)
            return Directions.Right;
        else
            return Directions.Up;
    }
    void IfEnemyIsAtTopOfMap()
    {
        if (transform.position.y >= topLimit)
            upAvailable = false;
        else
            upAvailable = true;
    }

    void PrintAvailableDirections()
    {
        Debug.Log("Directions Available: #############");
        Debug.Log("Up: " + upAvailable);
        Debug.Log("Down: " + downAvailable);
        Debug.Log("Left: " + leftAvailable);
        Debug.Log("Right: " + rightAvailable);
        Debug.Log("-----------------");
    }

    void OnCollisionEnter(Collision collision)
    {
        CollidesWithPillar(collision); 
        RemoveDirectionsFromDirectionsAvailableList(collision); 
        Debug.Log("Collision Enter");
        PickRandomDirection();
    }

    // void OnCollisionStay(Collision collision)
    // {
    //     //Debug.Log("Collision Stay");
    //     RemoveDirectionsFromDirectionsAvailableList(collision);
    // }

    void RemoveDirectionsFromDirectionsAvailableList(Collision collision)
    {
        collisionNormal = collision.contacts[0].normal;
        Debug.Log("Collision Normal: " + collisionNormal);
        if (collisionNormal == Vector3.up)
        {
            Debug.Log("down false");
            downAvailable = false;
        }
        else if (collisionNormal == Vector3.down)
        {
            Debug.Log("up false");
            upAvailable = false;
        }
        else if (collisionNormal == Vector3.right)
        {
            Debug.Log("left false");
            leftAvailable = false;
        }
        else if (collisionNormal == Vector3.left)
        {
            Debug.Log("right false");
            rightAvailable = false;
        }
    }

    void CollidesWithPillar(Collision collision)
    {
        if (collision.gameObject.tag == "Pillar")
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}