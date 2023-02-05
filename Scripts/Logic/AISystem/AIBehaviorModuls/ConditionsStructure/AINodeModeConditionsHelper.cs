using System.Collections.Generic;

namespace AISystem
{
    public class AINodeModeConditionsHelper
    {
        private static Dictionary<NodeModeType, AINodeModeCondition> modesConditionsCollection = new Dictionary<NodeModeType, AINodeModeCondition>() 
        {
            { NodeModeType.ATTACK, new AINodeModeConditions_Structure.AIAttackModeCondition()},
            { NodeModeType.DEFEND, new AINodeModeConditions_Structure.AIDeffModeCondition()},
            { NodeModeType.PARASITE, new AINodeModeConditions_Structure.AIParasiteModeCondition()},
            { NodeModeType.SECOND_CHANCE, new AINodeModeConditions_Structure.AISecondChanceModeCondition()},
            { NodeModeType.SLOWNING, new AINodeModeConditions_Structure.AISlowModeCondition()},
            { NodeModeType.SPEED, new AINodeModeConditions_Structure.AISpeedModeCondition()},
            { NodeModeType.SUICIDE, new AINodeModeConditions_Structure.AISuicideModeCondition()},
            { NodeModeType.REPRODUCTION, new AINodeModeConditions_Structure.AIReproductionModeCondition()}
        };

        public static AINodeModeCondition GetAINodeModeCondition(NodeModeType modeType)
        {
            AINodeModeCondition condition = null;
            modesConditionsCollection.TryGetValue(modeType, out condition);

            return condition;
        }
    }
}

