using System;

namespace AISystem
{
    public struct NodeTreeVerticle : IComparable<NodeTreeVerticle>
    {
        #region Fields



        #endregion

        #region Propeties

        public int VerticleValue {
            get;
            private set;
        }

        public MapNode TargetNode {
            get;
            private set;
        }

        public MapConnection SourceConnection {
            get;
            private set;
        }

        #endregion

        #region Methods

        public NodeTreeVerticle(int value, MapNode node, MapConnection connection)
        {
            VerticleValue = value;
            TargetNode = node;
            SourceConnection = connection;
        }

        public int CompareTo(NodeTreeVerticle other)
        {
            return VerticleValue.CompareTo(other.VerticleValue);
        }

        public bool Equals(int other)
        {
            return VerticleValue == other;
        }

        #endregion

        #region Enums



        #endregion
    }
}
