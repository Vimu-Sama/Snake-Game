using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] Powers = new GameObject[2];
    GameObject instance = null;
    Vector2 randomPosition;
    [HideInInspector] public float timePassed = 0;


    //Powers[0]-> shield power up
    //Powers[1]-> score boost power up
    //Powers[2]-> speed boost power up
    private void Awake()
    {
        instance = Instantiate(Powers[0], randomPosition, Quaternion.identity);
    }
    void Update()
    {
        timePassed += Time.deltaTime;
        PositionRandomizer();
        if (instance == null && timePassed>Random.Range(20,60))
        {
            int randomNumber = Random.Range(0, 3);
            instance = Instantiate(Powers[randomNumber], randomPosition, Quaternion.identity);
            timePassed = 0f; 
        }

    }
    void PositionRandomizer()
    {
        Camera cam = Camera.main;
        Vector2 lower_limit = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 upper_limit = cam.ViewportToWorldPoint(new Vector2(1, 1));
        randomPosition.x = Random.Range(lower_limit.x + 1, upper_limit.x - 1);
        randomPosition.y = Random.Range(lower_limit.y + 1, upper_limit.y - 1);
    }
}