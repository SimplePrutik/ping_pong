using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;

    public static MenuManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public int IndexBallColor { get; set; }

    Material[] ballColors;

    void Start()
    {
        if (PlayerPrefs.HasKey("BallColor"))
            IndexBallColor = PlayerPrefs.GetInt("BallColor");
        else
        {
            PlayerPrefs.SetInt("BallColor", 0);
            IndexBallColor = 0;
        }
        LoadMaterials();
    }

    void LoadMaterials()
    {
        ballColors = Resources.LoadAll<Material>("Materials/Ball");
    }

    public Material GetMaterial(int index)
    {
        return ballColors[index];
    }

    public Material[] GetMaterials() => ballColors;
}
