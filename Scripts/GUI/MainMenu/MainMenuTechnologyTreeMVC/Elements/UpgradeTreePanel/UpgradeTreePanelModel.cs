using System.Collections.Generic;
using UnityEngine;

namespace UpgradesGUI
{
    public class UpgradeTreePanelModel : UIModel
    {
        #region Fields



        #endregion

        #region Propeties

        private TechnologyTreeSettings TechnologyTreeDatabase {
            get;
            set;
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();

            TechnologyTreeDatabase = TechnologyTreeSettings.Instance;
        }

        public UpgradeNode GetUpgradeDefinitionById(int id)
        {
            return TechnologyTreeDatabase.GetUpgradeNodeById(id);
        }

        public List<UpgradeNode> GetUpgradesDefinitionByIds(List<int> ids)
        {
            List<UpgradeNode> output = new List<UpgradeNode>();
            foreach (int id in ids)
            {
                output.Add(GetUpgradeDefinitionById(id));
            }

            return output;
        }

        public List<UpgradeNode> GetRootUpgradesDefinitions()
        {
            return GetUpgradesDefinitionByIds(TechnologyTreeDatabase.SettingsData.RootIds);
        }

        public void TryBuyUpgrade(UpgradeNode upgradeDefiniton)
        {
            TechnologyTreeManager.Instance.TryBuyUpgrade(upgradeDefiniton);
        }

        #endregion

        #region Enums



        #endregion
    }
}
