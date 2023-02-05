using AISystem;
using System;
using System.Collections.Generic;

/// <summary>
/// Mam nadzieje, ze ktos kto bedzie to w przyszlosci czytal znajdzie litosc.
/// </summary>
public class GraphController
{
    #region Fields

    private const int CONDITION_DEFAULT_VALUE = 1;

    #endregion

    #region Propeties

    private static AICondition[] RejectNodeConditions
    {
        get;
        set;
    } = new AICondition[] { new CheckNudeIsFullAICondition(CONDITION_DEFAULT_VALUE) };

    #endregion

    #region Methods


    public static bool TryGetFirstConnectionToFrontNode(MapNode startVerticle, out MapConnection firstConnection)
    {
        List<int> visitedVertices = new List<int>();

        Stack<MapNode> verticesToCheck = new Stack<MapNode>();
        verticesToCheck.Push(startVerticle);

        firstConnection = null;
        while(verticesToCheck.Count > 0)
        {
            MapNode currentVerticle = verticesToCheck.Pop();
            for(int i =0; i < currentVerticle.ConnectionsReference.Count; i++)
            {
                MapConnection currentConnection = currentVerticle.ConnectionsReference[i];
                MapNode oppositeNode = currentVerticle.ConnectionsReference[i].GetNodeOppositeTo(currentVerticle.ID);

                // Wierzcholek docelowy pierwszego polaczenia nie moze zostac odrzucony przez warunki.
                if (currentConnection.IsContainsNodeId(startVerticle.ID) == true
                    && AIConditionsHelper.CheckConditions(oppositeNode, currentVerticle, startVerticle.CachedParentReference, RejectNodeConditions) == Constants.DEFAULT_VALUE)
                {
                    firstConnection = currentConnection;
                }

                // Wierzcholek nie byl odwiedzony i nalezy do aktualnego parenta AI.
                if (visitedVertices.Contains(oppositeNode.ID) == false && oppositeNode.ParentId == startVerticle.ParentId)
                {
                    // Wierzcholek posiada polaczenia z przeciwnikiem lub neutralne oraz jest wierzcholkiem parenta nody sprawdzanej.
                    if (oppositeNode.HasOnlyAllyConnections() == false && firstConnection != null)
                    {
                        return true;
                    }

                    verticesToCheck.Push(oppositeNode);
                }
            }

            visitedVertices.Add(currentVerticle.ID);
        }

        return false;
    }

    #endregion

    #region Enums



    #endregion
}
