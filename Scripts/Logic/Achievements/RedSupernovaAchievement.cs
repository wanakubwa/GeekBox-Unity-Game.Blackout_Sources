using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeekBox.Achievements
{
    [Serializable]
    public class RedSupernovaAchievement : AchievementBase
    {
        #region Fields

        #endregion

        #region Propeties

        // Variables.
        private NodeParent PlayerParent { get; set; }
        private int TargetValue { get; set; }

        #endregion

        #region Methods

        public RedSupernovaAchievement(int value)
        {
            TargetValue = value;
        }

        protected override void AttachGameplayEvents()
        {
            base.AttachGameplayEvents();

            PlayerParent = ParentsManager.Instance.ParentsCollection.GetElementByID(Constants.NODE_PLAYER_PARENT_ID);
            if(PlayerParent != null)
            {
                AttachPlayerParentEvents();
                SubstribePlayerNodesEvents();
            }
        }

        protected override void DetachGameplayEvents()
        {
            base.DetachGameplayEvents();

            DetachPlayerParentEvents();
            UnsubscribePlayerNodeEvents();
        }

        private void AttachPlayerParentEvents()
        {
            PlayerParent.OnNodeAdded += OnNodeAddedHandler;
            PlayerParent.OnNodeRemoved += OnNodeRemovedHandler;
        }

        private void DetachPlayerParentEvents()
        {
            PlayerParent.OnNodeAdded -= OnNodeAddedHandler;
            PlayerParent.OnNodeRemoved -= OnNodeRemovedHandler;
        }

        private void SubstribePlayerNodesEvents()
        {
            List<MapNode> playerNodes = PlayerParent.GetMapNodesReferences();
            foreach (MapNode node in playerNodes)
            {
                OnNodeAddedHandler(node);
            }
        }

        private void UnsubscribePlayerNodeEvents()
        {
            List<MapNode> playerNodes = PlayerParent.GetMapNodesReferences();
            foreach (MapNode node in playerNodes)
            {
                OnNodeRemovedHandler(node);
            }
        }

        // Handlers.
        private void OnNodeAddedHandler(MapNode node)
        {
            node.Values.OnValueChanged += OnMapNodeValueChanged;
        }

        private void OnNodeRemovedHandler(MapNode node)
        {
            node.Values.OnValueChanged -= OnMapNodeValueChanged;
        }

        private void OnMapNodeValueChanged(int value)
        {
            if(value >= TargetValue)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_RED_SUPERNOVA);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
