using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Move")]
    public RectTransform movePanel;
    public Image left;
    public Image right;
    public Sprite normalMove;
    public Sprite pressedMove;

    [Header("Jump")]
    public Image jump;
    public Sprite normalJump;
    public Sprite pressedJump;

    public void SetSize(int ratio)
    {
        movePanel.localScale *= ratio / 100f;
    }

    /// <summary>
    /// 0:normal, 1:pressed
    /// </summary>
    /// <param name="value"></param>
    public void SetLeftButton(int value)
    {
        switch(value)
        {
            case 0:
                left.sprite = normalMove;
                break;
            case 1:
                left.sprite = pressedMove;
                break;
        }
    }

    /// <summary>
    /// 0:normal, 1:pressed
    /// </summary>
    /// <param name="value"></param>
    public void SetRightButton(int value)
    {
        switch (value)
        {
            case 0:
                right.sprite = normalMove;
                break;
            case 1:
                right.sprite = pressedMove;
                break;
        }
    }

    /// <summary>
    /// 0:normal, 1:pressed
    /// </summary>
    /// <param name="value"></param>
    public void SetJumpButton(int value)
    {
        switch (value)
        {
            case 0:
                jump.sprite = normalJump;
                break;
            case 1:
                jump.sprite = pressedJump;
                break;
        }
    }
}
