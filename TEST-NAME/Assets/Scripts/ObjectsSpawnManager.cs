using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject batteriesObject;

    [SerializeField] GameObject clothButtonsObject;

    [SerializeField] GameObject rubberBallPrefab;

    int leftLimit;
    int rightLimit;
    int topLimit;
    int bottomLimit;

    int offset = 5;
    private CottonSpawnManager cottonSpawnManagerScript;

    public int numOfRubberBallsToSpawn = 3;
    public int numOfNeedleEnemiesToSpawn = 3;
    public int numOfBatteriesToSpawn = 3;
    public int numOfButtonsToSpawn = 3;

    // Start is called before the first frame update
    void Start()
    {
        cottonSpawnManagerScript = GameObject.Find("CottonSpawnManager").GetComponent<CottonSpawnManager>();
        leftLimit = Mathf.RoundToInt(cottonSpawnManagerScript.leftWall.transform.position.x) - offset;
        rightLimit = Mathf.RoundToInt(cottonSpawnManagerScript.rightWall.transform.position.x) + offset;
        topLimit = Mathf.RoundToInt(cottonSpawnManagerScript.topLeftWall.transform.position.y) - offset;
        bottomLimit = Mathf.RoundToInt(cottonSpawnManagerScript.bottomWall.transform.position.y) + offset;
        
        SpawnRubberBalls();
        SpawnBatteries();
        SpawnButtons();
    }

    // Update is called once per frame
    void Update() { }

    public void SpawnBatteries()
    {
        for (int i = 0; i < numOfBatteriesToSpawn; i++)
        {
            GameObject batteryObject = batteriesObject.transform.GetChild(i).gameObject; 
            batteryObject.SetActive(true);
            batteryObject.transform.position = new Vector3(GetRandomX(), GetRandomY(), 0);
        }
    }

    public void SpawnButtons()
    {
        for (int i = 0; i < numOfButtonsToSpawn; i++)
        {
            GameObject buttonObject = clothButtonsObject.transform.GetChild(i).gameObject;
            buttonObject.SetActive(true);
            buttonObject.transform.position = new Vector3(GetRandomX(), GetRandomY(), 0);
        }
    }

    public void SpawnRubberBalls()
    {
        for (int i = 0; i < numOfRubberBallsToSpawn; i++)
        {
            // Spawn if the rubber balls aren't overlapping with eachother           
            SpawnARubberBall();
        }
    }


    public void SpawnARubberBall()
    {
        float randomX = GetRandomX();
        float randomY = GetRandomY();
        float ballRadius = 2 * rubberBallPrefab.GetComponent<SphereCollider>().radius;
        randomY += ballRadius + 2.5f;
        randomX += ballRadius + 2.5f;
        GameObject rubberBallInstance = Instantiate(
            rubberBallPrefab,
            new Vector3(randomX, randomY, 0),
            rubberBallPrefab.transform.rotation
        );

        Debug.Log(rubberBallInstance.GetComponent<RubberBall>().CollidesWithRubberBall());
        if (rubberBallInstance.GetComponent<RubberBall>().CollidesWithRubberBall())
        {
            Debug.Log("Collides with another rubber ball");
            Destroy(rubberBallInstance);
            SpawnARubberBall();
        }
    }

   private float GetRandomX()
    {
        return Random.Range(leftLimit, rightLimit);
    }

    private float GetRandomY()
    {
        return Random.Range(bottomLimit, topLimit);
    }
}
