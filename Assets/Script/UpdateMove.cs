using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMove : MonoBehaviour
{

    void Update()
    {
        //this.transform.position = new Vector3()
        transform.Translate(0, 0, Time.deltaTime);
    }
}
