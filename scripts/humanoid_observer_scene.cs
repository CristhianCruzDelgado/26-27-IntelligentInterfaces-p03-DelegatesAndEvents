using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoid_observer_scene : MonoBehaviour
{
    private Transform currentTarget;
    private float speed = 1f;
    private float epsilon = 1f;
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
            StopIfHumanoidsReachedTarget();
        }
    }

    private void UpdatePosition()
    {
        Vector3 direction = currentTarget.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void StopIfHumanoidsReachedTarget()
    {
        if (Vector3.Distance(transform.position, currentTarget.position) < 
            epsilon)
        {
            shouldMove = false;
        }
    }

    void OnEnable()
    {
        cylinder_proximity_notifier.OnCubeApproachedReference += 
            HandleProximityEvent;
    }

    void OnDisable()
    {
        cylinder_proximity_notifier.OnCubeApproachedReference -= 
            HandleProximityEvent;
    }

    private void HandleProximityEvent()
    {
        if (IsRedHumanoid())
        {
            GameObject greenShield = 
                GameObject.FindGameObjectWithTag("green-shield");
            IfTargetItsNotNullSetTransformAndShouldMove(greenShield);
        }
        else if (IsGreenHumanoid())
        {
            GameObject targetShield = 
                GameObject.FindGameObjectWithTag("target-green-shield");
            IfTargetItsNotNullSetTransformAndShouldMove(targetShield);
        }
    }

    private bool IsRedHumanoid()
    {
        return gameObject.CompareTag("red-humanoid");
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

    private bool IsGreenHumanoid()
    {
        return gameObject.CompareTag("green-humanoid");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsRedHumanoid() || IsGreenHumanoid())
        {
            ChangeColor(Color.blue);
        }
    }

    private void ChangeColor(Color newColor)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.color = newColor;
        }
    }
}
