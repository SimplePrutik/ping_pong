using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{

    private Material [] colors = new Material[] { };

    private List<ColorSelection> colorButtons = new List<ColorSelection>();

    public ColorSelection proto;

    private void Start()
    {
        ColorSelection.OnColorChanged += ChangeColor;
        StartCoroutine(LoadBallColors());
    }

    IEnumerator LoadBallColors()
    {
        while (colors == null || colors.Length == 0)
        {
            colors = MenuManager.Instance.GetMaterials();
            yield return null;
        }
        for (int i = 0; i < colors.Length; ++i)
        {
            var colorSelection = Instantiate(proto, transform);
            colorButtons.Add(colorSelection);
            colorSelection.SetIndex(i);
            colorSelection.GetComponent<Image>().material = MenuManager.Instance.GetMaterial(i);
        }
        ChangeColor(PlayerPrefs.HasKey("BallColor") ? PlayerPrefs.GetInt("BallColor") : 0);
    }

    void ChangeColor(int color)
    {
        MenuManager.Instance.IndexBallColor = color;
        PlayerPrefs.SetInt("BallColor", color);
        for (int i = 0; i < colorButtons.Count; ++i)
        {
            colorButtons[i].SetSelection(i == color);
        }
        GameSettings.ballMaterial = MenuManager.Instance.GetMaterial(color);
    }

    private void OnDestroy()
    {
        ColorSelection.OnColorChanged -= ChangeColor;
    }
}
