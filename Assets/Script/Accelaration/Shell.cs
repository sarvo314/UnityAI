using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    float mass = 10f;
    float force = 1f;
    float accelaration;
    float gravity = -9.8f;
    float gAccel;
    float speedZ;
    float speedY;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        //accelaration = force / mass;
        //speedZ += accelaration * Time.deltaTime;

        //transform.Translate(0, 0, accelaration);
    }
}
