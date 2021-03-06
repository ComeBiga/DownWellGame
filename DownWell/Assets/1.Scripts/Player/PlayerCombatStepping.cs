using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerCombatStepping : MonoBehaviour
{
    // Hitbox
    [SerializeField] private GameObject hitBox;

    // Rigidbody
    public float stepUpSpeed = 7f;
    public float delayAsStep = .5f;
    private bool shootable = true;
    private bool shotLock = false;
    
    public bool ShotLock { get { return shotLock; } }

    // Enemy Collision
    private Collider2D[] enemyColliders;
    private ContactFilter2D enemyFilter;
    private LayerMask enemyLayerMask;

    // Event
    public event System.Action onStep;          // deprecated
    [Space]
    public UnityEvent OnStep;

    public PlayerCombatStepping(GameObject hitBox)
    {
        this.hitBox = hitBox;
        stepUpSpeed = 7f;
        delayAsStep = .5f;

        enemyFilter = new ContactFilter2D();
        enemyFilter.useLayerMask = true;
        enemyFilter.layerMask = LayerMask.GetMask("Enemy");
        Debug.Log(enemyFilter.layerMask);
    }

    private void Start()
    {
        enemyColliders = new Collider2D[3];

        enemyFilter = new ContactFilter2D();
        enemyFilter.useLayerMask = true;
        enemyFilter.layerMask = LayerMask.GetMask("Enemy");

        GetComponent<PlayerPhysics>().OnGrounded += OnGround;
    }

    public void Update()
    {
        var enemyColliders = new Collider2D[3];
        var hitNum = hitBox.GetComponent<CircleCollider2D>().OverlapCollider(enemyFilter, enemyColliders);

        foreach (var enemyCollider in enemyColliders)
        {
            if (enemyCollider != null)
            {
                if (!enemyCollider.GetComponent<Enemy>().Stepped()) break;

                //onStep.Invoke();
                OnStep.Invoke();

                // onStep, 탄창 클래스에서 참조
                //if (!reloaded) Reload();

                // onStep
                //StartCoroutine(SteppingUp());
                BeUnshootableForTime(delayAsStep);

                GetComponent<PlayerPhysics>().LeapOff(stepUpSpeed);

                break;
            }
        }
    }

    private void BeUnshootableForTime(float time)
    {
        StartCoroutine(EBeUnshootableForTime(time));
    }

    private IEnumerator EBeUnshootableForTime(float time)
    {
        //GetComponent<PlayerAttack>().weapon.shootable = false;
        shotLock = true;
        yield return new WaitForSeconds(time);

        //GetComponent<PlayerAttack>().weapon.shootable = true;
        shotLock = false;
    }

    private void OnGround()
    {
        shotLock = false;
    }
}
