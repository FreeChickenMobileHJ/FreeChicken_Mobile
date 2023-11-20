using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMoveObj : MonoBehaviour
{
    public enum MoveObstacleType { A, B,C,D};
    public MoveObstacleType Type;
    CityScenePlayer player;
    
    float initPositionX;
    public float distance;
    public float turningPoint;

    public bool turnSwitch;
    public float moveSpeed;
    
    public float angle = 0;
    private float lerpTime = 0;
    public float swingSpeed;

  
    public bool isMove;
   

    public float rotateSpeed;
   
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CityScenePlayer>();
        if (Type == MoveObstacleType.B) 
        {
            initPositionX = transform.position.x;
            turningPoint = initPositionX - distance;

        }
       
    }
    private void FixedUpdate()
    {
        switch (Type)
        {
            case MoveObstacleType.A:
                isMove = true;

                rotate();

                break;
            case MoveObstacleType.B:
                isMove = true;

                leftRight();

                break;
            case MoveObstacleType.C:
                if (player.ishurdleUp == true)
                {
                    hurdleUp();
                }
                else if (player.ishurdleUp == false)
                {
                    hurdleDown();
                }
                break;
            case MoveObstacleType.D:
                Swing();
                break;

        }
    }

    void Swing()
    {
        
        lerpTime += Time.deltaTime * swingSpeed;
        transform.rotation = CalculateMovementOfPendulum();


    }
    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * .5f;
    }
    public void hurdleUp()
    {
        transform.position = new Vector3(this.transform.position.x, 2, this.transform.position.z);
        
    }
    public void hurdleDown()
    {
        
        transform.position = new Vector3(this.transform.position.x, 0.2f, this.transform.position.z);

    }
    void rotate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        

    }
    void leftRight()
    {

        float currentPositionX = transform.position.x;

        if (currentPositionX >= initPositionX + distance)
        {
            turnSwitch = false;
        }
        else if (currentPositionX <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            
        }
        else
        {
            transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            
        }

    }
   
 
}
