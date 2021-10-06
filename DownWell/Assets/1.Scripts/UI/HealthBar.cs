using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillbar;

    private void Start()
    {

    }

    public void UpdateBar()
    {

    }

    public void PlayerHealthEvent()
    {
        PlayerManager.instance.player.GetComponent<PlayerHealth>().OnChangedHealth += UpdateBar;
    }
}
