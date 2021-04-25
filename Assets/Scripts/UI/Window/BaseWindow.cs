using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindow : MonoBehaviour, IWindow
{
    protected bool isActive;
    [HideInInspector] public bool isBound = false;
    protected CanvasGroup canvas => GetComponent<CanvasGroup>();
    public string currentWindowName;

    void Start()
    {
        WindowsManager.OnWindowOpened += Open;
        WindowsManager.OnWindowClosed += Close;
        isBound = true;
    }
    public void Close(string windowName)
    {
        if (windowName == currentWindowName)
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            isActive = false;
        }
    }

    public void Open(string windowName)
    {
        if (windowName == currentWindowName)
        {
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            StartCoroutine(ActivityDelay());
        }
    }

    IEnumerator ActivityDelay()
    {
        yield return new WaitForSeconds(0.15f);
        isActive = true;
    }

    void OnDestroy()
    {
        WindowsManager.OnWindowOpened -= Open;
        WindowsManager.OnWindowClosed -= Close;
    }
}
