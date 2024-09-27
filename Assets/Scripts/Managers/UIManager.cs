using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public GameObject crossHair;

    public List<StringBuilder> coolDowns = new List<StringBuilder>();
    public string skillCoolTime;
    public string meleeCoolTime;

    public List<GameObject> hearts = new List<GameObject>();
    public GameObject heartPrefab;
    public Transform heartContainer;

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

        GUILayout.Label(skillCoolTime, style);
        GUILayout.Label(meleeCoolTime, style);

        foreach (var cool in coolDowns)
        {
            GUILayout.Label(cool.ToString(), style);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    public void HeartInit(int hp)
    {
        for (int i = 0; i < hp; i++)
        {
            var heart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(heart);
        }
    }
    public void UpdateHearts(int hp)
    {
        int x = hearts.Count - hp;
        for (int i = 1; i <= x; ++i)
        {
            GameObject heart = hearts[hearts.Count - i];
            if (heart.transform.childCount > 0)
            {
                Transform filledHeart = heart.transform.GetChild(1);
                if (filledHeart != null)
                {
                    filledHeart.gameObject.SetActive(false);
                }
            }
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = !EditorApplication.isPlaying;
#endif
    }
}