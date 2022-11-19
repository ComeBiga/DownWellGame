using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IHitByProjectile
{
    GameObject bossObject;
    public GameObject upperBossObject;
    public float distanceFromBoss = 19f;

    public enum BossState { normal, rage }
    [SerializeField] BossState currentState = BossState.normal;

    public int maxHealth = 100;
    public Health health;
    public float damagedInterval = 0.5f;

    [Header("Damaged")]
    public Color bodyColor;
    public Color damagedColor;

    [Header("Death")]
    public GameObject dyingObj;
    private bool died = false;

    private ContactFilter2D filter;

    private void Start()
    {
        health = new Health(maxHealth);
        upperBossObject.GetComponent<SpriteRenderer>().color = Color.clear;

        filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.useTriggers = true;
        filter.layerMask = LayerMask.GetMask("Player");

        GetComponent<SpriteRenderer>().material.SetColor("_HitColor", damagedColor);

        //ColorChecker();
    }

    private void ColorChecker()
    {
        var pixels = GetComponent<SpriteRenderer>().sprite.texture.GetPixels();
        List<Color> exColor = new List<Color>();

        for (int i = 0; i < pixels.Length; i++)
        {
            if(exColor.Contains(pixels[i]) == false)
            {
                exColor.Add(pixels[i]);
                Debug.Log($"{pixels[i]}, {GetComponent<SpriteRenderer>().sprite.texture.name}");
            }
        }
    }

    private void Update()
    {
        //var colliders = new List<Collider2D>();
        //GetComponent<Collider2D>().OverlapCollider(filter.NoFilter(), colliders);

        //foreach (var collider in colliders)
        //{
        //    Debug.Log("Collide");
        //    if (collider != null && collider.tag == "Player")
        //    {
        //        var player = collider.transform;

        //    Debug.Log("Collide Player");
        //        player.GetComponent<PlayerCombat>().Damaged(transform);

        //        return;
        //    }
        //}
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

        if (Comebiga.SoundManager.instance != null) Comebiga.SoundManager.instance.Stop("BackgroundBoss");

        Destroy(this.gameObject);
        Destroy(upperBossObject);
        Instantiate(dyingObj, transform.position, Quaternion.identity);
        //BossStageManager.instance.EndBossStage();
    }

    private IEnumerator DamagedFX()
    {
        //GetComponent<SpriteRenderer>().color = Color.black;
        //GetComponent<SpriteRenderer>().material.SetColor("_HitColor", damagedColor);
        ChangeColor(damagedColor, -1);

        yield return new WaitForSeconds(damagedInterval);

        //GetComponent<SpriteRenderer>().color = Color.white;
        ChangeColor(bodyColor, 1);
    }

    public void Hit(int damage = 0)
    {
        Damaged(damage);
    }

    private void ChangeColor(Color color, int isHit)
    {
        //RuntimeAnimatorController ac = gameObject.GetComponent<Animator>().runtimeAnimatorController;
        //gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
        //gameObject.GetComponent<SpriteRenderer>().color = color;
        //gameObject.GetComponent<Animator>().runtimeAnimatorController = ac;

        //GetComponent<SpriteRenderer>().material.color = color;
        GetComponent<SpriteRenderer>().material.SetInt("_IsHit", isHit);
        //GetComponent<SpriteRenderer>().sharedMaterial.color = color;
    }

    public void InstantiateUpperBoss(Transform parent)
    {
        upperBossObject = Instantiate(upperBossObject, parent);
        upperBossObject.transform.localPosition = new Vector3(0, transform.position.y + distanceFromBoss, 0);
        //StartCoroutine(EUpdateUpperBoss());
    }

    public void UpdateUpperBoss()
    {
        StartCoroutine(EUpdateUpperBoss());
    }

    private IEnumerator EUpdateUpperBoss()
    {
        while(true)
        {
            if (died)
                break;

            upperBossObject.transform.localPosition = new Vector3(0, transform.position.y + distanceFromBoss, 0);

            yield return null;
        }
    }
}
