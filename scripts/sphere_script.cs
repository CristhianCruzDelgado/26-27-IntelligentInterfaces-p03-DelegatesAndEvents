using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_script : MonoBehaviour
{
    private float speed = 2f;
    private bool shouldMove = false;
    private Transform currentTarget;

    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (shouldMove)
        {
            Vector3 direction = currentTarget.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        cylinder_script.OnCubeCollided += HandleEventResponse;
    }

    private void OnDisable()
    {
        cylinder_script.OnCubeCollided -= HandleEventResponse;
    }

    private void HandleEventResponse()
    {
        if (gameObject.CompareTag("red"))
        {
            GameObject greenTargetType = 
                GameObject.FindGameObjectWithTag("green-target");
            if (greenTargetType != null)
            {
                currentTarget = greenTargetType.transform;
                shouldMove = true;
                Debug.Log(gameObject.name + " moving towards green target.");
            }
        }
        else if (gameObject.CompareTag("green") || 
                 gameObject.CompareTag("green-target"))
        {
            GameObject cylinderTarget =
                GameObject.FindGameObjectWithTag("cylinder");
            if (cylinderTarget != null)
            {
                currentTarget = cylinderTarget.transform;
                shouldMove = true;
                Debug.Log(gameObject.name + " moving towards cylinder.");
            }
        }
    }
}
