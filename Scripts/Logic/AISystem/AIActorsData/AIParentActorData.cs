using System;

namespace AISystem
{
    [Serializable]
    class AIParentActorData : IIDEquatable
    {
        #region Fields

        #endregion

        #region Propeties

        public NodeParent ParentActor
        {
            get;
            private set;
        }

        public ParentAIBehaviorModul BehaviorModul
        {
            get;
            private set;
        }

        public int ID => ParentActor.ID;

        #endregion

        #region Methods

        public AIParentActorData(NodeParent actor, AIParentSettings settings)
        {
            ParentActor = actor;
            BehaviorModul = new ParentAIBehaviorModul(settings, actor);
        }

        public void TryRefreshModul(float deltaTimeS)
        {
            if(BehaviorModul.CanRefresh(deltaTimeS) == true)
            {
                BehaviorModul.RefreshModul();
            }
        }

        public bool IDEqual(int otherId)
        {
            return otherId == ID;
        }

        #endregion

        #region Enums



        #endregion
    }
}
