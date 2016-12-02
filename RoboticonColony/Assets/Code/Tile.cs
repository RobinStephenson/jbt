using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
    //private Resources _resources;
    //private Roboticon _installedRoboticon;
    private int _cost;
    private int _ownerID;

    /// <summary>
        /// Constructor for tile with provided parameters
        /// </summary>
        /// <param name="resources">Resources that the tile has</param>
        /// <param name="roboticon">Roboticon on the tile, null if no roboticon</param>
        /// <param name="cost">Cost of the tile</param>
        /// <param name="owner">Owner of the tile</param>
    //public Tile(Resources resources, Roboticon roboticon, int cost, Player owner)
    //{
    //    _resources = resources;
    //    _installedRoboticon = roboticon;
    //    _cost = cost;
    //    _ownerID = owner.PlayerID;
    //}

    /// <summary>
        /// Constructor for tile with no owner or roboticon
        /// </summary>
        /// <param name="resource">Resources that the tile has</param>
        /// <param name="cost">Cost of the tile</param>
    //public Tile(Resources resource, int cost)
    //{
    //    _resources = resource;
    //    _cost = cost;
    //    //_installedRoboticon = null;
    //    _ownerID = -1;
    //}

    /// <summary>
        /// Adds a roboticon to the tile
        /// </summary>
        /// <param name="newRoboticon">Roboticon to be added</param>
    //public void addRoboticon(Roboticon newRoboticon)
    //{
    //    _installedRoboticon = newRoboticon;
    //}

    /// <summary>
        /// Removes the roboticon from the tile
        /// </summary>
    //public void removeRoboticon()
    //{
    //    _installedRoboticon = null;
    //}

    /// <summary>
        /// Gets the cost of the tile
        /// </summary>
        /// <returns></returns>
    public int getCost()
    {
        return _cost;
    }

    //public void produce()
  
    //}

    //public Resources TileResources
    //{
    //    get { return _resources; }
    //}

    //public Roboticon InstalledRoboticon
    //{
    //    get { return _installedRoboticon; }
    //    set { _installedRoboticon = value; }
    //}

    public int Cost
    {
        get { return _cost; }
    }

    public int OwnerID
    {
        get { return _ownerID; }
        set { _ownerID = value; }
    }
}
