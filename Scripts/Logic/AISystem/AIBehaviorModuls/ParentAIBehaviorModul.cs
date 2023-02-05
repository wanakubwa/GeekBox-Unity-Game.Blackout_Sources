using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AISystem
{
    [Serializable]
    class ParentAIBehaviorModul
    {
        #region Fields

        #endregion

        #region Propeties

        public AIParentSettings Settings
        {
            get;
            private set;
        }

        public NodeParent ParentActor
        {
            get; 
            private set;
        }

        // Variables.

        private float TimeDelayS
        {
            get;
            set;
        } = Constants.DEFAULT_VALUE;

        private float CurrentTimeCounterS { 
            get; 
            set; 
        } = Constants.DEFAULT_VALUE;

        private AICondition[] AttackConditionsCollection { 
            get;
            set;
        }

        private AICondition[] DefensiveConditionsCollection {
            get;
            set;
        }

        private List<AINodeModeCondition> NodeModeConditions {
            get;
            set;
        } = new List<AINodeModeCondition>();

        private MapNode MarkedNode {
            get;
            set;
        } = null;

        private NodeModeType MarkedType {
            get;
            set;
        } = NodeModeType.NORMAL;

        #endregion

        #region Methods

        public ParentAIBehaviorModul()
        {

        }

        public ParentAIBehaviorModul(AIParentSettings settings, NodeParent parent)
        {
            Settings = settings;
            ParentActor = parent;

            Initialize();
        }

        public void Initialize()
        {
            TimeDelayS = RandomMath.RandomRangeUnity(Settings.RefreshDelayMinS, Settings.RefreshDelayMaxS);

            // Building conditions collections.
            AttackConditionsCollection = new AICondition[]
            {
                new CheckNeighbourNeutralAICondition(Settings.CheckNeighbourNeutralAIConditionValue),
                new CheckNeighbourOccupatedAICondition(Settings.CheckNeighbourOccupatedAIConditionValue),
                new CheckNeighbourParentIsSmallerAICondition(Settings.CheckNeighbourParentIsSmallerAIConditionValue),
                new CheckCanReatakeWithAllyAICondition(Settings.CheckCanReatakeWithAllyAIConditionValue),
                new CheckCanRetakeTargetAICondition(Settings.CheckCanRetakeTargetAIConditionValue)
            };

            DefensiveConditionsCollection = new AICondition[]
            {
                new CheckNodeWithBiggestChargeAICondition(Settings.CheckNodeWithBiggestChargeAIConditionValue),
                new CheckNodeIncreaseAttackPotentialAICondition(Settings.CheckNodeIncreaseAttackPotentialAIConditionValue)
            };

            // Inicjalizacja warunkow dla dostepnych trybow nodek.
            AINodeModeCondition modeCondition = null;
            foreach (NodeModeType modeType in ParentActor.ModifiersValues.AvailableModes)
            {
                modeCondition = AINodeModeConditionsHelper.GetAINodeModeCondition(modeType);
                if(modeCondition != null)
                {
                    NodeModeConditions.Add(modeCondition);
                }
            }
        }

        public bool CanRefresh(float deltaTimeS)
        {
            CurrentTimeCounterS += deltaTimeS;
            if(CurrentTimeCounterS >= TimeDelayS)
            {
                return true;
            }

            return false;
        }

        public void RefreshModul()
        {
            CurrentTimeCounterS = Constants.DEFAULT_VALUE;
            Debug.LogFormat("[AI] Sprawdzanie rodzica id: {0}", ParentActor.ID);

            if(TryMakeAttackAction() == false)
            {
                TryMakeDefenceAction();
            }
        }

        private bool TryMakeAttackAction()
        {
            MapConnection connection = GetConnectionForAttack();
            if(connection == null)
            {
                return false;
            }

            MapNode ownNode = connection.GetNodeOwnedByParent(ParentActor.ID);
            MapManager.Instance.SendChargeBetweenNodes(ownNode, connection.GetNodeOppositeTo(ownNode.NodeId));
            return true;
        }

        private void TryMakeDefenceAction()
        {
            List<MapNode> parentNodes = GetParentMapNodes();

            // W pierwszej kolejnosci sprawdzamy czy nie jest mozliwe oznaczenie nody jako potencjal kupna upgradu.
            if(TryMarkNode(parentNodes) == true)
            {
                MapConnection connection = GetConnectionToMarkedNode();
                if(connection != null)
                {
                    MapNode ownNode = connection.GetNodeOppositeTo(MarkedNode.ID);
                    MapManager.Instance.SendChargeBetweenNodes(ownNode, MarkedNode);
                }

                TryBuyUpgrade();
            }

            MapNode senderNode = GetNodeCandidateToDefense(parentNodes);
            if (senderNode == null)
            {
                return;
            }

            MapConnection connectionToFront;
            if (GraphController.TryGetFirstConnectionToFrontNode(senderNode, out connectionToFront) == true)
            {
                MapManager.Instance.SendChargeBetweenNodes(senderNode, connectionToFront.GetNodeOppositeTo(senderNode.NodeId));
            }
        }

        private void TryBuyUpgrade()
        {
            if(MarkedNode == null || MarkedType == NodeModeType.NORMAL)
            {
                return;
            }

            NodeProfileBase nodeProfile = NodeContentSettings.Instance.GetNodeProfileByModeType(MarkedType);

            if (MarkedNode.Values.ChargeValue >= nodeProfile.ProfileCost.ChargeCost && MarkedNode.Values.ProfileModeType != MarkedType)
            {
                MarkedNode.SetNodeModeProfile(nodeProfile);
                MarkedNode.Values.SubstractCharge(nodeProfile.ProfileCost.ChargeCost);
            }
        }

        private bool TryMarkNode(List<MapNode> parentNodes)
        {
            MarkedNode = null;
            MarkedType = NodeModeType.NORMAL;

            if (NodeModeConditions.Count < 1)
            {
                return false;
            }

            int maxPotential = Constants.DEFAULT_VALUE;
            NodeModeType selectedType = NodeModeType.NORMAL;
            MapNode selecteNode = null;

            for(int i =0; i < parentNodes.Count; i++)
            {
                for(int j =0; j < NodeModeConditions.Count; j++)
                {
                    if(maxPotential < NodeModeConditions[j].GetNodePotential(parentNodes[i], ParentActor))
                    {
                        selectedType = NodeModeConditions[j].ModeType;
                        selecteNode = parentNodes[i];
                    }
                }
            }

            if(maxPotential >= Constants.AI_MODE_POTENTIAL_THRESHOLD)
            {
                MarkedNode = selecteNode;
                MarkedType = selectedType;
                return true;
            }

            return false;
        }

        private MapConnection GetConnectionForAttack()
        {
            List<MapNode> nodes = GetParentMapNodes();
            HashSet<MapConnection> connectionsWithEnemy = new HashSet<MapConnection>();

            foreach (MapNode node in nodes)
            {
                connectionsWithEnemy.AddRange(node.GetConnectionsWithEnemy());
            }

            MapNode ownNode, enemyNode;
            NodeTreeVerticle lastVerticle;
            int evaluatedValue;

            // Ta struktura przechowuje informacja jaka noda posiada ta wartosc i z jakiego polaczenia wychodzi.
            List<NodeTreeVerticle> evaluatedEnemyNodes = new List<NodeTreeVerticle>();

            foreach (MapConnection connection in connectionsWithEnemy)
            {
                ownNode = connection.GetNodeOwnedByParent(ParentActor.ID);
                enemyNode = connection.GetNodeOppositeTo(ownNode.NodeId);
                evaluatedValue = AIConditionsHelper.CheckConditions(enemyNode, ownNode, ParentActor, AttackConditionsCollection);
                lastVerticle = new NodeTreeVerticle(evaluatedValue, enemyNode, connection);
                evaluatedEnemyNodes.Add(lastVerticle);
            }

            // Sortowanie rosnaco.
            evaluatedEnemyNodes.Sort();

            MapConnection selectedConnection = null;
            int enemyNodeValue;
            for ( int i = evaluatedEnemyNodes.Count - 1; i >= 0; i--)
            {   
                enemyNodeValue = evaluatedEnemyNodes[i].TargetNode.Values.GetTotalValue();
                ownNode = evaluatedEnemyNodes[i].SourceConnection.GetNodeOppositeTo(evaluatedEnemyNodes[i].TargetNode.ID);

                //TMP !!!!! - usunac po demie testowym.
                CheckCanReatakeWithAllyAICondition tmpCondition = new CheckCanReatakeWithAllyAICondition(25);

                if (ownNode.Values.ChargeValue > (enemyNodeValue + enemyNodeValue * Settings.TargetChargeOverflowPercent) || tmpCondition.Evaluate(evaluatedEnemyNodes[i].TargetNode, ownNode, ParentActor) != Constants.DEFAULT_VALUE)
                {
                    selectedConnection = evaluatedEnemyNodes[i].SourceConnection;
                    break;
                }
            }

            return selectedConnection;
        }

        private MapNode GetNodeCandidateToDefense(List<MapNode> parentNodes)
        {
            List<MapNode> nodesWithAllyConnections = new List<MapNode>();

            foreach (MapNode node in parentNodes)
            {
                if(node.HasOnlyAllyOrNeutralConnections() == true)
                {
                    nodesWithAllyConnections.Add(node);
                }
            }

            // Ta struktura przechowuje informacja jaka noda posiada ta wartosc i z jakiego polaczenia wychodzi.
            List<NodeTreeVerticle> evaluatedEnemyNodes = new List<NodeTreeVerticle>();
            int evaluatedValue;

            foreach (MapNode innerNode in nodesWithAllyConnections)
            {
                evaluatedValue = AIConditionsHelper.CheckConditions(innerNode, null, ParentActor, DefensiveConditionsCollection);
                evaluatedEnemyNodes.Add(new NodeTreeVerticle(evaluatedValue, innerNode, null));
            }

            // Sortowanie rosnaco.
            evaluatedEnemyNodes.Sort();

            NodeTreeVerticle verticle = evaluatedEnemyNodes.LastOrDefault();

            // Jezeli wybrana noda posiada jakas wartosc wyboru.
            return verticle.VerticleValue > 0 ? verticle.TargetNode : null;
        }

        private MapConnection GetConnectionToMarkedNode()
        {
            NodeProfileBase selectedProfile = NodeContentSettings.Instance.GetNodeProfileByModeType(MarkedType);

            MapConnection selectedConnection = null;
            List<MapConnection> nodesWithAllyConnections = new List<MapConnection>();

            // Inicjalizacja kolekcji sasiadujacych nodek.
            MapNode oppositeNode = null;
            foreach (MapConnection connection in MarkedNode.ConnectionsReference)
            {
                oppositeNode = connection.GetNodeOppositeTo(MarkedNode.ID);
                if (oppositeNode.ParentId == ParentActor.ID)
                {
                    if(oppositeNode.Values.ChargeValue + MarkedNode.Values.ChargeValue > selectedProfile.ProfileCost.ChargeCost)
                    {
                        selectedConnection = connection;
                    }
                }
            }

            // Jezeli wybrana noda posiada jakas wartosc wyboru.
            return selectedConnection;
        }


        private List<MapNode> GetParentMapNodes()
        {
            List<int> nodesIds = ParentActor.NodesIdCollection;
            List<MapNode> nodes = new List<MapNode>();

            foreach (int id in nodesIds)
            {
                nodes.Add(MapManager.Instance.MapNodesCollection.GetElementByID(id));
            }

            return nodes;
        }

        #endregion

        #region Enums

        //public class AITreeVerticle : IComparable<AITreeVerticle>
        //{
        //    #region Fields



        //    #endregion

        //    #region Propeties

        //    public int VerticleValue {
        //        get;
        //        private set;
        //    }

        //    public MapNode TargetNode {
        //        get;
        //        private set;
        //    }

        //    public MapConnection SourceConnection {
        //        get;
        //        private set;
        //    }

        //    #endregion

        //    #region Methods

        //    public AITreeVerticle(int verticleValue, MapNode targetNode, MapConnection sourceConnection)
        //    {
        //        VerticleValue = verticleValue;
        //        TargetNode = targetNode;
        //        SourceConnection = sourceConnection;
        //    }

        //    public void AddValue(int value)
        //    {
        //        VerticleValue += value;
        //    }

        //    public int CompareTo(AITreeVerticle other)
        //    {
        //        return VerticleValue.CompareTo(other.VerticleValue);
        //    }

        //    public bool Equals(int other)
        //    {
        //        return VerticleValue == other;
        //    }

        //    #endregion

        //    #region Enums



        //    #endregion
        //}

        #endregion
    }
}
