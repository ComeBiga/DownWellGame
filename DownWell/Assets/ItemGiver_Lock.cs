using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver_Lock : ItemGiver
{
    public int unlockCoinCount = 100;
    public GameObject dialogueBox;

    [HideInInspector]
    public bool showingDialogue = false;
    public float dialogueOffset = 1f;
    private bool locked = true;

    public override void Hit(int damage = 0)
    {
        if (!showingDialogue && UICollector.Instance.coin.Current < unlockCoinCount)
        {
            var newDialogueBox = Instantiate(dialogueBox, transform.position + Vector3.up * dialogueOffset, Quaternion.identity);
            newDialogueBox.GetComponent<DialogueBox>().onInStateEnter += () => { showingDialogue = true; };
            newDialogueBox.GetComponent<DialogueBox>().onOutStateExit += () => {
            showingDialogue = false;
        };

        }
    }

    public override void Destroy()
    {
        if (locked)
            return;

        itemDropper.Random(transform.position);

        GetComponent<Effector>().Generate("Destroy");

        Destroy(this.gameObject);

    }
}
