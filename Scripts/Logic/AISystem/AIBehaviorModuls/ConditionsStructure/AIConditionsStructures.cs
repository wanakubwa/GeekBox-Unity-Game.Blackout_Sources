using System;
using System.Collections.Generic;

namespace AISystem
{
    #region Attack_Conditions

    public class CheckNudeIsFullAICondition : AICondition
    {
        public CheckNudeIsFullAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            return targetNode.Values.ChargeValue >= targetNode.Values.GetChargeMaxValue();
        }
    }

    public class CheckCanRetakeTargetAICondition : AICondition
    {
        public CheckCanRetakeTargetAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            return targetNode.Values.GetTotalValue() < ownedNode.Values.ChargeValue;
        }
    }

    public class CheckNeighbourNeutralAICondition : AICondition
    {
        public CheckNeighbourNeutralAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            return targetNode.ParentId == Constants.NODE_NEUTRAL_PARENT_ID;
        }
    }

    public class CheckNeighbourOccupatedAICondition : AICondition
    {
        public CheckNeighbourOccupatedAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            return (targetNode.ParentId != Constants.NODE_NEUTRAL_PARENT_ID && targetNode.ParentId != actorParent.ID);
        }
    }

    public class CheckNeighbourParentIsSmallerAICondition : AICondition
    {
        public CheckNeighbourParentIsSmallerAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            return targetNode.CachedParentReference.NodesIdCollection.Count < actorParent.NodesIdCollection.Count;
        }
    }

    public class CheckCanReatakeWithAllyAICondition : AICondition
    {
        public CheckCanReatakeWithAllyAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            bool result = false;

            // Polaczenia wrogie dla mojego wroga moga byc polaczeniami z moja noda.
            List<MapConnection> connectionWithEnemies = targetNode.GetConnectionsWithEnemy();
            foreach (MapConnection enemyConnection in connectionWithEnemies)
            {
                MapNode allyNode = enemyConnection.GetNodeOppositeTo(targetNode.ID);
                if (allyNode.ParentId == actorParent.ID && allyNode.ID != ownedNode.ID)
                {
                    if(allyNode.Values.ChargeValue + ownedNode.Values.ChargeValue > targetNode.Values.GetTotalValue())
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }
    }

    #endregion
    #region Defence_Conditions

    public class CheckNodeWithBiggestChargeAICondition : AICondition
    {
        public CheckNodeWithBiggestChargeAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            int maxNodeChargeValue = 0;
            MapManager mapManager = MapManager.Instance;

            foreach(int nodeID in actorParent.NodesIdCollection)
            {
                MapNode node = mapManager.MapNodesCollection.GetElementByID(nodeID);
                int currentNodeCharge = node.Values.ChargeValue;
                if (maxNodeChargeValue < currentNodeCharge && node.HasOnlyAllyConnections() == true)
                {
                    maxNodeChargeValue = currentNodeCharge;
                }
            }

            return targetNode.Values.ChargeValue >= maxNodeChargeValue;
        }
    }

    public class CheckNodeIncreaseAttackPotentialAICondition : AICondition
    {
        public CheckNodeIncreaseAttackPotentialAICondition(int successValue) : base(successValue)
        {
        }

        protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            bool result = false;
            List<MapConnection> connectionsWithEnemy;
            List<MapConnection> connections = targetNode.GetConnectionsWithAlly();

            // Wszystkie polaczenia musza byc polaczeniami wewnetrznymi - waruenk kwalifikacji nody jako defensywna.
            foreach (MapConnection connection in connections)
            {
                // Drugi koniec polaczenia - noda sojszynicza.
                MapNode oppositeAllyNode = connection.GetNodeOppositeTo(targetNode.ID);
                connectionsWithEnemy = oppositeAllyNode.GetConnectionsWithEnemy();
                foreach (MapConnection withEnemyConnection in connectionsWithEnemy)
                {
                    MapNode enemyNode = withEnemyConnection.GetNodeOppositeTo(oppositeAllyNode.ID);

                    // Jezeli suma ladunku nody sprawdzanej i nody do ktorej moze zostac wyslany ladunek jest wieksza od ladunku przeciwnika.
                    // Zwiekszamy mozliwosc ataku na tego przeciwnika.
                    if (enemyNode.Values.GetTotalValue() < oppositeAllyNode.Values.ChargeValue + targetNode.Values.ChargeValue)
                    {
                        return true;
                    }
                }
            }

            return result;
        }
    }

    #endregion
}
