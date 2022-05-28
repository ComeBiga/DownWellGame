
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterCollection : MonoBehaviour
{
    [SerializeField] private CharacterCollector collector;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private GameObject profile;

    private void Start()
    {
        InitCharacterProfiles();
    }

    public void InitCharacterProfiles()
    {
        foreach(var character in collector.Characters)
        {
            var newProfile = Instantiate(profile, transform);

            newProfile.GetComponent<UICharacterProfile>().Character = character;

            newProfile.GetComponentInChildren<Toggle>().group = toggleGroup;
            newProfile.GetComponentInChildren<Toggle>().onValueChanged.AddListener(SetPlayerCharacter);
        }
    }

    public GameObject GetSelectedCharacter()
    {
        var toggled = toggleGroup.ActiveToggles().FirstOrDefault();
        Debug.Log(toggled.GetComponentInParent<UICharacterProfile>());

        return toggled.GetComponentInParent<UICharacterProfile>().Character;
    }

    private void SetPlayerCharacter(bool value)
    {
        if(value)
        {
            PlayerManager.instance.SetPlayerCharacter(GetSelectedCharacter());
        }
    }
}
