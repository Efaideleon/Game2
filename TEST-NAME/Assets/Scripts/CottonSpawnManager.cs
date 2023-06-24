using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSpawnManager : MonoBehaviour
{
    public GameObject cottonPrefab;

    public GameObject leftWall;

    public GameObject rightWall;

    public GameObject topLeftWall;

    public GameObject topRightWall;

    public GameObject bottomWall;

    private int xRange;
    private int yRange;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCotton();
    }

    // Update is called once per frame
    void Update() { }

    void SpawnCotton()
    {
        // get the max size for the cotton
        xRange =
            Mathf.Abs(
                Mathf.RoundToInt(rightWall.transform.position.x - leftWall.transform.position.x)
            ) - 1;
        yRange = Mathf.Abs(
            Mathf.RoundToInt(topLeftWall.transform.position.y - bottomWall.transform.position.y) - 1
        );
        Debug.Log("xRange: " + xRange + " yRange: " + yRange);
        // loading the cotton
        for (int i = 0; i < xRange; i++)
        {
            for (int j = 0; j < yRange; j++)
            {
                Instantiate(cottonPrefab, new Vector3(-i, j, 0), cottonPrefab.transform.rotation);
            }
        }
    }
}
