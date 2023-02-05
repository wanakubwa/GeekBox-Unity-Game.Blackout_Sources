using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UpgradesGUI
{
    public class UpgradeVisualizationElement : SizeButton, ICleanable
    {
        #region Fields

        [SerializeField]
        private Image iconImage;
        [SerializeField]
        private Image backgroundImage;

        [Header("State Settings")]
        [SerializeField]
        private Sprite lockedIcon;
        [SerializeField]
        private Sprite blackBacgroundSprite;
        [SerializeField]
        private Sprite whiteBackgroundSprite;

        #endregion

        #region Propeties

        public UpgradesTreePanelView ParentReference
        {
            get;
            private set;
        }

        public UpgradeNode CurrentUpgradeNode
        {
            get;
            private set;
        }

        public Sprite LockedIcon
        {
            get => lockedIcon;
        }

        public Sprite BlackBacgroundSprite
        {
            get => blackBacgroundSprite;
        }

        public Sprite WhiteBackgroundSprite
        {
            get => whiteBackgroundSprite;
        }

        public Image IconImage
        {
            get => iconImage;
        }

        public Image BackgroundImage
        {
            get => backgroundImage;
        }

        public Sprite UpgradeIcon {
            get;
            private set;
        }

        private UpgradeVisualizationState BehaviorState {
            get;
            set;
        }

        private Action<UpgradeNode> OnBuyUpgradeCallback
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void SetInfo(UpgradeNode node, UpgradesTreePanelView parent, Action<UpgradeNode> onBuyUpgradeCallback)
        {
            CurrentUpgradeNode = node;
            ParentReference = parent;
            UpgradeIcon = node.GetIcon();
            OnBuyUpgradeCallback = onBuyUpgradeCallback;
            SetBehaviorState(new UpgradeLockVisualizationState());
        }

        public void RefreshInfoBox()
        {
            BehaviorState.ShowInfo(ParentReference.InfoBox);
        }

        public void SetIconColor(Color newColor)
        {
            IconImage.color = newColor;
        }

        public void SetIcon(Sprite icon)
        {
            IconImage.sprite = icon;
        }

        public void SetBackgroundSprite(Sprite background)
        {
            BackgroundImage.sprite = background;
        }

        public void SetBehaviorState(UpgradeVisualizationState newState)
        {
            BehaviorState = newState;
            BehaviorState.SetContext(this);
        }

        public void RefreshState()
        {
            BehaviorState.TrySwicthState();
        }

        public override void OnSelected()
        {
            base.OnSelected();

            BehaviorState.ShowInfo(ParentReference.InfoBox);
            BehaviorState.OnHold(ParentReference.InfoBox);
        }

        public override void OnDeselected()
        {
            base.OnDeselected();

            BehaviorState.OnRelease(ParentReference.InfoBox);
        }

        public void CleanData()
        {
            Destroy(gameObject);
        }

        public void HandleBuyUpgrade()
        {
            OnBuyUpgradeCallback(CurrentUpgradeNode);
        }

        #endregion

        #region Enums



        #endregion
    }
}

