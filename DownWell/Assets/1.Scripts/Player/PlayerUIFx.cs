using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIFx : MonoBehaviour
{
    public List<UIFx> uifxs;

    [System.Serializable]
    public class UIFx
    {
        public string name = "";
        public GameObject go;
    }

    public float duration;

    public void Display(string name)
    {
        var uf = uifxs.Find(u => u.name == name);

        StartCoroutine(EDisplay(uf));
    }

    private IEnumerator EDisplay(UIFx uifx)
    {
        uifx.go.SetActive(true);
        //Debug.Log($"Display {uifx.name}");

        yield return new WaitForSeconds(duration);

        uifx.go.SetActive(false);
    }
}
