
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterCollection : Singleton<UICharacterCollection>
{
    [SerializeField] private CharacterCollector collector;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private GameObject profile;

    private List<Button> profileButtons;

    [Header("Character Selection")]
    [SerializeField] private UICharacterSelection selection;

    private void Start()
    {
        profileButtons = new List<Button>();

        InitCharacterProfiles();
    }

    public void InitCharacterProfiles()
    {
        foreach(var character in collector.Characters)
        {
            var newProfile = Instantiate(profile, transform);

            newProfile.GetComponent<UICharacterProfile>().Character = character.pref;

            //newProfile.GetComponent<Button>().onClick.AddListener(SetPlayerCharacter);
            //profileButtons.Add(newProfile.GetComponent<Button>());

            newProfile.GetComponentInChildren<Toggle>().group = toggleGroup;
            newProfile.GetComponentInChildren<Toggle>().onValueChanged.AddListener(SetPlayerCharacter);

            //if (character.locked) newProfile.GetComponentInChildren<Toggle>().interactable = false;
        }
    }

    public void ResetCharacterProfiles()
    {
        var profiles = GetComponentsInChildren<UICharacterProfile>();

        for (int i = 0; i < profiles.Length; i++)
            Destroy(profiles[i].gameObject);

        InitCharacterProfiles();

        PlayerManager.instance.Collector.Characters.Find(c => c.CharacterName == "FatCat").locked = true;
        PlayerManager.instance.Collector.Characters.Find(c => c.CharacterName == "WildCat").locked = true;
        PlayerManager.instance.Collector.Characters.Find(c => c.CharacterName == "Civet").locked = true;
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
            var selectedCharacterProfile = collector.GetCharacterProfile(GetSelectedCharacter().GetComponent<Player>().name);
            PlayerManager.instance.SetPlayerCharacter(selectedCharacterProfile.pref);

            selection.UpdateSelectionPanel(selectedCharacterProfile); 
        }
    }
}
