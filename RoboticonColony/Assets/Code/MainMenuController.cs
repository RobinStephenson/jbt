using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public static string PlayerOneName;
    public static string PlayerTwoName;

    public Text PlayerOneNameText;
    public Text PlayerTwoNameText;

    public void StartGameClicked()
    {
        if (PlayerOneNameText.text.Length == 0)
        {
            PlayerOneName = "Player One";
        }
        else
        {
            PlayerOneName = PlayerOneNameText.text;
        }

        if (PlayerTwoNameText.text.Length == 0)
        {
            PlayerTwoName = "Player Two";
        }
        else
        {
            PlayerTwoName = PlayerTwoNameText.text;
        }

        SceneManager.LoadScene(1);
    }
}
