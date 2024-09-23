using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followObj;
    public Transform target;

    Transform lookTarget;
    Quaternion currentRotate;


    public float followSpeed = 10f;
    public float sensitivity = 300f;
    public float clampAngle = 40f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;

    public float smoothness = 10f;
    private void OnEnable()
    {
        TargetingSystem.OnTargeting += HandleTargetChange;
    }

    private void OnDisable()
    {
        TargetingSystem.OnTargeting -= HandleTargetChange;
    }
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
    }
    void LateUpdate()
    {
        if(lookTarget == null)
        {
            TargetingOff();
        }
        else
        {
            TargetingOn();
        }
    }
    void HandleTargetChange(Transform target)
    {
        lookTarget = target;
    }
    void TargetingOn()
    {
        Vector3 adjustedPosition = new Vector3(lookTarget.position.x, lookTarget.position.y + 1, lookTarget.position.z);

        currentRotate = Quaternion.LookRotation(adjustedPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentRotate, smoothness * Time.deltaTime);
        transform.position = followObj.position;

        Vector3 angles = currentRotate.eulerAngles;
        rotX = angles.x;
        rotY = angles.y;
    }
    void TargetingOff()
    {
        rotX -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        currentRotate = Quaternion.Euler(rotX, rotY, 0f);

        transform.rotation = currentRotate;
        transform.position = followObj.position;
    }
}
