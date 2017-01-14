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
    /// <returns>true or false based on the users selection, or null if the user timesout </returns>
    public bool? Prompt(string question, Timeout timeout, string trueOption = "Yes", string falseOption = "No")
    {
        return null;
    }
}
