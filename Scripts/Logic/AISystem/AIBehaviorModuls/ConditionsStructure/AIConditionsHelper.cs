
namespace AISystem
{
    public class AIConditionsHelper
    {
        #region Fields

        #endregion

        #region Propeties

        #endregion

        #region Methods

        public static int CheckConditions(MapNode node, MapNode ownedNode, NodeParent actorParent, AICondition[] conditions)
        {
            int sum = Constants.DEFAULT_VALUE;

            for(int i =0; i< conditions.Length; i++)
            {
                sum += conditions[i].Evaluate(node, ownedNode, actorParent);
            }

            return sum;
        }

        #endregion

        #region Enums



        #endregion
    }
}
