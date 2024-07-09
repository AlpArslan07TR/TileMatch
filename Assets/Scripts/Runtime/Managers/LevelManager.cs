using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TextAsset[] levelFiles;
    LevelData[] _levelData;
    LevelSaveData _levelSaveData;


    void Awake()
    {
        ReadLevels();
        Load();

        LevelEvents.OnLevelWin += Save_Callback;
        LevelEvents.OnLevelDataNeeded += LevelDataNeeded_Callback;
        
    }

    void OnDestroy()
    {
        LevelEvents.OnLevelWin -= Save_Callback;
        LevelEvents.OnLevelDataNeeded -= LevelDataNeeded_Callback;
    }

    void ReadLevels()
    {
        _levelData = new LevelData[levelFiles.Length];

        for (int i=0; i < levelFiles.Length; i++)
        {
            _levelData[i] = JsonUtility.FromJson<LevelData>(levelFiles[i].text);
        }
    }

    void Load()
    {
        if (DataHandler.HasData(DataKeys.LevelScoreDataKey))
        {
            _levelSaveData = DataHandler.Load<LevelSaveData>(DataKeys.LevelScoreDataKey);
        }
        else
        {
            _levelSaveData = new LevelSaveData(new LevelScoreData[_levelData.Length]);

            for(int i=0; i < _levelData.Length; i++)
            {
                _levelSaveData.Data[i].index = i;
                _levelSaveData.Data[i].title = _levelData[i].title;
            }

            _levelSaveData.Data[0].isUnlocked = true;
            _levelSaveData.Data[0].highScore = 0;

            DataHandler.Save(_levelSaveData, DataKeys.LevelScoreDataKey);

        }
    }

    void Save_Callback(CompleteData completeData)
    {
        _levelSaveData.Data[completeData.Index + 1].isUnlocked = true;
        _levelSaveData.Data[completeData.Index].highScore = completeData.Score;
        DataHandler.Save(_levelSaveData, DataKeys.LevelScoreDataKey);
    }

    void LevelDataNeeded_Callback()
    {
        LevelEvents.OnSpawnLevelSelectionButtons?.Invoke(_levelSaveData.Data);
    }
}
