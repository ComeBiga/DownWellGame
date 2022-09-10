using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public float activeDuration = 3f;

    public Action onInStateEnter;
    public Action onInStateExit;
    public Action onOutStateEnter;
    public Action onOutStateExit;

    private void Awake()
    {
        onInStateExit += OutExit;
        onOutStateExit += () => {
            Destroy(this.gameObject);
        };
    }

    public void OutExit()
    {
        Invoke("SetProperty", activeDuration);
    }

    private void SetProperty()
    {
        GetComponent<Animator>().SetTrigger("Out");
    }
}
