using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield_script : MonoBehaviour
{
    private Rigidbody targetShieldRigidbody;
    private Rigidbody orientationTargetRigidbody;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        cylinder_notifier.OnCubeApproachedReference += HandleProximityEvent;
    }

    private void OnDisable()
    {
        cylinder_notifier.OnCubeApproachedReference -= HandleProximityEvent;
    }

    private void HandleProximityEvent()
    {
        if (gameObject.CompareTag("humanoid-red"))
        {
            TeleportToShield();
        }
        else if (gameObject.CompareTag("humanoid-green"))
        {
            OrientTowardsTarget();
        }
    }

    private void TeleportToShield()
    {
        GameObject targetShield = 
            GameObject.FindGameObjectWithTag("shield-target");
        targetShieldRigidbody = targetShield.GetComponent<Rigidbody>();
        if (targetShieldRigidbody != null)
        {
            rigidbody.position = targetShieldRigidbody.position;
            Debug.Log(gameObject.name + " teleported to target shield.");
        }
    }

    private void OrientTowardsTarget()
    {
        GameObject orientationTarget = 
            GameObject.FindGameObjectWithTag("shield-target");
        orientationTargetRigidbody = orientationTarget.GetComponent<Rigidbody>();
        if (orientationTargetRigidbody != null)
        {
            Vector3 direction = orientationTargetRigidbody.position - 
                                rigidbody.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rigidbody.MoveRotation(targetRotation);
                Debug.Log(gameObject.name + " oriented towards target object.");
            }
        }
    }
}
