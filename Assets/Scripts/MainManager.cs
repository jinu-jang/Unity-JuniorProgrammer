using System;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color teamColor;

    private static readonly string SAVEFILE_NAME = "savefile.json";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [Serializable]
    public class SaveData
    {
        public Color teamColor;

        public SaveData(Color teamColor)
        {
            this.teamColor = teamColor;
        }
    }

    public void SaveColor()
    {
        var saveData = new SaveData(teamColor);

        var jsonStr = JsonUtility.ToJson(saveData);

        // Save file to disk.
        var outPath = Path.Combine(Application.persistentDataPath, SAVEFILE_NAME);
        File.WriteAllText(outPath, jsonStr);
        Debug.Log(outPath);
    }

    public void LoadColor()
    {
        var savePath = Path.Combine(Application.persistentDataPath, SAVEFILE_NAME);

        if (File.Exists(savePath))
        {
            var json = File.ReadAllText(savePath);
            var data = JsonUtility.FromJson<SaveData>(json);

            Load(data);
        }
    }

    /// <summary>
    /// Load the data in from given SaveData class.
    /// </summary>
    /// <param name="data">Data container to read configuration from.</param>
    private void Load(SaveData data)
    {
        teamColor = data.teamColor;
    }
}
