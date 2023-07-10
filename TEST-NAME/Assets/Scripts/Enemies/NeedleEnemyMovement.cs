using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleEnemyMovement : MonoBehaviour
{
    private Rigidbody enemyRb;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pillar")
        {
            //enemyRb.useGravity = false;
        }
    }
}
