using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : Singleton<AchievementSystem>
{
    [SerializeField] public Queue<IAchievementInfo> achievedList;
    [SerializeField] private List<Achievement> achievements;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        achievedList = new Queue<IAchievementInfo>();

        foreach (var achievement in achievements)
        {
            achievement.OnAchieve += GetAchievementData;
            //achievement.OnAchieve += (achieved) => achievedList.Enqueue(achieved);
        }
    }

    private List<Achievement> FindAchievements(string target)
    {
        return achievements.FindAll(a => a.RequireTarget(target));
    }

    public void ProgressAchievement(string target, int amount = 1)
    {
        var _achievements = FindAchievements(target);

        if (_achievements == null) return;

        foreach(var achievement in _achievements)
        {
            achievement.Progress(target, amount);
        }
    }

    private void GetAchievementData(IAchievementInfo achivementInfo)
    {
        Debug.Log($"GetAchievementData");
        achievedList.Enqueue(achivementInfo);
    }

    public Queue<IAchievementInfo> RewardAsAllAchieved()
    {
        if (achievedList == null)
            return null;

        foreach(var achieved in achievedList)
        {
            RewardByType(achieved);
        }

        return achievedList;
    }

    public void RewardAsAchieved()
    {
        if (achievedList == null)
            return;

        RewardByType(achievedList.Dequeue());
    }

    private void RewardByType(IAchievementInfo achieved)
    {
        switch(achieved.TypeOfReward)
        {
            case Achievement.RewardType.CHARACTER_UNLOCK:
                var characterToUnlock = PlayerManager.instance.Collector.Characters.Find(c => c.CharacterName == achieved.Name);
                characterToUnlock.locked = false;
                break;
        }

        Debug.Log(PlayerManager.instance.Collector.Characters.Find(c => c.CharacterName == achieved.Name).locked);
    }

    public void ResetAllAchievements()
    {
        foreach(var achievement in achievements)
        {
            achievement.Reset();
        }
    }
}
