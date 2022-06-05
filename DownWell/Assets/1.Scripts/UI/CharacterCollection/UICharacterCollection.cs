
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterCollection : Singleton<UICharacterCollection>
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

            newProfile.GetComponent<UICharacterProfile>().Character = character.pref;

            newProfile.GetComponentInChildren<Toggle>().group = toggleGroup;
            newProfile.GetComponentInChildren<Toggle>().onValueChanged.AddListener(SetPlayerCharacter);

            if (character.locked) newProfile.GetComponentInChildren<Toggle>().interactable = false;
        }
    }

    public void ResetCharacterProfiles()
    {
        var profiles = GetComponentsInChildren<UICharacterProfile>();

        for (int i = 0; i < profiles.Length; i++)
            Destroy(profiles[i].gameObject);

        InitCharacterProfiles();
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
