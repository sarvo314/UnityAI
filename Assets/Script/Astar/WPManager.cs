using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Link
{
    public enum direction { UNI, BI };
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{

    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
