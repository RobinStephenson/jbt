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
        // should also set installed tile to null on the roboticon
        throw new InvalidOperationException("No Roboticon installed");
    }

    /// <summary>
    /// install a roboticon on this tile
    /// </summary>
    /// <exception cref="RoboticonAlreadyInstalledException">A roboticon is already installled</exception>
    /// <param name="newRoboticon">the roboticon being installed</param>
    public void InstallRoboticon(Roboticon newRoboticon)
    {
        // should also set the installedTile on the roboticon to this one
        throw new RoboticonAlreadyInstalledException();
    }
}
