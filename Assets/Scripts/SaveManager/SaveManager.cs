using System.Collections;
using System.Collections.Generic;
//biblioteca necessária para salvar
using System.IO;
using UnityEngine;
using Studio.Core.Singleton;
using Items;
using System;
using Cloth;

//da pra trocar para um .bat para alterar e deixar com maior "mascaramento", ninguem ver.
public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]private SaveSetup _saveSetup;
    /*dataPath -> fica dentro dos assets
        *persistentDataPath -> dentro de um servidor no computador (mais utilizado,salva no usuário corretamente)
        *streamingAssetsPath -> se você colocar uma pasta com nome streamingAssets (parecido com persistent, mas ele faz com que seja salvo na pasta streaming.
    */
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded;
    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        //não vai ser destruído quando ser criado o jogo.
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Player";
        _saveSetup.coins = 0;
        _saveSetup.medPacks = 0;
        _saveSetup.health = 30f;

        _saveSetup.checkPoint = 0;
        _saveSetup.clothSetup = ClothManager.Instance.ResetSetup();
    }

    #region SAVE
    [NaughtyAttributes.Button]
    public void Save()
    {
        SaveItems();
        SavePlayerConfig();
        //esse true é para quebrar as linhas
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);

        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = ItemManager.Instance.GetItemByType(ItemType.COIN).soInt.value;
        _saveSetup.medPacks = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt.value;
    }

    public void SavePlayerConfig()
    {
        if(Player.Instance != null)_saveSetup.health = Player.Instance.GetLife();
        _saveSetup.clothSetup = ClothManager.Instance.GetCurrentSetup();
        _saveSetup.checkPoint = CheckpointManager.Instance.GetLastCheckPoint();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
    }
    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
        }
        else
        {
            //Reset
            CreateNewSave();
            Save();
        }

        //FileLoaded.Invoke(_saveSetup);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public int medPacks;

    public float health;
    public int checkPoint;

    public ClothSetup clothSetup;
}