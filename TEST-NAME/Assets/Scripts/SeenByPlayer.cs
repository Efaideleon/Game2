using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenByPlayer : MonoBehaviour
{
    bool seenByPayer = false;

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!seenByPayer)
        {
            SetBatteryOnTopOfCotton();
        }
    }

    void SetBatteryOnTopOfCotton()
    {
        if (GetDistanceToPlayer() < 5.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
            seenByPayer = true;
        }
    }

    float GetDistanceToPlayer() 
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }
}
