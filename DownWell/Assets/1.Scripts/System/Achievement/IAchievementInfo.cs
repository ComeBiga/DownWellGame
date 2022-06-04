using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAchievementInfo
{
    public string Name { get; }
    public string Description { get; }
    public Sprite Image { get; }
    public Achievement.RewardType TypeOfReward { get; }
}
