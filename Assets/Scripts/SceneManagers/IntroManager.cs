using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public RectTransform logo;
    private Vector2 logoSize;

    private float animTime = 3f;

    IEnumerator PlayLogo()
    {
        var animCurve = new AnimationCurve(new Keyframe[]
        {
            new Keyframe(0f, 1f),
            new Keyframe(animTime / 2f, 0f),
            new Keyframe(animTime, 1f)
        });
        float time = 0f;
        while (time < animTime)
        {
            time += Time.deltaTime;
            var sizeX = animCurve.Evaluate(time) * logoSize.x;
            var sizeY = animCurve.Evaluate(time) * logoSize.y;
            logo.sizeDelta = new Vector2(sizeX, sizeY);
            yield return null;
        }
        logo.sizeDelta = new Vector2(logoSize.x, logoSize.y);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        logoSize = logo.sizeDelta;
        StartCoroutine(PlayLogo());
    }
}
