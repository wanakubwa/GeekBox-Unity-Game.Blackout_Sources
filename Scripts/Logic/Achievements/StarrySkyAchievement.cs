using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class StarrySkyAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TargetlvlIndex { get; set;}
        private int MaxNodesCount { get; set; }
        private NodeParent PlayerParent { get; set; }

        #endregion

        #region Methods

        public StarrySkyAchievement(int targetlvlIndex)
        {
            TargetlvlIndex = targetlvlIndex;
        }

        protected override void AttachGameplayEvents()
        {
            base.AttachGameplayEvents();

            if(ScenariosManager.Instance.CurrentScenarioInfo.OrderNo == TargetlvlIndex)
            {
                // hack.
                MaxNodesCount = MapManager.Instance.MapNodesCollection.Count;
                PlayerParent = ParentsManager.Instance.ParentsCollection.GetElementByID(Constants.NODE_PLAYER_PARENT_ID);
                if(PlayerParent != null)
                {
                    PlayerParent.OnNodeAdded += OnNodeAdded;
                }
            }
        }

        protected override void DetachGameplayEvents()
        {
            base.DetachGameplayEvents();

            if (PlayerParent != null)
            {
                PlayerParent.OnNodeAdded -= OnNodeAdded;
            }
        }

        private void CheckAchievement(int currentNodes)
        {
            if(MaxNodesCount >= currentNodes)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_STARRY_SKY);
            }
        }

        // Handlers.
        private void OnNodeAdded(MapNode n)
        {
            CheckAchievement(PlayerParent.NodesIdCollection.Count);
        }

        #endregion

        #region Enums



        #endregion
    }
}
