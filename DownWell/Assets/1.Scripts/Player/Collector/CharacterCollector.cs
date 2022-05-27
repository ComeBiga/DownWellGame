using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Collection", menuName = "Character/CharacterCollection")]
public class CharacterCollector : ScriptableObject
{
    [SerializeField] private List<GameObject> characters;

    public List<GameObject> Characters { get { return characters; } }

    public GameObject GetCharacter(string name)
    {
        return characters.Find(n => n.GetComponent<Player>().name == name);
    }
    
    public GameObject GetCharacter(int num)
    {
        return characters.Find(n => n.GetComponent<Player>().num == num);
    }
}
