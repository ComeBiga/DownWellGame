using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public GameObject player;
    public List<GameObject> characters;

    /// <summary>
    /// 선택한 캐릭터를 플레이어의 캐릭터로 선택
    /// </summary>
    /// <param name="name">캐릭터 이름</param>
    public void SelectPlayerCharacter(string name)
    {
        // player = characters.Find(c => c.GetComponent<Player>().name == name);
    }
}
