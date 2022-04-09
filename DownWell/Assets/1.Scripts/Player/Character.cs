using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string name = "";
    public int num;
    public Sprite sprite;
    public Color color;
    public RuntimeAnimatorController animatorController;
    public GameObject prefab;

    [Header("Health")]
    public int health;

    [Header("Movement")]
    public int speed;
    public int jumpSpeed;

    public void InitPlayerValues(GameObject player)
    {
        player.name = name;
        player.GetComponent<SpriteRenderer>().sprite = sprite;
        player.GetComponent<SpriteRenderer>().color = color;
        player.GetComponent<Animator>().runtimeAnimatorController = animatorController;

        player.GetComponent<PlayerHealth>().MaxHealth = health;

        player.GetComponent<PlayerController>().speed = speed;
        player.GetComponent<PlayerController>().jumpSpeed = jumpSpeed;
    }
}
