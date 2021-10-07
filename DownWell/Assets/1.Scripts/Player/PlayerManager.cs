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
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public GameObject player;
    //public List<GameObject> characters;

    public Character selectedCharacter;
    public List<Character> characters1 = new List<Character>();

    private void Start()
    {
        LoadAllCharacterFromResource();

        if(selectedCharacter == null) SelectPlayerCharacter("Cat");

        //startPos = GameManager.instance.startPos;
        //player = Instantiate(player, startPos.position, Quaternion.identity);
    }

    /// <summary>
    /// ������ ĳ���͸� �÷��̾��� ĳ���ͷ� ����
    /// </summary>
    /// <param name="name">ĳ���� �̸�</param>
    public void SelectPlayerCharacter(string name)
    {
        //player = characters.Find(c => c.GetComponent<Player>().name == name);

        selectedCharacter = characters1.Find(c => c.name == name);
    }

    /// <summary>
    /// ������ ĳ���͸� �÷��̾��� ĳ���ͷ� ����
    /// </summary>
    /// <param charNum="charNum">ĳ���� ��ȣ</param>
    public void SelectPlayerCharacter(int charNum)
    {
        //player = characters.Find(c => c.GetComponent<Player>().num == charNum);

        selectedCharacter = characters1.Find(c => c.num == charNum);
    }

    /// <summary>
    /// ������ ĳ���͸� ���ҽ� ������ ���� �ε�
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>�ε��� ĳ����</returns>
    public Character SelectPlayerCharacterFromResource(string fileName)
    {
        return Resources.Load("Characters/" + name) as Character;
    }

    /// <summary>
    /// ���ҽ� ������ ���� ��� ĳ���� �ε�
    /// </summary>
    public void LoadAllCharacterFromResource()
    {
        var characters = Resources.LoadAll("Characters", typeof(Character));

        foreach(var c in characters)
        {
            this.characters1.Add(c as Character);
        }
    }
}
