using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private float leftLimit = 0;
    private float rightLimit = -35;
    private float direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        BounceOfBoundaries();
        transform.position += new Vector3(1.0f, 0, 0) * Time.deltaTime * speed * direction;        
    }

    void BounceOfBoundaries()
    {
        if (transform.position.x <= rightLimit)
        {
            direction = 1;
        }
        else if (transform.position.x >= leftLimit)
        {
            direction = -1;
        }
    }
}
