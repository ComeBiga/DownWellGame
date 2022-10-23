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
    public GameObject giveItem;
    private bool locked = true;

    public override void Hit(int damage = 0)
    {
        if (UICollector.Instance.coin.Current < unlockCoinCount)
        {
            if(!showingDialogue)
            {
                var newDialogueBox = Instantiate(dialogueBox, transform.position + Vector3.up * dialogueOffset, Quaternion.identity);
                newDialogueBox.GetComponent<DialogueBox>().onInStateEnter += () => { showingDialogue = true; };
                newDialogueBox.GetComponent<DialogueBox>().onOutStateExit += () => {
                    showingDialogue = false;
                };
                newDialogueBox.GetComponent<DialogueBox>().txtDialogue.text = $"{unlockCoinCount}";
            }
        }
        else
        {
            locked = false;
            UICollector.Instance.coin.Use(unlockCoinCount);

            Destroy();
        }
    }

    public override void Destroy()
    {
        if (locked)
            return;

        itemDropper.Random(transform.position);
        itemDropper.DropItem(giveItem, transform.position);

        GetComponent<Effector>().Generate("Destroy");

        Destroy(this.gameObject);

    }
}
