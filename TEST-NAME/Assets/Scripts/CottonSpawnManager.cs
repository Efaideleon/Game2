using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonSpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject cottonPrefab;

    [SerializeField]
    GameObject leftWall;

    [SerializeField]
    GameObject rightWall;

    [SerializeField]
    GameObject topLeftWall;

    [SerializeField]
    GameObject topRightWall;

    [SerializeField]
    GameObject bottomWall;

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
        xRange = Mathf.Abs(Mathf.RoundToInt(rightWall.transform.position.x));
        yRange = Mathf.Abs(Mathf.RoundToInt(topLeftWall.transform.position.y) - 1);

        // loading the cotton
        for (int i = 0; i < xRange; i++)
        {
            for (int j = 0; j < yRange; j++)
            {
                Instantiate(
                    cottonPrefab,
                    new Vector3((leftWall.transform.position.x - 1) - i, 1 + j, 0),
                    cottonPrefab.transform.rotation
                );
            }
        }
    }
}
