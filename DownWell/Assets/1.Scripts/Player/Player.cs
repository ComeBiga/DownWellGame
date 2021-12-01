using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public new string name = "";
    public int num;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "StageEnd")
            {
                // StageEnd
                //GameManager.instance.StageEnd();
                BossStageManager.instance.StartBossStage();
                Destroy(collision.gameObject);
            }
        }
    }
}
