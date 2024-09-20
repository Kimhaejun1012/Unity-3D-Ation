using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public GameObject crossHair;


    public List<StringBuilder> coolDowns = new List<StringBuilder>();


    private void Awake()
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
    public void SetCrossHair()
    {
        crossHair.SetActive(!crossHair.activeSelf);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        GUILayout.BeginArea(new Rect(Screen.width - 250, 30, 240, 300));
        GUILayout.BeginVertical();

        foreach (var cool in coolDowns)
        {
            GUILayout.Label(cool.ToString(), style);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}