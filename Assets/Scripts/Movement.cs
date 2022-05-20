using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movespeed = 1f;
    
    bool[] directionArrays= new bool[4] ;
    //0- up, 1- left, 2- right, 3- down
    Vector3 current_direction = new Vector3(0, 0, 0);


    private void Update()
    {

        transform.position += current_direction * Time.deltaTime;
        GetDirection();       
        
    }


    void GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) && !directionArrays[3])
        {
            current_direction = new Vector3(0, movespeed, 0);
            SetDirectionBools(0);
        }
        else if (Input.GetKeyDown(KeyCode.A) && !directionArrays[2])
        {
            current_direction = new Vector3(movespeed * (-1), 0, 0);
            SetDirectionBools(1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && !directionArrays[0])
        {
            current_direction = new Vector3(0, movespeed * (-1), 0);
            SetDirectionBools(3);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !directionArrays[1])
        {
            current_direction = new Vector3(movespeed, 0, 0);
            SetDirectionBools(2);
        }
    }

    void SetDirectionBools(int s)
    {
        for(int i=0;i<4;i++)
        {
            directionArrays[i] = false; 
        }
        directionArrays[s] = true;
    }

}
