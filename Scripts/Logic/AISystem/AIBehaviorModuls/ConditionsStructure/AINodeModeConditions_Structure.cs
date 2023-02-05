using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISystem
{
    public class AINodeModeConditions_Structure
    {

        #region NodeModeTypes

        public class AIAttackModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.ATTACK;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasMultiplyOwnConnectionsAICondition(30),
                    new CheckHasOnlyAllyConnectionsAICondition(30)
                };
            }
        }

        public class AIDeffModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.DEFEND;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasMultiplyEnemyConnectionsAICondition(50),
                    new CheckHasMultiplyOwnConnectionsAICondition(25)
                };
            }
        }


        public class AIParasiteModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.PARASITE;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasMultiplyEnemyConnectionsAICondition(50)
                };
            }
        }

        public class AISecondChanceModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.SECOND_CHANCE;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasOnlyAllyConnectionsAICondition(60),
                    new CheckHasCurrentlyModeAICondition(-25),
                    new CheckHasMultiplyOwnConnectionsAICondition(25)
                };
            }
        }

        public class AISlowModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.SLOWNING;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasMultiplyEnemyConnectionsAICondition(55),
                    new CheckHasCurrentlyModeAICondition(-50)
                };
            }
        }

        public class AISpeedModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.SPEED;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasMultiplyEnemyConnectionsAICondition(30),
                    new CheckHasMultiplyOwnConnectionsAICondition(30),
                    new CheckHasCurrentlyModeAICondition(-50)
                };
            }
        }

        public class AISuicideModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.SUICIDE;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasOnlyAllyConnectionsAICondition(30),
                    new CheckCanRetakeMoreThanOneNodeAICondition(60)
                };
            }
        }

        public class AIReproductionModeCondition : AINodeModeCondition
        {
            public override NodeModeType ModeType => NodeModeType.REPRODUCTION;

            protected override void InitializeConditions()
            {
                Conditions = new AICondition[] {
                    new CheckHasOnlyAllyConnectionsAICondition(55)
                };
            }
        }

        #endregion

        #region Conditions

        /// <summary>
        /// Czy sprawdzana noda posiada wiecej niz 2 polaczenia sojusznicze.
        /// </summary>
        public class CheckHasMultiplyOwnConnectionsAICondition : AICondition
        {
            public CheckHasMultiplyOwnConnectionsAICondition(int successValue) : base(successValue)
            {
            }

            protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
            {
                int counter = 0;

                foreach (MapConnection connection in ownedNode.ConnectionsReference)
                {
                    if(connection.IsConnectBewtweenSameParent() == true)
                    {
                        counter++;
                    }
                }

                return counter > 2;
            }
        }

        /// <summary>
        /// Czy noda posiada wiecej niz 1 polaczenie wrogie.
        /// </summary>
        public class CheckHasMultiplyEnemyConnectionsAICondition : AICondition
        {
            public CheckHasMultiplyEnemyConnectionsAICondition(int successValue) : base(successValue)
            {
            }

            protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
            {
                int counter = 0;

                foreach (MapConnection connection in ownedNode.ConnectionsReference)
                {
                    if (connection.IsConnectBewtweenSameParent() == false)
                    {
                        counter++;
                    }
                }

                return counter > 2;
            }
        }


        /// <summary>
        /// Czy noda posiada tylko sojusznicze polaczenia.
        /// </summary>
        public class CheckHasOnlyAllyConnectionsAICondition : AICondition
        {
            public CheckHasOnlyAllyConnectionsAICondition(int successValue) : base(successValue)
            {
            }

            protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
            {
                int counter = 0;

                foreach (MapConnection connection in ownedNode.ConnectionsReference)
                {
                    if (connection.IsConnectBewtweenSameParent() == false)
                    {
                        counter++;
                        break;
                    }
                }

                return !(counter > 0);
            }
        }

        /// <summary>
        /// Czy noda swoim ladunkiem moze przejac wiecej niz jedna node.
        /// </summary>
        public class CheckCanRetakeMoreThanOneNodeAICondition : AICondition
        {
            public CheckCanRetakeMoreThanOneNodeAICondition(int successValue) : base(successValue)
            {
            }

            protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
            {
                int counter = 0;
                List<MapNode> otherMapNodes = ParentsManager.Instance.GetAllParentsNodesExceptNeutral(actorParent.ID);

                int currentNodeCharge = ownedNode.Values.ChargeValue;
                int chargeToSubstract = currentNodeCharge / otherMapNodes.Count;
                foreach (MapNode otherNode in otherMapNodes)
                {
                    if(otherNode.Values.GetTotalValue() <= chargeToSubstract)
                    {
                        counter++;
                    }
                }

                return counter > 1;
            }
        }

        /// <summary>
        /// Czy noda swoim ladunkiem moze przejac wiecej niz jedna node.
        /// </summary>
        public class CheckHasCurrentlyModeAICondition : AICondition
        {
            public CheckHasCurrentlyModeAICondition(int successValue) : base(successValue)
            {
            }

            protected override bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
            {
                return ownedNode.Values.ProfileModeType != NodeModeType.NORMAL && ownedNode.Values.ProfileModeType != NodeModeType.DEFAULT;
            }
        }

        #endregion
    }
}
