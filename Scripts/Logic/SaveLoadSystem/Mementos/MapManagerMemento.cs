using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapManagerMemento : MementoBase
{
    #region Fields

    [SerializeField]
    private List<MapNode> mapNodesSave = new List<MapNode>();
    [SerializeField]
    private List<MapConnection> mapConnectionsSave = new List<MapConnection>();

    #endregion

    #region Propeties

    public List<MapNode> MapNodesSave
    {
        get => mapNodesSave;
        private set => mapNodesSave = value;
    }

    public List<MapConnection> MapConnectionsSave { 
        get => mapConnectionsSave; 
        private set => mapConnectionsSave = value; 
    }

    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        MapManager manager = sourceManager as MapManager;

        MapNodesSave = manager.MapNodesCollection;
        MapConnectionsSave = manager.MapConnectionsCollection;
    }

    #endregion

    #region Enums



    #endregion
}
