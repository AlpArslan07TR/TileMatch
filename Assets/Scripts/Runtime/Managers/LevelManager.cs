using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelSelectionSO levelSelectionSO;
    [SerializeField] TextAsset[] levelFiles;
    LevelData[] _levelData;
    LevelSaveData _levelSaveData;


    void Awake()
    {
        ReadLevels();
        Load();
        LevelEvents.OnLevelSelected += LevelSelected;
        LevelEvents.OnLevelWin += Save_Callback;
        LevelEvents.OnLevelDataNeeded += LevelDataNeeded_Callback;
        GameEvents.OnCompleted += OnCompleteCallBack;
        
    }

    void OnDestroy()
    {
        LevelEvents.OnLevelSelected -= LevelSelected;
        LevelEvents.OnLevelWin -= Save_Callback;
        LevelEvents.OnLevelDataNeeded -= LevelDataNeeded_Callback;
        GameEvents.OnCompleted -= OnCompleteCallBack;
    }
    void LevelSelected(int index)
    {
        levelSelectionSO.levelIndex = index;
        levelSelectionSO.levelData = _levelData[index];
        levelSelectionSO.score = _levelSaveData.Data[index].highScore;
        SceneEvent.OnLoadGameScene?.Invoke();
        
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

    private void OnCompleteCallBack()
    {
        if(levelSelectionSO.score< ScoreManager.Instance.Score)
        {
            Win();
        }
        else
        {
            Fail();
        }
    }

    private void Win()
    {
        GameEvents.OnWin?.Invoke();
        Save_Callback(GetCompleteData());
        LoadMetaScene();
    }

   
    private void Fail()
    {
        GameEvents.OnFail?.Invoke();
    }
    
    
    private CompleteData GetCompleteData()
    {
        return new CompleteData(levelSelectionSO.levelIndex, ScoreManager.Instance.Score);
    }

    private async void LoadMetaScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        SceneEvent.OnLoadMetaScene?.Invoke();
    }
}
