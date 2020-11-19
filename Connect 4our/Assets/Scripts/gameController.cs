using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public GameObject[] spaceList;
    public GameObject marker;

    public bool thereIsAWinner;

    public Button leftButton, dropButton, rightButton, player1winButton, player2winButton, restartButton;
    public Text player1winText, player2winText, restartText;
    bool lInteract = true, rInteract = true, dInteract = true;
    float markerPos = 0;
    int colValue;
    int[] corrSpaces = new int[6];
    int[] bottomMark = new int[7] { 5, 5, 5, 5, 5, 5, 5 };
    int dropCount = 1;
    Color corrColor;

    void Start()
    {
        ColUpdate();
        markerPos = 0;

        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        dropButton.gameObject.SetActive(true);

        thereIsAWinner = false;

        leftButton.onClick.AddListener(LeftClick);
        rightButton.onClick.AddListener(RightClick);

        player1winButton.interactable = false;
        player2winButton.interactable = false;
        restartButton.interactable = false;

        player1winButton.GetComponent<Image>().color = Color.clear;
        player2winButton.GetComponent<Image>().color = Color.clear;
        restartButton.GetComponent<Image>().color = Color.clear;

        player1winText.GetComponent<Text>().color = Color.clear;
        player2winText.GetComponent<Text>().color = Color.clear;
        restartText.GetComponent<Text>().color = Color.clear;

        //Debug.Log(bottomMark[colValue]);
    }


    public void Update()
    {
        if (markerPos <= -339)
        {
            lInteract = false;
        }
        else if (markerPos > -339)
        {
            lInteract = true;
        }

        if (markerPos >= 339)
        {
            rInteract = false;
        }
        else if (markerPos < 339)
        {
            rInteract = true;
        }
    }

    public void LeftClick()
    {
        if (markerPos > -339 && lInteract == true)
        {
            lInteract = true;
            marker.transform.position += new Vector3(-56.5f, 0f, 0f);
            markerPos -= 56.5f;
        }
        ColUpdate();
    }

    public void RightClick()
    {
        if (markerPos < 339 && rInteract == true)
        {
            rInteract = true;
            marker.transform.position += new Vector3(56.5f, 0f, 0f);
            markerPos += 56.5f;
        }
        ColUpdate();
    }

    public void DropClick()
    {
        if (spaceList[corrSpaces[bottomMark[colValue]]].GetComponent<Image>().color == Color.white)
        {
            spaceList[corrSpaces[bottomMark[colValue]]].GetComponent<Image>().color = corrColor;
        }

        bottomMark[colValue]--;
        dropCount++;

        ColUpdate();
    }

    void ColUpdate()
    {
        winCheck();

        if (!thereIsAWinner)
        {
            player1winButton.interactable = false;
            player2winButton.interactable = false;
            restartButton.interactable = false;

            player1winButton.GetComponent<Image>().color = Color.clear;
            player2winButton.GetComponent<Image>().color = Color.clear;
            restartButton.GetComponent<Image>().color = Color.clear;

            player1winText.GetComponent<Text>().color = Color.clear;
            player2winText.GetComponent<Text>().color = Color.clear;
            restartText.GetComponent<Text>().color = Color.clear;

            for (int i = 0; i < 7; i++) //determins correct column
            {
                if (markerPos == (113 * i) - 339)
                {
                    colValue = i;
                    break;
                }
            }

            for (int j = 0; j < 6; j++) //every space in the selected column
            {
                corrSpaces[j] = (7 * j) + colValue;
            }

            if (bottomMark[colValue] <= -1) //determines if a column is full or not
            {
                dInteract = false;
                bottomMark[colValue] = -1;
            }
            else
            {
                dInteract = true;
            }

            if (dropCount % 2 == 0) //decides whose turn it is
            {
                corrColor = Color.black;
            }
            else if (dropCount % 2 != 0)
            {
                corrColor = Color.yellow;
            }

            marker.GetComponent<Image>().color = corrColor;
            leftButton.interactable = lInteract;
            rightButton.interactable = rInteract;
            dropButton.interactable = dInteract;

            //Debug.Log(bottomMark[colValue]);
        }

    }

    void winCheck()
    {
        for (int row = 0; row < 6; row++) //checks horizontal win
        {
            for (int col = 0; col < 4; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 1].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 2].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 3].GetComponent<Image>().color == corrColor)
                {
                    winner(corrColor);
                }
            }
        }

        for (int row = 0; row < 3; row++) //checks vertical win
        {
            for (int col = 0; col < 7; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 7].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 14].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 21].GetComponent<Image>().color == corrColor)
                {
                    winner(corrColor);
                }
            }
        }

        for (int row = 0; row < 3; row++) //checks right diagonal win
        {
            for (int col = 0; col < 4; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 8].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 16].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 24].GetComponent<Image>().color == corrColor)
                {
                    winner(corrColor);
                }
            }
        }

        for (int row = 0; row < 3; row++) //checks left diagonal win
        {
            for (int col = 0; col < 4; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 6].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 12].GetComponent<Image>().color == corrColor && spaceList[col + (row * 7) + 18].GetComponent<Image>().color == corrColor)
                {
                    winner(corrColor);
                }
            }
        }
    }
    
    void winner(Color winColor)
    {
        thereIsAWinner = true;

        if (winColor == Color.yellow)
        {
            player1Won();
            player1winText.text = "Winner!";
            Debug.Log("Player 1 wins");
        }
        else if (winColor == Color.black)
        {
            player2Won();
            player2winText.text = "Winner!";
            Debug.Log("Player 2 wins");
        }
        else if (dropCount >= 42)
        {
            player1winText.text = "It's a tie!";
            player2winText.text = "It's a tie!";
            Debug.Log("Both players have tied");
        }
    }

    void player1Won()
    {
        player1winButton.interactable = true;
        restartButton.interactable = true;
        
        restartButton.GetComponent<Image>().color = Color.white;

        player1winText.GetComponent<Text>().color = Color.black;
        restartText.GetComponent<Text>().color = Color.black;

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }

    void player2Won()
    {
        player2winButton.interactable = true;
        restartButton.interactable = true;

        restartButton.GetComponent<Image>().color = Color.white;

        player2winText.GetComponent<Text>().color = Color.black;
        restartText.GetComponent<Text>().color = Color.black;

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(false);
    }
}
