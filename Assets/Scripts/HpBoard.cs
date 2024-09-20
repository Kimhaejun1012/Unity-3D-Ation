using UnityEngine;

public class HpBoard : MonoBehaviour
{
    Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        transform.LookAt(cam);
    }
}
