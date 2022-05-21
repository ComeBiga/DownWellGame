using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IHitByProjectile
{
    GameObject bossObject;
    public GameObject upperBossObject;

    public enum BossState { normal, rage }
    [SerializeField] BossState currentState = BossState.normal;

    public int maxHealth = 100;
    public Health health;
    public float damagedInterval = 0.5f;

    [Header("Death")]
    public GameObject dyingObj;
    private bool died = false;

    private void Start()
    {
        health = new Health(maxHealth);
        upperBossObject.GetComponent<SpriteRenderer>().color = Color.clear;
    }

    #region Object Handle
    public void GetBoss()
    {
        bossObject = GameObject.FindGameObjectWithTag("Boss");
        SetBossActive(false);
    }

    public void SetBossActive(bool value)
    {
        bossObject.SetActive(value);
    }
    #endregion

    public void SetState(BossState state)
    {
        currentState = state;
    }

    public bool UnderHealthRatio(int healthRatio)
    {
        if (health.CurrentRatio() < healthRatio)
        {
            //SetState(BossState.rage);
            return true;
        }

        return false;
    }

    public void Damaged(int amount)
    {
        if (died) return;

        health.Lose(amount);

        if (health.Current <= 0)
            Die();

        StartCoroutine(DamagedFX());
    }

    private void Die()
    {
        Debug.Log("Boss Die");
        died = true;
        Destroy(this.gameObject);
        Instantiate(dyingObj, transform.position, Quaternion.identity);
        //BossStageManager.instance.EndBossStage();
    }

    private IEnumerator DamagedFX()
    {
        GetComponent<SpriteRenderer>().color = Color.black;

        yield return new WaitForSeconds(damagedInterval);

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Hit(int damage = 0)
    {
        Damaged(damage);
    }
}
