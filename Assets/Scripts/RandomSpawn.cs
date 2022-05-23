using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] Food= new GameObject[2];
    GameObject instance=null;
    Vector2 randomPosition;

    //Food[0]-> food, will increase the snake's size
    //Food[1]-> anti-food, will decrease the snake's size
    private void Awake()
    {
        
        instance = Instantiate(Food[0], randomPosition, Quaternion.identity);
    }
    void Update()
    {
        PositionRandomizer();
        if(instance==null)
        {
            int randomNumber = Random.Range(0, 2);
            instance = Instantiate(Food[randomNumber], randomPosition, Quaternion.identity);
        }
    }

    void PositionRandomizer()
    {
        Camera cam = Camera.main;
        Vector2 lower_limit = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 upper_limit = cam.ViewportToWorldPoint(new Vector2(1, 1));
        randomPosition.x = Random.Range(lower_limit.x+1, upper_limit.x-1);
        randomPosition.y = Random.Range(lower_limit.y+1, upper_limit.y-1);
    }

}
