using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 10.0f;
    public float rotSpeed = 0.00001f;
    // Start is called before the first frame update

    GameObject tracker;
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
    }
    void ProgressTracker()
    {
        NextWaypoint();

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, 10f);
    }

    private void NextWaypoint()
    {
        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
            currentWP++;

        if (currentWP >= waypoints.Length)
            currentWP = 0;
    }


    // Update is called once per frame
    void Update()
    {
        ProgressTracker();

        Quaternion lookWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookWP, rotSpeed * Time.deltaTime);
        //this.transform.LookAt(waypoints[currentWP].transform);
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
