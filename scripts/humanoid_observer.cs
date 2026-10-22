using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoid_observer : MonoBehaviour
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

            if (Vector3.Distance(transform.position, currentTarget.position) < 
                epsilon)
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
        humanoid_notifier.OnCubeCollidedHumanoidRed += 
            HandleCubeHitHumanoidRed;
        humanoid_notifier.OnCubeCollidedHumanoidGreen += 
            HandleCubeHitHumanoidGreen;
    }

    private void OnDisable()
    {
        humanoid_notifier.OnCubeCollidedHumanoidRed -= 
            HandleCubeHitHumanoidRed;
        humanoid_notifier.OnCubeCollidedHumanoidGreen -= 
            HandleCubeHitHumanoidGreen;
    }

    private void HandleCubeHitHumanoidRed()
    {
        if (gameObject.CompareTag("humanoid-red"))
        {
            GameObject greenShield = 
                GameObject.FindGameObjectWithTag("shield-green");
            if (greenShield != null)
            {
                currentTarget = greenShield.transform;
                shouldMove = true;
                Debug.Log(gameObject.name + " moving towards green shield.");
            }
        }
    }

    private void HandleCubeHitHumanoidGreen()
    {
        if (gameObject.CompareTag("humanoid-green"))
        {
            GameObject selectedShield =
                GameObject.FindGameObjectWithTag("shield-green-target");
            if (selectedShield != null)
            {
                currentTarget = selectedShield.transform;
                shouldMove = true;
                Debug.Log(gameObject.name + " moving towards selected shield.");
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