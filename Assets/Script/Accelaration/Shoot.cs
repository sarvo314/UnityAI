using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject shellSpawnPos;
    public GameObject target;
    public GameObject parent;

    float turnSpeed = 5f;

    [SerializeField]
    [Range(5f, 50f)]
    float speed = 5f;

    void Fire()
    {
        GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        Vector3 direction = (target.transform.position - parent.transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0, 90, 0));
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * speed);

        //parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        float? angle = RotateTurret();
    }
    float? RotateTurret()
    {
        float? angle = CalculateAngle(true);

        if (angle != null)
        {
            Debug.Log("Working");
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, transform.localEulerAngles.y, 0);
        }
        return angle;
    }

    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = speed * speed;

        float underTheSquareRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSquareRoot >= 0)
        {
            float root = Mathf.Sqrt(underTheSquareRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            Debug.Log("Working");
            if (low)
                return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg;
            else
                return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;

        }
        else
            return null;
    }

}

