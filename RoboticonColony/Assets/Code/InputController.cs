using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    /// <exception cref="TimeoutException">the user has taken too long</exception>
    /// <returns>true or false based on the users selection</returns>
    public bool PromptBool(string question, Timeout timeout, string trueOption = "Yes", string falseOption = "No")
    {
        throw new TimeoutException();
    }

    /// <summary>
    /// prompt the user for a number in the given range
    /// </summary>
    /// <param name="question">the question to display the user</param>
    /// <param name="timeout">the time the user has to decide</param>
    /// <param name="min">the minium value the user can choose</param>
    /// <param name="max">the maximum value the user can choose</param>
    /// <param name="cancelable">should the user be able to cancel the prompt</param>
    /// <exception cref="CancelledException">the user cancelled the prompt</exception>
    /// <exception cref="TimeoutException">the user has taken too long</exception>
    /// <returns>the chosen value</returns>
    public int PromptInt(string question, Timeout timeout, int min = 0, int max = int.MaxValue, bool cancelable = false)
    {
        throw new CancelledException();
    }
}
