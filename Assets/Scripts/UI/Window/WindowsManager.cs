using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    private List<BaseWindow> windows;

    public static Action<string> OnWindowOpened = delegate { };
    public static Action<string> OnWindowClosed = delegate { };

    private static WindowsManager _instance;
    public static WindowsManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(LoadWindows());
    }

    IEnumerator LoadWindows()
    {
        windows = new List<BaseWindow>();
        foreach (var window in GetComponentsInChildren<BaseWindow>())
        {
            windows.Add(window);
        }

        while (windows.Any(x => !x.isBound))
        {
            yield return null;
        }
        OpenWindow("MainMenu");
    }

    public void OpenWindow(string windowName)
    {
        foreach (var window in windows)
            CloseWindow(window.currentWindowName);
        OnWindowOpened(windowName);
    }

    public void CloseWindow(string windowName)
    {
        OnWindowClosed(windowName);
    }



}
