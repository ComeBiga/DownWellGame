using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMan : MonoBehaviour
{
    [SerializeField]
    int charMoveSpeed = 20;
    public GameObject secondChar;

    [Header("Player")]
    public GameObject character;

    [Header("Buttons")]
    public GameObject rbtn;
    public GameObject lbtn;
    public GameObject selbtn;

    bool moveR;
    bool moveL;

    int moveX = 0;
    int distanceNextObj;

    public static int charNum = 0;

    void Awake()
    {
        //character = GameObject.Find("character");
        //rbtn = GameObject.Find("rButton");
        //lbtn = GameObject.Find("lButton");

        charNum = 0;
        distanceNextObj = (int)secondChar.transform.localPosition.x;
    }

    void Update()
    {
        btnVisible();
        if (moveR || moveL)
        {
            rbtn.GetComponent<Button>().enabled = false;
            lbtn.GetComponent<Button>().enabled = false;
        }
        else
        {
            rbtn.GetComponent<Button>().enabled = true;
            lbtn.GetComponent<Button>().enabled = true;
        }

        if (moveX >= distanceNextObj)
        {
            moveR = false;
            moveL = false;
            moveX = 0;
        }

        if (moveR)
        {
            moveX += charMoveSpeed;
            character.transform.localPosition -= new Vector3(charMoveSpeed, 0, 0);
        }
        else if (moveL)
        {
            moveX += charMoveSpeed;
            character.transform.localPosition += new Vector3(charMoveSpeed, 0, 0);
        }
    }

    public void selectbtn()
    {
        PlayerManager.instance.SelectPlayerCharacter(charNum);
        SoundManager.instance.PlayBGMSound("base");  //사운드 시작
        //SceneManager.LoadScene(1);

        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(1);
        //asyncOper.allowSceneActivation = true;
    }

    public void arrowbtn(string arrow)
    {
        switch (arrow)
        {
            case "right":
                moveR = true;
                charNum += 1;
                break;
            case "left":
                moveL = true;
                charNum -= 1;
                break;
        }
    }

    void btnVisible()
    {
        if (charNum == 0)
        {
            lbtn.SetActive(false);
            selbtn.SetActive(true);
        }
        else if (charNum == 1)
        {
            rbtn.SetActive(true);
            lbtn.SetActive(true);
            selbtn.SetActive(false);
        }
        else if (charNum == 2)
        {
            rbtn.SetActive(false);
            selbtn.SetActive(false);
        }
    }

}
