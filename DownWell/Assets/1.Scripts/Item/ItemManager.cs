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

<<<<<<< HEAD
    float originInvincibleTime;
    bool useHealth;
    
=======
    [HideInInspector]
    public float originInvincibleTime;
>>>>>>> e4284d82d190c5475bedba456f4f93a63b508559
    [HideInInspector]
    public string curItem = "";

    public GameObject bubble;

    void Start()
    {
        playercombat = GetComponent<PlayerCombat>();
        itemImage = GameObject.Find("ItemSocket").GetComponent<Image>();
<<<<<<< HEAD
        
=======
        originInvincibleTime = playercombat.invincibleTime;
>>>>>>> e4284d82d190c5475bedba456f4f93a63b508559
        itemImage.enabled = false;
    }

    public void ItemSocket(GameObject getitem)
    {
        if (getitem.name == "Item(Clone)") return;
<<<<<<< HEAD
        
=======
>>>>>>> e4284d82d190c5475bedba456f4f93a63b508559
        itemImage.sprite = getitem.GetComponent<SpriteRenderer>().sprite;
        curItem = getitem.name;
        itemImage.enabled = true;
    }

<<<<<<< HEAD
    public void UseItem() //발동형  ex) 방어방울
    {
        itemImage.enabled = false;

        originInvincibleTime = playercombat.invincibleTime;
        useHealth = playercombat.useLoseHealth;

=======
    public void UseItem()
    {
        itemImage.enabled = false;

>>>>>>> e4284d82d190c5475bedba456f4f93a63b508559
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

<<<<<<< HEAD
    public void ImmediatelyUseItem(GameObject getitem)  //소모형 ex) 포션
    {
        switch(getitem.name)
        {
            case "potion(Clone)":
                if (PlayerManager.instance.player.GetComponent<PlayerHealth>().CurrentHealth
                    < PlayerManager.instance.player.GetComponent<PlayerHealth>().MaxHealth
                    && PlayerManager.instance.player.GetComponent<PlayerHealth>().CurrentHealth > 0)
                PlayerManager.instance.player.GetComponent<PlayerHealth>().GainHealth(1);
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
=======
    void DestroyObj()
    {
>>>>>>> e4284d82d190c5475bedba456f4f93a63b508559
        Destroy(this.gameObject.transform.GetChild(3).gameObject);
    }
}