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
    /// <param name="timeout">if the user times out the callback wont be called</param>
    /// <param name="cancelable">if the user cancels the callback will be called with null</param>
    public void PromptBool(string question, Action<bool?> callback, Timeout timeout = null, string trueOption = "Yes", string falseOption = "No", bool cancelable = false)
    {
    }

    /// <summary>
    /// prompt the user for a number in the given range
    /// </summary>
    /// <param name="question">the question to display the user</param>
    /// <param name="timeout">the time the user has to decide</param>
    /// <param name="min">the minium value the user can choose</param>
    /// <param name="max">the maximum value the user can choose</param>
    /// <param name="timeout">if the user times out the callback wont be called</param>
    /// <param name="cancelable">if the user cancels the callback will be called with null</param>
    public void PromptInt(string question, Action<int?> callback, Timeout timeout = null, int min = 0, int max = int.MaxValue, bool cancelable = false)
    {
    }

    /// <summary>
    /// prompt the user to choose an item from the list
    /// </summary>
    /// <typeparam name="T">The type of the list of items which must inherit AbstractPromptListItem</typeparam>
    /// <param name="question">the questions to display to the user</param>
    /// <param name="items">the list of items the user can choose from.</param>
    /// <param name="timeout">if the user times out the callback wont be called</param>
    /// <param name="cancelable">if the user cancels the callback will be called with null</param>
    public void PromptList<T>(string question, Action<T> callback, List<T> items, Timeout timeout = null, bool cancelable = false) where T: IPromptListItem
    {
    }

    /// <summary>
    /// ask the user to confirm they have read a message
    /// </summary>
    /// <param name="message">the message for the user to read</param>
    /// <param name="timeout">if the user times out the callback wont be called</param>
    public void Confirm(string message, Action callback, Timeout timeout = null)
    {
    }

    /// <summary>
    /// prompt the user to select a tile
    /// </summary>
    /// <param name="callback">call this with the chosen tile as the paramater</param>
    /// <param name="timeout" >if the user times out the callback wont be called</param>
    /// <param name="cancelable">if the user cancels the callback will be called with null</param>
    public void ChooseTile(Action<Tile> callback, Timeout timeout = null, bool cancelable = false)
    {
    }
}
