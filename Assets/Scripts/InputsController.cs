using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputsController : MonoBehaviour
{
    private void Start()
    {
        GetStarted();
    }

    private void GetStarted()
    {
        var controllers = GameObject.FindObjectsOfType<PlayerInput>();

        foreach (var item in controllers)
        {
            item.defaultActionMap = "Tank";
        }
    }
}
