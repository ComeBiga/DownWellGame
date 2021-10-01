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
    /// ������ ĳ���͸� �÷��̾��� ĳ���ͷ� ����
    /// </summary>
    /// <param name="name">ĳ���� �̸�</param>
    public void SelectPlayerCharacter(string name)
    {
        // player = characters.Find(c => c.GetComponent<Player>().name == name);
    }
}
