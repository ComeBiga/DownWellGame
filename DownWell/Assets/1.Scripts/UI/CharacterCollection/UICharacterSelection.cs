using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelection : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private Text characterName;
    [SerializeField] private Text characterDescription;

    [Header("Button")]
    [SerializeField] private GameObject selectBtn;
    [SerializeField] private GameObject buyBtn;
    [SerializeField] private GameObject lockBtn;

    /// <summary>
    /// 0:selectButton, 1:buyButton, 2:lockButton
    /// </summary>
    /// <param name="num"></param>
    public void SwitchButton(int num)
    {
        switch(num)
        {
            case 0:
                selectBtn.SetActive(true);
                buyBtn.SetActive(false);
                lockBtn.SetActive(false);
                break;
            case 1:
                selectBtn.SetActive(false);
                buyBtn.SetActive(true);
                lockBtn.SetActive(false);
                break;
            case 2:
                selectBtn.SetActive(false);
                buyBtn.SetActive(false);
                lockBtn.SetActive(true);
                break;
        }
    }

    public void SetSelectionPanel(CharacterCollector.CharacterProfile characterProfile)
    {
        characterName.text = characterProfile.CharacterName;
        characterDescription.text = characterProfile.description;

        buyBtn.GetComponentInChildren<Text>().text = "-100";
        
        if (characterProfile.locked && characterProfile.canBuy)
        {
            SwitchButton(1);
        }
        else if(characterProfile.locked)
        {
            SwitchButton(2);
        }
        else
        {
            SwitchButton(0);
        }
    }

    public void SetSelectionPanel(string name, string description, string price = "10")
    {
        characterName.text = name;
        characterDescription.text = description;

        buyBtn.GetComponentInChildren<Text>().text = price;
    }
}
