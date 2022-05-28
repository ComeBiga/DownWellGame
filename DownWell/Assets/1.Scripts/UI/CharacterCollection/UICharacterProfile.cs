using UnityEngine;
using UnityEngine.UI;

public class UICharacterProfile : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private Image profileImage;

    public GameObject Character 
    {
        get 
        {
            return character;
        }
        set
        {
            character = value;
            InitImage();
        }
    }

    public void InitImage()
    {
        profileImage.sprite = character.GetComponent<SpriteRenderer>().sprite;
    }
}
