using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    public void LoseHealth()
    {
        if (GetComponent<PlayerDamaged>().IsInvincible)
            return;

        health--;
        Debug.Log("Lose hp");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene(0);
    }
}
