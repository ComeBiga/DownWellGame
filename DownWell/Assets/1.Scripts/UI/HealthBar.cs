using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject hp;

    void Start()
    {   
    }

    void Update()
    {
        PlayerHealthEvent();
    }

    public void UpdateBar()
    {
        Destroy(hp.transform.GetChild(0).gameObject);
    }

    public void PlayerHealthEvent()
    {
        PlayerManager.instance.player.GetComponent<PlayerHealth>().OnChangedHealth += UpdateBar;
    }
}
