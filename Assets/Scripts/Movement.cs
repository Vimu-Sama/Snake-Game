using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movespeed = 1f;
    
    bool[] directionArrays= new bool[4] ;
    //0- up, 1- left, 2- right, 3- down
    [HideInInspector] public bool haveTakenInput=false ;
    public bool isPlayer2 = false;
    Vector3 current_direction;
    bool control = false;
    float original_speed;
    private void Awake()
    {
        control = false;
        original_speed= movespeed; 
        if(isPlayer2)
        {
            current_direction = new Vector3(-movespeed, 0, 0);
            this.transform.eulerAngles = new Vector3(0, 0,180);
        }
            
        else
            current_direction = new Vector3(movespeed, 0, 0);
        directionArrays[2] = true;
    }

    public void ChangePosition()
    {

        transform.position += current_direction * Time.deltaTime;
        validatePosition();
             
    }

    private void Update()
    {
        if(GetComponent<ManageBody>().GetPowerType()==3)
        {
            control = true;
            movespeed = 6f;
        }
        else if(GetComponent<ManageBody>().GetPowerType() !=3 && control)
        {
            movespeed = original_speed;
        }
        GetDirection(); 
    }



    void GetDirection()
    {
        if(!isPlayer2)
        {
            if (Input.GetKeyDown(KeyCode.W) && !directionArrays[3])
            {
                current_direction = new Vector3(0, movespeed, 0);
                //gameObject.transform.RotateAround(transform.position, Vector3.forward, 90f);
                //transform.Rotate(0,0,90, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
                SetDirectionBools(0);
            }
            else if (Input.GetKeyDown(KeyCode.A) && !directionArrays[2])
            {
                current_direction = new Vector3(movespeed * (-1), 0, 0);
                //gameObject.transform.RotateAround(transform.position, Vector3.forward, 180f);
                //transform.Rotate(0,0, 180, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, 180);
                SetDirectionBools(1);
            }
            else if (Input.GetKeyDown(KeyCode.S) && !directionArrays[0])
            {
                current_direction = new Vector3(0, movespeed * (-1), 0);
                //this.transform.RotateAround(transform.position, Vector3.forward, -90f);
                //transform.Rotate(0, 0, -90, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, -90);
                SetDirectionBools(3);
            }
            else if (Input.GetKeyDown(KeyCode.D) && !directionArrays[1])
            {
                current_direction = new Vector3(movespeed, 0, 0);
                //gameObject.transform.RotateAround(transform.position, Vector3.forward, 90f);
                //transform.Rotate(0, 0, 0, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                SetDirectionBools(2);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !directionArrays[3])
            {
                current_direction = new Vector3(0, movespeed, 0);
                //gameObject.transform.RotateAround(transform.position, Vector3.forward, 90f);
                //transform.Rotate(0,0,90, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, 90);
                SetDirectionBools(0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && !directionArrays[2])
            {
                current_direction = new Vector3(movespeed * (-1), 0, 0);
                //gameObject.transform.RotateAround(transform.position, Vector3.forward, 180f);
                //transform.Rotate(0,0, 180, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, 180);
                SetDirectionBools(1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !directionArrays[0])
            {
                current_direction = new Vector3(0, movespeed * (-1), 0);
                //this.transform.RotateAround(transform.position, Vector3.forward, -90f);
                //transform.Rotate(0, 0, -90, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, -90);
                SetDirectionBools(3);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !directionArrays[1])
            {
                current_direction = new Vector3(movespeed, 0, 0);
                //gameObject.transform.RotateAround(transform.position, Vector3.forward, 90f);
                //transform.Rotate(0, 0, 0, Space.World);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                SetDirectionBools(2);
            }
        }
        haveTakenInput = true;
    }

    void SetDirectionBools(int s)
    {
        for(int i=0;i<4;i++)
        {
            directionArrays[i] = false; 
        }
        directionArrays[s] = true;
    }


    void validatePosition()
    {
        Camera cam = Camera.main;
        Vector2 lower_limit = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 upper_limit = cam.ViewportToWorldPoint(new Vector2(1, 1));
        if (this.transform.position.x < lower_limit.x)
            this.transform.position = new Vector3(upper_limit.x, 
                transform.position.y, transform.position.z);
        else if(this.transform.position.x> upper_limit.x)
            this.transform.position = new Vector3(lower_limit.x,
                transform.position.y, transform.position.z);
        else if (this.transform.position.y < lower_limit.y)
            this.transform.position = new Vector3(transform.position.x, upper_limit.y,transform.position.z);
        else if (this.transform.position.y > upper_limit.y)
            this.transform.position = new Vector3(transform.position.x,lower_limit.y,
                 transform.position.z);
    }

}
