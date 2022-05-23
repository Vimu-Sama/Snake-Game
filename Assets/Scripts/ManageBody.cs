using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBody : MonoBehaviour
{
    int powerUpType = 0;
    //powerUpType= 1 -> shield power up
    //powerUpType= 2 -> score booster power up
    //powerUpType= 3 -> speed booster power up
    bool onlyOnetime = false;
    [Header("SnakeBody List")]
    [SerializeField] List<Transform> snakeBody;
    [Header("SnakeBody Parts Sprite")]
    [SerializeField] Transform snakebodyGameobject;
    [HideInInspector] public bool isDestroying = false;
    [SerializeField] ScoreKeeper scoreKeeper;
    private void Awake()
    {
        powerUpType = 0;
        onlyOnetime = false;
       //snakeBody = new List<Transform>();
       //snakeBody.Add(this.transform);
    }
    private void FixedUpdate()
    {
        //Debug.Log("power value" + powerUpType);
        //snakeBody[n].GetComponent<Transform>().position= transform.position;
        if((powerUpType!=0) && !onlyOnetime)
        {
            onlyOnetime = true;
            StartCoroutine(DeactivatePowerUp(powerUpType));
        }
        if (!isDestroying)
        {
            for (int i = snakeBody.Count - 1; i > 0; i--)
            {
                if (snakeBody[i] != null && snakeBody[i - 1] != null)
                    snakeBody[i].position = snakeBody[i - 1].position;
                //snakeBody[i].position = Vector3.MoveTowards(snakeBody[i - 1].position, snakeBody[i].position, 0.5f);

            }
            this.GetComponent<Movement>().ChangePosition();
        }
        
    }
    

    IEnumerator DeactivatePowerUp(int typeofPowerUp)
    {
        yield return new WaitForSeconds(5);
        powerUpType = 0;
        Debug.Log("the thing got settled");
        onlyOnetime = false;
    }
    public int GetPowerType()
    {
        return powerUpType;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Food")
        {
            Transform segment= Instantiate(this.snakebodyGameobject);
            segment.position = snakeBody[snakeBody.Count - 1].position;     
            snakeBody.Add(segment);
            int multiplier = 1;
            if (powerUpType == 2)
                multiplier = 2;
            scoreKeeper.score += (multiplier*10);
            
        }
        else if(collision.tag== "Anti Food" && scoreKeeper.score!=0)
        {
            Transform instance = snakeBody[snakeBody.Count - 1];
            snakeBody.Remove(instance);//snakeBody[snakeBody.Count - 1]);
            Destroy(instance.gameObject);
            scoreKeeper.score -= 10;
            //Destroy(collision.gameObject);
        }
        else if (collision.tag == "ShieldPower")
        {
            powerUpType = 1;
        }
        else if (collision.tag == "ScoreBooster")
        {
            powerUpType=2;
        }
        else if (collision.tag == "SpeedBooster")
        {
            powerUpType = 3;
        }
        else if(collision.tag== "Player" && GetComponent<Movement>().haveTakenInput && (powerUpType!= 1))
        {
            InitiateDestruction();
        }
        Destroy(collision.gameObject);
    }


    public void InitiateDestruction()
    {
        GetComponent<Movement>().movespeed = 0;
        isDestroying = true;
        StartCoroutine(DestroySnake());
    }
    IEnumerator DestroySnake()
    {
        for(int i=snakeBody.Count-1; i>=0; i--)
        {
            //Destroy(snakeBody[i].gameObject);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
