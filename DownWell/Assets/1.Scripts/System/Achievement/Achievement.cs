using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Achievement", menuName = "Achievement/Achievement")]
public class Achievement : ScriptableObject, IAchievementInfo
{
    [SerializeField] private string name = "";
    [SerializeField] private string description = "";
    [SerializeField] private Sprite image;
    public enum RewardType { CHARACTER_UNLOCK };
    [SerializeField] private RewardType rewardType;

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public Sprite Image { get { return image; } }
    public RewardType TypeOfReward { get { return rewardType; } }


    [System.Serializable]
    public class Requirement
    {
        public string target;
        public int requireAmount;
        [HideInInspector] public int currentAmount;
        [HideInInspector] public bool achieved = false;

        public bool IsAchieved
        {
            get
            {
                return currentAmount >= requireAmount;
            }
        }
    }

    [SerializeField] protected Requirement[] requirements;

    public event System.Action<IAchievementInfo> OnAchieve;

    public bool RequireTarget(string target)
    {
        foreach(var rq in requirements)
        {
            if (rq.target == target)
                return true;
        }

        return false;
    }

    public void Progress(string target, int amount = 1)
    {
        var requirement = GetRequirement(target);

        if (requirement.achieved) return;

        requirement.currentAmount += amount;
        AchieveRequirement(requirement);
    }

    private void AchieveRequirement(Requirement requirement)
    {
        if(requirement.IsAchieved)
        {
            requirement.achieved = true;

            if (IsAchievedAll())
                OnAchieve?.Invoke(this);
        }
    }

    public bool IsAchievedAll()
    {
        var achieveCount = 0;

        foreach(var rq in requirements)
        {
            if (rq.achieved)
                achieveCount++;
        }

        return (achieveCount >= requirements.Length) ? true : false;
    }

    private Requirement GetRequirement(string target)
    {
        foreach(var rq in requirements)
        {
            if (rq.target == target)
                return rq;
        }

        return null;
    }
}
