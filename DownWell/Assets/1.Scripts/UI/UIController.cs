using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Move")]
    public RectTransform movePanel;
    public Image left;
    public Image right;
    public Sprite normalMoveL;
    public Sprite pressedMoveL;
    public Sprite normalMoveR;
    public Sprite pressedMoveR;

    [Header("Jump")]
    public RectTransform jumpPanel;
    public Image jump;
    public Sprite normalJump;
    public Sprite pressedJump;

    public void SetSize(int ratio)
    {
        movePanel.localScale *= ratio / 100f;

        jumpPanel.localScale *= ratio / 100f;
    }

    public void SetOffset(float offset)
    {
        movePanel.localPosition += Vector3.right * offset;
        jumpPanel.localPosition -= Vector3.right * offset;
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
                left.sprite = normalMoveL;
                break;
            case 1:
                left.sprite = pressedMoveL;
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
                right.sprite = normalMoveR;
                break;
            case 1:
                right.sprite = pressedMoveR;
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

    public void OnPointerDownLeftButton()
    {
        Debug.Log("Left");
    }
    
    public void OnPointerDownRightButton()
    {
        Debug.Log("Right");
    }
    
    public void OnPointerDownJumpButton()
    {
        Debug.Log("Jump");
    }
}
