using System;
using UnityEngine;

namespace AISystem
{
    [Serializable]
    public class AIParentSettings : IIDEquatable, IObjectCloneable<AIParentSettings>
    {
        #region Fields

        [SerializeField]
        private int parentId = Constants.DEFAULT_ID;

        // Common Settings
        [SerializeField]
        private float refreshDelayMinS = 0.5f;
        [SerializeField]
        private float refreshDelayMaxS = 1.5f;
        [SerializeField]
        private float targetChargeOverflowPercent = 0f;

        // Attack
        [SerializeField]
        private int checkNeighbourNeutralAIConditionValue = 50;
        [SerializeField]
        private int checkNeighbourOccupatedAIConditionValue = -25;
        [SerializeField]
        private int checkNeighbourParentIsSmallerAIConditionValue = 25;
        [SerializeField]
        private int checkCanReatakeWithAllyAIConditionValue = 75;
        [SerializeField]
        private int checkCanRetakeTargetAIConditionValue = 100;
        [SerializeField]
        private bool checkNestedNodes = false;

        // Deff
        [SerializeField]
        private int checkNodeWithBiggestChargeAIConditionValue = 25;
        [SerializeField]
        private int checkNodeIncreaseAttackPotentialAIConditionValue = 50;

        #endregion

        #region Propeties

        public int ParentId { 
            get => parentId; 
            private set => parentId = value; 
        }

        public int ID => ParentId;

        public float RefreshDelayMinS { 
            get => refreshDelayMinS; 
            private set => refreshDelayMinS = value; 
        }

        public float RefreshDelayMaxS { 
            get => refreshDelayMaxS; 
            private set => refreshDelayMaxS = value; 
        }

        public float TargetChargeOverflowPercent { 
            get => targetChargeOverflowPercent; 
            private set => targetChargeOverflowPercent = value; 
        }

        public bool CheckNestedNodes { 
            get => checkNestedNodes; 
            private set => checkNestedNodes = value; 
        }
        public int CheckNeighbourNeutralAIConditionValue { 
            get => checkNeighbourNeutralAIConditionValue; 
            private set => checkNeighbourNeutralAIConditionValue = value; 
        }
        public int CheckNeighbourOccupatedAIConditionValue { 
            get => checkNeighbourOccupatedAIConditionValue; 
            private set => checkNeighbourOccupatedAIConditionValue = value; 
        }
        public int CheckNeighbourParentIsSmallerAIConditionValue {
            get => checkNeighbourParentIsSmallerAIConditionValue; 
            private set => checkNeighbourParentIsSmallerAIConditionValue = value;
        }
        public int CheckNodeWithBiggestChargeAIConditionValue { 
            get => checkNodeWithBiggestChargeAIConditionValue;
            private set => checkNodeWithBiggestChargeAIConditionValue = value; 
        }
        public int CheckNodeIncreaseAttackPotentialAIConditionValue { 
            get => checkNodeIncreaseAttackPotentialAIConditionValue; 
            private set => checkNodeIncreaseAttackPotentialAIConditionValue = value; 
        }

        public int CheckCanReatakeWithAllyAIConditionValue { 
            get => checkCanReatakeWithAllyAIConditionValue; 
            private set => checkCanReatakeWithAllyAIConditionValue = value; 
        }
        public int CheckCanRetakeTargetAIConditionValue { 
            get => checkCanRetakeTargetAIConditionValue; 
            private set => checkCanRetakeTargetAIConditionValue = value; 
        }

        #endregion

        #region Methods

        public AIParentSettings()
        {

        }

        public AIParentSettings(int parentID)
        {
            ParentId = parentID;
        }

        public AIParentSettings(AIParentSettings source)
        {
            ParentId = source.ParentId;
            RefreshDelayMinS = source.RefreshDelayMinS;
            RefreshDelayMaxS = source.RefreshDelayMaxS;
            TargetChargeOverflowPercent = source.TargetChargeOverflowPercent;
            CheckNestedNodes = source.CheckNestedNodes;
        }

        public bool IDEqual(int otherId)
        {
            return ParentId == otherId;
        }

        public AIParentSettings Clone()
        {
            return new AIParentSettings(this);
        }

        public void SetMaxDelayS(float delayS)
        {
            RefreshDelayMaxS = delayS;
        }

        public void SetMinDelayS(float delayS)
        {
            RefreshDelayMinS = delayS;
        }

        public void SetTargetChargeOverflowPercent(float value)
        {
            TargetChargeOverflowPercent = value;
        }

        public void SetCheckNextedNodes(bool checkNested)
        {
            CheckNestedNodes = checkNested;
        }

        public void SetCheckNeighbourNeutralAIConditionValue(int value)
        {
            CheckNeighbourNeutralAIConditionValue = value;
        }

        public void SetCheckNeighbourOccupatedAIConditionValue(int value)
        {
            CheckNeighbourOccupatedAIConditionValue = value;
        }

        public void SetCheckNeighbourParentIsSmallerAIConditionValue(int value)
        {
            CheckNeighbourParentIsSmallerAIConditionValue = value;
        }

        public void SetCheckNodeWithBiggestChargeAIConditionValue(int value)
        {
            CheckNodeWithBiggestChargeAIConditionValue = value;
        }

        public void SetCheckNodeIncreaseAttackPotentialAIConditionValue(int value)
        {
            CheckNodeIncreaseAttackPotentialAIConditionValue = value;
        }

        #endregion

        #region Enums



        #endregion
    }
}
