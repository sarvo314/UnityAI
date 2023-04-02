using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Vector3 startPosition, startRotation;

    [SerializeField]
    Transform point;
    [SerializeField]
    float normalizeValueHitDist = 20f;


    [Range(-1f, 1f)]
    public float accelaration, turn;

    public float timeSinceStart = 0f;

    [Header("Fitness")]
    public float overallFitness;
    public float distanceMultiplier = 1.4f;
    public float avgSpeedMultiplier = 0.2f;
    public float sensorMultiplier = 0.1f;

    private Vector3 lastPosition;
    private float totalDistanceTravelled;
    private float avgSpeed;

    private float aSensor, bSensor, cSensor;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
    }
    public void Reset()
    {
        timeSinceStart = 0f;
        totalDistanceTravelled = 0f;
        avgSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }
    private void FixedUpdate()
    {
        InputSensors();
        lastPosition = transform.position;

        MoveCar(accelaration, turn);

        timeSinceStart += Time.fixedDeltaTime;

        CalculateFitness();
    }
    void Update()
    {
        var dir = transform.TransformDirection(point.position);
        Debug.DrawRay(Vector3.zero, dir * 100);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bounds")
            Reset();
        Debug.Log("We collided");
    }

    private void CalculateFitness()
    {
        totalDistanceTravelled += Vector3.Distance(transform.position, lastPosition);
        avgSpeed = totalDistanceTravelled / timeSinceStart;

        overallFitness = (totalDistanceTravelled * distanceMultiplier) +
                             (avgSpeed * avgSpeedMultiplier) + (((aSensor + bSensor + cSensor) / 3) * sensorMultiplier);

        if (timeSinceStart > 20 && overallFitness < 40)
        {
            Reset();
        }
        if (overallFitness >= 1000)
        {
            Reset();
        }

    }

    private void InputSensors()
    {
        Vector3 leftDir = (transform.forward + transform.right);
        Vector3 fwdDir = (transform.forward);
        Vector3 rightDir = (transform.forward - transform.right);

        Ray r = new Ray(transform.position, leftDir);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        {
            aSensor = hit.distance / normalizeValueHitDist;
            Debug.Log("A sensor " + aSensor);
        }

        r.direction = fwdDir;
        if (Physics.Raycast(r, out hit))
        {
            bSensor = hit.distance / normalizeValueHitDist;
            Debug.Log("b sensor " + bSensor);

        }
        r.direction = rightDir;
        if (Physics.Raycast(r, out hit))
        {
            cSensor = hit.distance / normalizeValueHitDist;
            Debug.Log("C sensor " + cSensor);
        }


    }

    private Vector3 inp;

    public void MoveCar(float v, float h)
    {
        inp = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, v * 11.4f), Time.deltaTime);
        //transforms according to localscale
        //for example if (0,0,1) is forward so transformdirection
        //will rotate it in forward direction according to local scale
        inp = transform.TransformDirection(inp);

        transform.position += inp;

        transform.eulerAngles += new Vector3(0, (h * 90) * Time.fixedDeltaTime, 0);
    }
}
