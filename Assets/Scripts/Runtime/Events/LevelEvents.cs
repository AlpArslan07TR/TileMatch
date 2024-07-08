using System;
public class LevelEvents
{
    public static Action<int> OnLevelSelected;
    public static Action OnLevelDataNeeded;
    public static Action<LevelScoreData[]> OnSpawnLevelSelectionButtons;
    public static Action<CompleteData> OnLevelWin;
}
