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

    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }

    public void OnLoad(SaveSetup setup)
    {
        if(uiTextName!=null)uiTextName.text = "Start Level: " + (setup.checkPoint );
    }

    public void NewGame()
    {
        SaveManager.Instance.CreateNewSave();
        SaveManager.Instance.Save();
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
        SaveManager.Instance.Save();
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

    public void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
