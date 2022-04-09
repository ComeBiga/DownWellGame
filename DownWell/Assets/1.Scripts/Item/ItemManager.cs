using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    #region Singleton
    public static ItemManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    Image itemImage;
    PlayerCombat playercombat;

    float originInvincibleTime;
    bool useHealth;

    [HideInInspector]
    public string curItem = "";

    public GameObject bubble;

    void Start()
    {
        playercombat = GetComponent<PlayerCombat>();
        itemImage = GameObject.Find("ItemSocket").GetComponent<Image>();
        itemImage.enabled = false;
    }

    public void ItemSocket(GameObject getitem)
    {
        if (getitem.name == "Item(Clone)") return;

        itemImage.sprite = getitem.GetComponent<SpriteRenderer>().sprite;
        curItem = getitem.name;
        itemImage.enabled = true;
    }

    public void UseItem() //발동형  ex) 방어방울
    {
        itemImage.enabled = false;

        originInvincibleTime = playercombat.invincibleTime;
        useHealth = playercombat.useLoseHealth;

        switch (curItem)
        {
            case "Bubble(Clone)":
                Instantiate(bubble, transform.position, Quaternion.identity, this.gameObject.transform);
                playercombat.useLoseHealth = false;
                playercombat.invincibleTime = 1f;
                Invoke("DestroyObj", playercombat.invincibleTime);
                break;
        }

        curItem = "";
    }

    public void ImmediatelyUseItem(GameObject getitem)  //소모형 ex) 포션
    {
        switch(getitem.name)
        {
            case "potion(Clone)":
                if (PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().CurrentHealth
                    < PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().MaxHealth
                    && PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().CurrentHealth > 0)
                PlayerManager.instance.playerObject.GetComponent<PlayerHealth>().GainHealth(1);
                break;
        }
    }

    void originStatus()
    {
        playercombat.invincibleTime = originInvincibleTime;
        playercombat.useLoseHealth = useHealth;
    }

    void DestroyObj()
    {
        originStatus();
        Destroy(this.gameObject.transform.GetChild(3).gameObject);
    }
}