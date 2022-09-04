using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageBody : MonoBehaviour
{
    [HideInInspector]public int powerUpType = 0;
    [HideInInspector] public bool isHealth = true;
    [Header("Snake PowerUp Colors")]
    [SerializeField] Color[] colors = new Color[3] ;
    Color originalColor ;
    //powerUpType= 1 -> shield power up
    //powerUpType= 2 -> score booster power up
    //powerUpType= 3 -> speed booster power up
    bool onlyOnetime = false;
    [Header("SnakeBody List")]
    public List<Transform> snakeBody;
    [Header("SnakeBody Parts Sprite")]
    [SerializeField] Transform snakebodyGameobject;
    [HideInInspector] public bool isDestroying = false;
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] PowerSpawn PowerSpawner;
    [SerializeField] GameObject powerUpHalo;
    [SerializeField] GameObject otherplayer= null;
    [SerializeField] TextMeshProUGUI ultimateStatement = null;
    [Header("Audio Clips")]
    [SerializeField] AudioClip food_sound;
    [SerializeField] AudioClip power_sound;
    [SerializeField] AudioClip antifood_sound;
    tagType currentEnum ;          //enum for the current gameobject
    tagType collisionEnum ;        //enum for the object detected OnTrigger()
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        powerUpType = 0;
        onlyOnetime = false;
        ultimateStatement.text = "One of the Player died itself!";
        isHealth = false;
        currentEnum = GetComponent<EnumDefiner>().GetTagType();
        spriteRenderer= powerUpHalo.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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
        onlyOnetime = false;
        spriteRenderer.color = originalColor;   //to be changed here
    }
    public int GetPowerType()
    {
        return powerUpType;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDestroying)
        {
            return;
        }

        collisionEnum = collision.gameObject.GetComponent<EnumDefiner>().GetTagType();

        if(collisionEnum==tagType.food)                             
        {
            Transform segment= Instantiate(this.snakebodyGameobject);
            AudioSource.PlayClipAtPoint(food_sound, Camera.main.transform.position);
            segment.position = snakeBody[snakeBody.Count - 1].position;     
            snakeBody.Add(segment);
            int multiplier = 1;
            if (powerUpType == 2)
                multiplier = 2;
            scoreKeeper.score += (multiplier*10);
            Destroy(collision.gameObject);

        }
        else if(collisionEnum == tagType.antiFood)        
        {
            Transform instance = snakeBody[snakeBody.Count - 1];
            AudioSource.PlayClipAtPoint(antifood_sound, Camera.main.transform.position);
            snakeBody.Remove(instance);//snakeBody[snakeBody.Count - 1]);
            Destroy(instance.gameObject);
            scoreKeeper.score -= 10;
            Destroy(collision.gameObject);
         
            //Destroy(collision.gameObject);
        }
        else if (collisionEnum == tagType.shieldPower)             
        {
            powerUpType = 1;
            AudioSource.PlayClipAtPoint(power_sound, Camera.main.transform.position);
            PowerSpawner.timePassed = 0f;
            spriteRenderer.color = colors[0];    //to use getcomponent
            Destroy(collision.gameObject);
        }
        else if (collisionEnum == tagType.scoreBooster)                  
        {
            powerUpType=2;
            AudioSource.PlayClipAtPoint(power_sound, Camera.main.transform.position);
            PowerSpawner.timePassed = 0f;
            spriteRenderer.color = colors[1];       //getcomponent
            Destroy(collision.gameObject);
        }
        else if (collisionEnum == tagType.speedBooster)               
        {
            powerUpType = 3;
            AudioSource.PlayClipAtPoint(power_sound, Camera.main.transform.position);
            PowerSpawner.timePassed= 0f;
            spriteRenderer.color = colors[2];
            Destroy(collision.gameObject);
        }
        else if(collisionEnum == currentEnum && GetComponent<Movement>().haveTakenInput && (powerUpType!= 1))
        {
            InitiateDestruction();
        }
        else if(collisionEnum == tagType.player && currentEnum== tagType.player2)
        {
            if (otherplayer.GetComponent<ManageBody>().powerUpType != 1)
            {
                ultimateStatement.text= "Player 2 bit Player 1";
                if (otherplayer != null)
                {
                    int n = otherplayer.GetComponent<ManageBody>().snakeBody.Count;
                    for (int i = 0; i < n; i++)
                    {
                        otherplayer.GetComponent<ManageBody>().snakeBody[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                    }
                }
            }
            InitiateDestruction();
        }
        else if(collisionEnum== tagType.player2 && currentEnum== tagType.player)
        {
            if(otherplayer.GetComponent<ManageBody>().powerUpType!=1)
            {
                ultimateStatement.text= "Player 1 bit Player 2";
                if (otherplayer != null)
                {
                    int n = otherplayer.GetComponent<ManageBody>().snakeBody.Count;
                    for (int i = 0; i < n; i++)
                    {
                        otherplayer.GetComponent<ManageBody>().snakeBody[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                    }
                }
            }
            InitiateDestruction();
        }

    }


    public void InitiateDestruction()
    {
        GetComponent<Movement>().movespeed = 0;
        isDestroying = true;
    }
    
}
