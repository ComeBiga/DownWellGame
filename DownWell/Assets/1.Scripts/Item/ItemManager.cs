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

    [HideInInspector]
    public float originInvincibleTime;
    [HideInInspector]
    public string curItem = "";

    public GameObject bubble;

    void Start()
    {
        playercombat = GetComponent<PlayerCombat>();
        itemImage = GameObject.Find("ItemSocket").GetComponent<Image>();
        originInvincibleTime = playercombat.invincibleTime;
        itemImage.enabled = false;
    }

    public void ItemSocket(GameObject getitem)
    {
        if (getitem.name == "Item(Clone)") return;
        itemImage.sprite = getitem.GetComponent<SpriteRenderer>().sprite;
        curItem = getitem.name;
        itemImage.enabled = true;
    }

    public void UseItem()
    {
        itemImage.enabled = false;

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

    void DestroyObj()
    {
        Destroy(this.gameObject.transform.GetChild(3).gameObject);
    }
}