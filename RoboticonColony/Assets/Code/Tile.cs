using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour {

    // A placeholder
    public AbstractPlayer Owner;
    public Roboticon InstalledRoboticon;

    /// <summary>
    /// remove the roboticon installed on this tile
    /// </summary>
    /// <exception cref="InvalidOperationException">there is no roboticon installed</exception>
    /// <returns>the removed roboticon</returns>
    public Roboticon RemoveRoboticon()
    {
        throw new InvalidOperationException("No Roboticon installed");
    }
}
