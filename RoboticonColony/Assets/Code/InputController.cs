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
    public bool PromptBool(string question, Timeout timeout = null, string trueOption = "Yes", string falseOption = "No")
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
    public int PromptInt(string question, Timeout timeout = null, int min = 0, int max = int.MaxValue, bool cancelable = false)
    {
        throw new CancelledException();
    }

    /// <summary>
    /// prompt the user to choose an item from the list
    /// </summary>
    /// <typeparam name="T">The type of the list of items which must inherit AbstractPromptListItem</typeparam>
    /// <param name="question">the questions to display to the user</param>
    /// <param name="items">the list of items the user can choose from.</param>
    /// <param name="timeout">the time the user has to decide</param>
    /// <param name="cancelable">should teh user be able to cancel the prompt</param>
    /// <exception cref="CancelledException">the user cancelled the prompt</exception>
    /// <exception cref="TimeoutException">the user has taken too long</exception>
    /// <returns>the selected item</returns>
    public T PromptList<T>(string question, List<T> items, Timeout timeout = null, bool cancelable = false) where T: IPromptListItem
    {
        throw new CancelledException();
    }

    /// <summary>
    /// ask the user to confirm they have read a message
    /// </summary>
    /// <param name="message">the message for the user to read</param>
    /// <param name="timeout">how long the user has</param>
    /// <exception cref="TimeoutException">the user has taken too long</exception>
    public void Confirm(string message, Timeout timeout = null)
    {
        throw new TimeoutException();
    }

    /// <summary>
    /// prompt the user to select a tile
    /// </summary>
    /// <param name="timeout" >a timeout for the user to choose in</param>
    /// <param name="cancelable">should the user be able to cancel the prompt</param>
    /// <exception cref="CancelledException">thrown if the user cancels</exception>
    /// <exception cref="TimeoutException">thrown if the user runs out of time</exception>
    /// <returns>the chosen tile or null if the user times out </returns>
    public Tile ChooseTile(Timeout timeout = null, bool cancelable = false)
    {
        return null;
    }
}
