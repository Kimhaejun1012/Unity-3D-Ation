using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Transform attackMonster;

    public UnityEvent camZoom;
    public UnityEvent zoomFinish;

    public GameObject crossHair;
    public Transform heartContainer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        UIManager.instance.crossHair = crossHair;
        UIManager.instance.heartContainer = heartContainer;
    }
    public void CamZoomStart()
    {
        camZoom.Invoke();
    }
    public void CamZoomFinish()
    {
        zoomFinish.Invoke();
    }
}
