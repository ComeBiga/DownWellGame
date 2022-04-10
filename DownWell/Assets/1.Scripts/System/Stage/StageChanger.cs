using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChanger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {
                if (StageManager.instance.Current.BossObject == null)
                    GameManager.instance.ClearStage();
                else
                    BossStageManager.instance.StartBossStage();

                Destroy(this.gameObject);
            }
        }
    }
}
