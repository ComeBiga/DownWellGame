using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushInputManager : MonoBehaviour
{
    #region Singleton
    public static BrushInputManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    private void Update()
    {
        ClickLeftMouseButton();
    }

    void ClickLeftMouseButton()
    {
        if (Input.GetMouseButton(0))
        {
            var tileInfo = GetTileInfoOnMousePosition();

            if (tileInfo != null)
            {
                //BrushManager.instance.PaintTile(tileInfo);
                tileInfo.SetInfo(BrushManager.instance.currentBrush);
                JsonIO.levelChanged = true;
            }
        }

        if(Input.GetMouseButton(1))
        {
            var tileInfo = GetTileInfoOnMousePosition();

            if (tileInfo != null)
            {
                tileInfo.SetInfo(BrushManager.instance.eraserBrush);
                JsonIO.levelChanged = true;
            }
        }
    }

    public TileInfo GetTileInfoOnMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null)
        {
            //Debug.Log(hit.transform.name);
            return hit.transform.gameObject.GetComponent<TileInfo>();
        }

        return null;
    }
}
