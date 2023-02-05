

namespace AISystem
{
    public abstract class AICondition
    {
        #region Fields

        protected int successValue;

        #endregion

        #region Propeties


        #endregion

        #region Methods

        public AICondition(int successValue) => (this.successValue) = successValue;

        protected abstract bool CheckCondition(MapNode targetNode, MapNode ownedNode, NodeParent actorParent);

        public virtual int Evaluate(MapNode targetNode, MapNode ownedNode, NodeParent actorParent)
        {
            return CheckCondition(targetNode, ownedNode, actorParent) == true ? successValue : Constants.DEFAULT_VALUE;
        }

        #endregion

        #region Enums



        #endregion
    }
}
