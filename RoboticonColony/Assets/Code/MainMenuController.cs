using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public static string PlayerOneName = "Player 1";
    public static string PlayerTwoName = "Player 2";

    public Text PlayerOneNameText;
    public Text PlayerTwoNameText;

    public void StartGameClicked()
    {
        if (PlayerOneNameText.text.Length == 0)
        {
            PlayerOneName = "Player 1";
        }
        else
        {
            PlayerOneName = PlayerOneNameText.text;
        }

        if (PlayerTwoNameText.text.Length == 0)
        {
            PlayerTwoName = "Player 2";
        }
        else
        {
            PlayerTwoName = PlayerTwoNameText.text;
        }

        SceneManager.LoadScene(1);
    }
}
