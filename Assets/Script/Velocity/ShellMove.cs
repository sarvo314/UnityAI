using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour
{
    float speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, speed * Time.deltaTime);
    }
}
