using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAIScript : MonoBehaviour
{
    GameObject playerT;
    Transform targetT;

    private CarAIControllerScript carC;
    private Vector3 targetPosition;
    private float reachedTargetDistance;
    private float stoppingDistance;
    private float stoppingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
        carC = GetComponent<CarAIControllerScript>();
        reachedTargetDistance = 12f;
        stoppingDistance = 60f;
        stoppingSpeed = 20f;
    }

    // Update is called once per frame
    private void Update()
    {
        // agafar target
        playerT = GameObject.FindWithTag("Player");
        targetT = playerT.transform;

        SetTargetPosition(targetT.position);

        float VerticalAxis = 0f;
        float HorizontalAxis = 0f;

        
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget > reachedTargetDistance)// Si la distancia es mes gran que la distancia objectiu. Cotxe va cap a objectiu
        {
         
            Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
            VerticalAxis = 1f;
            if (distanceToTarget < stoppingDistance && carC.GetSpeed() > stoppingSpeed)// si la distancia a l'objectiu es mes alta que la distancia de frenada
            {
                VerticalAxis = -1f;
            }

            
            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            if (angleToDir > 25 && angleToDir < 40)
            {
                HorizontalAxis = 0.1f;
            }
            else if (angleToDir < -25 && angleToDir > -40)
            {
                HorizontalAxis = -0.1f;
            }
            else if (angleToDir >= 40)
            {
                HorizontalAxis = 0.7f;
            }
            else if (angleToDir <= -40)
            {
                HorizontalAxis = -0.7f;
            }
            else
            {
                HorizontalAxis = 0f;
            }
        }
        else// Si la distancia es mes petita que la distancia objectiu frena
        {
            
            if (carC.GetSpeed() > 15f)
            {
                VerticalAxis = -1f;
            }
            else
            {
                VerticalAxis = 0f;
            }
            HorizontalAxis = 0f;
        }

        carC.SetInputs(VerticalAxis, HorizontalAxis);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }


}
