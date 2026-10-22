using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoid_notifier : MonoBehaviour
{
    public delegate void CollisionAction();
    public static event CollisionAction OnCubeCollidedHumanoidRed;
    public static event CollisionAction OnCubeCollidedHumanoidGreen;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cube"))
        {
            if (IsRedHumanoid())
            {
                OnCubeCollidedHumanoidRed?.Invoke();
            }
            else if (IsGreenHumanoid())
            {
                OnCubeCollidedHumanoidGreen?.Invoke();
            }
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
}
