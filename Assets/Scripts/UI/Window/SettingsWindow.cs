using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindow : BaseWindow
{
    public void CloseSettings()
    {
        WindowsManager.Instance.OpenWindow("MainMenu");
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseSettings();
            }
        }
    }
}
