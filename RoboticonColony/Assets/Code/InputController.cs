using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// prompt the user with a boolean question
    /// </summary>
    /// <param name="question">the question to display</param>
    /// <param name="trueOption">the option to display which will return true when selected</param>
    /// <param name="falseOption">the option to display which will return false when selected</param>
    /// <param name="timeout">the time the user has to decide</param>
    /// <returns>true or false based on the users selection, or null if the user times out</returns>
    public bool? PromptBool(string question, Timeout timeout, string trueOption = "Yes", string falseOption = "No")
    {
        return null;
    }

    /// <summary>
    /// prompt the user for a number in the given range
    /// </summary>
    /// <param name="question">the question to display the user</param>
    /// <param name="timeout">the time the user has to decide</param>
    /// <param name="min">the minium value the user can choose</param>
    /// <param name="max">the maximum value the user can choose</param>
    /// <param name="cancelable">should the user be able to cancel the prompt</param>
    /// <returns>the chosen value, or null if the user times out or cancels</returns>
    public int? PromptInt(string question, Timeout timeout, int min = 0, int max = int.MaxValue, bool cancelable = false)
    {
        return null;
    }
}
