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

    /// <summary>
    /// prompt the user to choose an item from the list
    /// </summary>
    /// <typeparam name="T">The type of the list of items which must inherit AbstractPromptListItem</typeparam>
    /// <param name="question">the questions to display to the user</param>
    /// <param name="timeout">the time the user has to decide</param>
    /// <param name="items">the list of items the user can choose from.</param>
    /// <param name="cancelable">should teh user be able to cancel the prompt</param>
    /// <exception cref="CancelledException">the user cancelled the prompt</exception>
    /// <exception cref="TimeoutException">the user has taken too long</exception>
    /// <returns>the selected item</returns>
    public T PromptList<T>(string question, Timeout timeout, List<T> items, bool cancelable = false) where T: AbstractPromptListItem
    {
        throw new CancelledException();
    }

    /// <summary>
    /// ask the user to confirm they have read a message
    /// </summary>
    /// <param name="message">the message for the user to read</param>
    /// <param name="timeout">how long the user has</param>
    /// <exception cref="TimeoutException">the user has taken too long</exception>
    public void Confirm(string message, Timeout timeout)
    {
        throw new TimeoutException();
    }
}
