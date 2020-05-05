using System.Collections.Generic;
using UnityEngine;
using Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameService : MonoSingletonGeneric<GameService>
{

    public List<GameObject> LevelArts;
    GameData gameData = new GameData();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        gameData = LoadData();
        Debug.Log("gameData BulletsFired " + gameData.BulletsFired);
    }


    public void DestroyeAllGameArts()
    {
        for (int i = 0; i < LevelArts.Count; i++)
        {
            LevelArts[i].SetActive(false);
        }
    }


    public void SaveData(GameData gameData)
    {
        CheckIfGameDataNeedstoSave(gameData);
        string path = Application.streamingAssetsPath + Constants.GameDataFileName;
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, this.gameData);
        stream.Close();
    }


    public GameData LoadData()
    {
        string path = Application.streamingAssetsPath + Constants.GameDataFileName;

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData gameData = formatter.Deserialize(stream) as GameData;
            this.gameData = gameData;
            stream.Close();
        }
        else
        {
            Debug.Log("File not found in " + path);
        }
        return gameData;
    }


    private void CheckIfGameDataNeedstoSave(GameData gameData)
    {
        if (gameData.BulletsFired > this.gameData.BulletsFired)
            this.gameData.BulletsFired = gameData.BulletsFired;

        if (gameData.EnemiesHit > this.gameData.EnemiesHit)
            this.gameData.EnemiesHit = gameData.EnemiesHit;

        if (gameData.EnemiesKilled > this.gameData.EnemiesKilled)
            this.gameData.EnemiesKilled = gameData.EnemiesKilled;
    }

}
