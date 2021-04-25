using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow : BaseWindow
{
    public void PlayBtn()
    {
        SceneManager.LoadScene("Game");
    }

    public void SettingsBtn()
    {
        WindowsManager.Instance.OpenWindow("Settings");
    }
    
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && isActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
