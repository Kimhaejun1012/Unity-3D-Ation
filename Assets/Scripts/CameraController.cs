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
    bool smoothnessFinish = false;

    private Camera cam;

    Coroutine zoomIn;
    Coroutine zoomOut;

    private float zoomSpeed = 10;
    private float maxZoom = 30;
    private float defaultZoom;

    private void OnEnable()
    {
        TargetingSystem.OnTargeting += HandleTargetChange;
    }

    private void OnDisable()
    {
        TargetingSystem.OnTargeting -= HandleTargetChange;
    }
    private void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        defaultZoom = cam.fieldOfView;
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
        if(target != null)
        {
            StartCoroutine(SmoothRotate());
        }
    }
    void TargetingOn()
    {
        Vector3 adjustedPosition = new Vector3(lookTarget.position.x, lookTarget.position.y + 1, lookTarget.position.z);

        currentRotate = Quaternion.LookRotation(adjustedPosition - transform.position);
        transform.position = followObj.position;
        //transform.rotation = Quaternion.Slerp(transform.rotation, currentRotate, smoothness * Time.deltaTime);
        if(smoothnessFinish)
        transform.LookAt(adjustedPosition);

        Vector3 angles = currentRotate.eulerAngles;
        rotX = angles.x;
        rotY = angles.y;
    }
    void TargetingOff()
    {
        rotX -= Input.GetAxis("Mouse Y") * sensitivity / Time.timeScale * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity / Time.timeScale * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        currentRotate = Quaternion.Euler(rotX, rotY, 0f);

        transform.position = followObj.position;
        transform.rotation = currentRotate;
    }
    public void StartZoom()
    {
        if (zoomOut != null)
        {
            StopCoroutine(zoomOut);
            zoomOut = null;
        }
        zoomIn = StartCoroutine(ZoomIn());
    }
    public void FinishZoom()
    {
        if (zoomIn != null)
        {
            StopCoroutine(zoomIn);
            zoomIn = null;
        }
        zoomOut = StartCoroutine(ZoomOut());
    }
    private IEnumerator ZoomIn()
    {
        while (cam.fieldOfView >= maxZoom)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxZoom, Time.deltaTime * zoomSpeed);
            yield return null;
        }
    }
    private IEnumerator ZoomOut()
    {
        while (cam.fieldOfView < defaultZoom)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultZoom, Time.deltaTime * zoomSpeed);
            yield return null;
        }
    }
    private IEnumerator SmoothRotate()
    {
        smoothnessFinish = false;
        while (true)
        {
            Vector3 adjustedPosition = new Vector3(lookTarget.position.x, lookTarget.position.y + 1, lookTarget.position.z);
            Quaternion targetRotate = Quaternion.LookRotation(adjustedPosition - transform.position);

            transform.position = followObj.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotate, smoothness * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotate) < 1f)
            {
                smoothnessFinish = true;
                break;
            }

            yield return null;
        }
    }
}
