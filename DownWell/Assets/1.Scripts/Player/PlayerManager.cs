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

    public GameObject playerObject;
    [SerializeField] private CharacterCollector characters;
    public CharacterCollector Collector { get { return characters; } }
    //public List<GameObject> characters;

    //public Character selectedCharacter;
    //public List<Character> characters1 = new List<Character>();

    public string stageName = "";

    private void Start()
    {
        
    }

    public void Init()
    {
        if (playerObject != null) playerObject = null;
        playerObject = characters.GetCharacterProfile(0).pref;
    }

    public void DestoryPlayerObject()
    {
        Destroy(playerObject);
    }

    public void Instantiate(Vector3 position)
    {
        playerObject = Instantiate(playerObject, position, Quaternion.identity);
        playerObject.GetComponent<PlayerPhysics>().Init();
        playerObject.GetComponent<PlayerAttack>().InitWeapons();
        //Camera.main.GetComponent<SmoothFollow>().InitFollowCamera(playerObject.transform);
    }
    
    public void Instantiate(GameObject playerObject, Vector3 position)
    {
        this.playerObject = Instantiate(playerObject, position, Quaternion.identity);
        this.playerObject.GetComponent<PlayerPhysics>().Init();
    }

    public void SetPlayerCharacter(GameObject playerObject)
    {
        this.playerObject = playerObject;
    }

    #region Deprecated(SelectedPlayer)
    ///// <summary>
    ///// 선택한 캐릭터를 플레이어의 캐릭터로 선택
    ///// </summary>
    ///// <param name="name">캐릭터 이름</param>
    //public void SelectPlayerCharacter(string name)
    //{
    //    //player = characters.Find(c => c.GetComponent<Player>().name == name);

    //    selectedCharacter = characters1.Find(c => c.name == name);
    //}

    ///// <summary>
    ///// 선택한 캐릭터를 플레이어의 캐릭터로 선택
    ///// </summary>
    ///// <param charNum="charNum">캐릭터 번호</param>
    //public void SelectPlayerCharacter(int charNum)
    //{
    //    //player = characters.Find(c => c.GetComponent<Player>().num == charNum);

    //    selectedCharacter = characters1.Find(c => c.num == charNum);
    //}

    ///// <summary>
    ///// 선택한 캐릭터를 리소스 폴더로 부터 로드
    ///// </summary>
    ///// <param name="fileName"></param>
    ///// <returns>로드한 캐릭터</returns>
    //public Character SelectPlayerCharacterFromResource(string fileName)
    //{
    //    return Resources.Load("Characters/" + name) as Character;
    //}

    ///// <summary>
    ///// 리소스 폴더로 부터 모든 캐릭터 로드
    ///// </summary>
    //public void LoadAllCharacterFromResource()
    //{
    //    var characters = Resources.LoadAll("Characters", typeof(Character));

    //    foreach(var c in characters)
    //    {
    //        this.characters1.Add(c as Character);
    //    }
    //}
    #endregion
}
