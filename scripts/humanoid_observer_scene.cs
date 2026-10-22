using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoid_observer_scene : MonoBehaviour
{
    private float speed = 2f;
    private float epsilon = 1f;
    private bool shouldMove = false;
    private Transform currentTarget;
    private MeshRenderer meshRenderer;

    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (shouldMove && currentTarget != null)
        {
            Vector3 direction = currentTarget.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, currentTarget.position) < epsilon)
            {
                shouldMove = false;
            }
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
        if (gameObject.CompareTag("humanoid-green"))
        {
            GameObject greenShield = GameObject.FindGameObjectWithTag("shield-green");
            if (greenShield != null)
            {
                currentTarget = greenShield.transform;
                shouldMove = true;
                Debug.Log(gameObject.name + " moving towards green shield.");
            }
        }
        else if (gameObject.CompareTag("humanoid-red"))
        {
            GameObject targetShield = GameObject.FindGameObjectWithTag("shield-green-target");
            if (targetShield != null)
            {
                currentTarget = targetShield.transform;
                shouldMove = true;
                Debug.Log(gameObject.name + " moving towards green target shield.");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("humanoid-red") ||
            collision.gameObject.CompareTag("humanoid-green"))
        {
            ChangeColor(Color.blue);
            Debug.Log(gameObject.name + " collided with shield and changed color.");
        }
    }

    private void ChangeColor(Color newColor)
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = newColor;
        }
        else
        {
            Renderer childRenderer = GetComponentInChildren<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.material.color = newColor;
            }
        }
    }
}