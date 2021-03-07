using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTankRotate : MonoBehaviour
{
    float rotateY;
    private void Start()
    {
        rotateY = -150f;
    }
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, rotateY, 0);
        rotateY += Time.deltaTime * 10;
    }
}
