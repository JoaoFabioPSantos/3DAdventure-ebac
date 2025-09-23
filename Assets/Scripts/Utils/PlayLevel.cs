using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;
    public SaveSetup setup;
    public int firstLevel;

    public void NewGame()
    {
        SaveManager.Instance.ResetSave();
        //SaveManager.Instance.Save();
        Invoke(nameof(LoadGame), 0.5f);
    }

    public void ContinueGame()
    {
        SaveManager.Instance.Load();
        Invoke(nameof(LoadGame), 0.5f);
        
    }

    public void BackMenu()
    {
        Time.timeScale = 1f;
        SaveManager.Instance.SavePlayerConfig();
        Invoke(nameof(LoadMenu), 0.5f);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

}
