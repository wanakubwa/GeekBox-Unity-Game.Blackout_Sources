
namespace AISystem
{
    public abstract class AINodeModeCondition
    {
        #region Fields

        private const float PRICE_MULTIPLY_FACTOR = 1.5f;

        #endregion

        #region Propeties

        public abstract NodeModeType ModeType { get; }

        protected AICondition[] Conditions { 
            get;
            set;
        }

        #endregion

        #region Methods

        public AINodeModeCondition()
        {
            InitializeConditions();
        }

        protected abstract void InitializeConditions();

        public virtual int GetNodePotential(MapNode node, NodeParent parent)
        {
            int potential = Constants.DEFAULT_VALUE;
            foreach (AICondition condition in Conditions)
            {
                potential += condition.Evaluate(null, node, parent);
            }

            return potential;
        }

        public virtual bool CanBuyMode(MapNode node, int cost)
        {
            return cost <= node.Values.ChargeValue * PRICE_MULTIPLY_FACTOR;
        }

        #endregion

        #region Enums



        #endregion
    }
}
