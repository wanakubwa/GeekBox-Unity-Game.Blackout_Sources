using UnityEngine;

namespace UpgradesGUI
{
    public abstract class UpgradeVisualizationState
    {
        #region Fields



        #endregion

        #region Propeties

        public virtual Color StateIconColor
        {
            get => Color.white;
        }

        protected UpgradeVisualizationElement Context
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void SetContext(UpgradeVisualizationElement context)
        {
            Context = context;
            if (TrySwicthState() == false)
            {
                SetupIcon();
            }
        }

        public virtual void OnHold(UpgradeInfoBox infoBox) { }

        public virtual void OnRelease(UpgradeInfoBox infoBox) { }

        public abstract bool TrySwicthState();

        public abstract void SetupIcon();

        public abstract void ShowInfo(UpgradeInfoBox infoBox);

        #endregion

        #region Enums



        #endregion
    }
}

