using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoid_observer_2 : MonoBehaviour
{
    private Rigidbody targetShieldRigidbody;
    private Rigidbody orientationTargetRigidbody;
    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
            TeleportToShield();
        }
        else if (IsGreenHumanoid())
        {
            OrientTowardsTarget();
        }
    }

    private bool IsRedHumanoid()
    {
        return gameObject.CompareTag("red-humanoid");
    }

    private bool IsGreenHumanoid()
    {
        return gameObject.CompareTag("green-humanoid");
    }

    private void TeleportToShield()
    {
        GameObject targetShield = 
            GameObject.FindGameObjectWithTag("target-shield");
        if (targetShield != null)
        {
            targetShieldRigidbody = targetShield.GetComponent<Rigidbody>();
            if (targetShieldRigidbody != null)
            {
                rigidbody.position = targetShieldRigidbody.position;
            }
        }
    }

    private void OrientTowardsTarget()
    {
        GameObject orientationTarget = 
            GameObject.FindGameObjectWithTag("target-shield");
        if (orientationTarget != null)
        {
            orientationTargetRigidbody = 
                orientationTarget.GetComponent<Rigidbody>();
            if (orientationTargetRigidbody != null)
            {
                Vector3 direction = orientationTargetRigidbody.position - 
                                    rigidbody.position;
                direction.y = 0f;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = 
                        Quaternion.LookRotation(direction);
                    rigidbody.MoveRotation(targetRotation);
                }
            }
        }
    }
}
