using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBall : MonoBehaviour
{
    public bool collideWCotton = false;
    public bool collidesWithRubberBall = false;        

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Cotton"))
        {
            collideWCotton = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("RubberBall"))
        {
            collidesWithRubberBall = true;
        }  
    }

    public bool CollidesWithRubberBall()
    {
        return collidesWithRubberBall;
    }
}
