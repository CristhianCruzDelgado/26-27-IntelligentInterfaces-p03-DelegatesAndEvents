using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_observer : MonoBehaviour
{
    private Transform currentTarget;
    private float speed = 2f;
    private bool shouldMove = false;

    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (shouldMove && currentTarget != null)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        Vector3 direction = currentTarget.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;        
    }

    void OnEnable()
    {
        cylinder_collision_notifier.OnCubeCollided += HandleEventResponse;
    }

    void OnDisable()
    {
        cylinder_collision_notifier.OnCubeCollided -= HandleEventResponse;
    }

    private void HandleEventResponse()
    {
        if (IsSphereRed())
        {
            GameObject greenTargetType = 
                GameObject.FindGameObjectWithTag("green-target");
            IfTargetItsNotNullSetTransformAndShouldMove(greenTargetType);
        }
        else if (IsSphereGreen() || IsSphereGreenTarget())
        {
            GameObject cylinderTarget =
                GameObject.FindGameObjectWithTag("cylinder");
            IfTargetItsNotNullSetTransformAndShouldMove(cylinderTarget);
        }
    }

    private bool IsSphereRed()
    {
        return gameObject.CompareTag("red");
    }

    private void IfTargetItsNotNullSetTransformAndShouldMove(
        GameObject target)
    {
        if (target != null)
        {
            currentTarget = target.transform;
            shouldMove = true;
        }        
    }

    private bool IsSphereGreen()
    {
        return gameObject.CompareTag("green");
    }

    private bool IsSphereGreenTarget()
    {
        return gameObject.CompareTag("red");
    }
}
