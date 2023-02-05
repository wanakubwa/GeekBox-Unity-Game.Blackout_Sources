using GeekBox.TechnologyTree;
using GeekBox.UI;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UpgradesGUI
{
    public class UpgradesTreePanelView : UIView, ICleanable
    {
        #region Fields

        [SerializeField]
        private UpgradeVisualizationElement normalUpgradePrefab;
        [SerializeField]
        private UpgradeVisualizationElement specialUpgradePrefab;
        [SerializeField]
        private UpgradeVisualizationContainer containerPrefab;
        [SerializeField]
        private UpgradeInfoBox infoBox;

        [Space]
        [SerializeField]
        private RectTransform technologyTreeContent;
        [SerializeField]
        private RectTransform connecionsContent;

        [Title("Line Renderers")]
        [SerializeField]
        private UILineRenderer connectionRenderer;

        #endregion

        #region Propeties

        private UpgradeVisualizationElement NormalUpgradePrefab { get => normalUpgradePrefab; }
        private UpgradeVisualizationElement SpecialUpgradePrefab { get => specialUpgradePrefab; }
        public UpgradeInfoBox InfoBox { get => infoBox; }
        private RectTransform TechnologyTreeContent { get => technologyTreeContent; }
        private UpgradeVisualizationContainer ContainerPrefab { get => containerPrefab; }
        private UILineRenderer ConnectionRendererPrefab { get => connectionRenderer; }
        private UpgradeTreePanelModel CurrentModel {
            get;
            set;
        }

        private UpgradesTreePanelController CurrentController {
            get;
            set;
        }

        private SortedList<int, UpgradeVisualizationElement> SpawnedUpgradesCollection { 
            get; 
            set; 
        } = new SortedList<int, UpgradeVisualizationElement>();

        private List<UpgradeVisualizationContainer> SpawnedContainers {
            get;
            set;
        } = new List<UpgradeVisualizationContainer>();

        private List<UILineRenderer> SpawnedLinesCollection {
            get;
            set;
        } = new List<UILineRenderer>();

        private RectTransform ConnecionsContent { get => connecionsContent; }

        #endregion

        #region Methods

        public void RefreshPanel()
        {
            BuildTechnologyTree();
        }

        public void CleanData()
        {
            SpawnedUpgradesCollection.ClearClean();
            SpawnedContainers.ClearDestroy();
            SpawnedLinesCollection.ClearDestroy();
            InfoBox.Hide();
        }

        public override void Initialize()
        {
            CurrentModel = GetModel<UpgradeTreePanelModel>();
            CurrentController = GetController<UpgradesTreePanelController>();
        }

        public void RefreshUpgradeWithNeighbors(int unlockedUpgradeId)
        {
            MEC.Timing.RunCoroutine(_RefreshUpgradeWithNeighborsCoroutine(unlockedUpgradeId));
        }

        public void RefreshAllUpgrades()
        {
            MEC.Timing.RunCoroutine(_RefreshUpgradesCoroutine());
        }

        private void BuildTechnologyTree()
        {
            MEC.Timing.RunCoroutine(_BuildTechnologyTreeCoroutine());
        }

        private UpgradeVisualizationElement SpawnNormalUpgradeVisualization(UpgradeNode upgrade)
        {
            UpgradeVisualizationElement upgradeVisualization = Instantiate(NormalUpgradePrefab);
            upgradeVisualization.SetInfo(upgrade, this, CurrentController.BuySelectedUpgrade);

            SpawnedUpgradesCollection.Add(upgrade.UpgradeId, upgradeVisualization);

            return upgradeVisualization;
        }

        private UpgradeVisualizationElement SpawnSpecialUpgradeVisualization(UpgradeNode upgrade)
        {
            UpgradeVisualizationElement upgradeVisualization = Instantiate(SpecialUpgradePrefab);
            upgradeVisualization.SetInfo(upgrade, this, CurrentController.BuySelectedUpgrade);

            SpawnedUpgradesCollection.Add(upgrade.UpgradeId, upgradeVisualization);

            return upgradeVisualization;
        }

        private UpgradeVisualizationContainer SpawnVisualizationContainer(List<UpgradeVisualizationElement> visualizations)
        {
            UpgradeVisualizationContainer visualizationContainer = Instantiate(ContainerPrefab);
            visualizationContainer.transform.ResetParent(TechnologyTreeContent);
            visualizationContainer.transform.SetAsFirstSibling();
            visualizationContainer.InsertUpgradesVisualizations(visualizations);

            return visualizationContainer;
        }

        private UILineRenderer SpawnConnectionRenderer()
        {
            UILineRenderer renderer = Instantiate(ConnectionRendererPrefab);
            renderer.transform.ResetParent(ConnecionsContent);
            SpawnedLinesCollection.Add(renderer);
            return renderer;
        }

        // Coroutines
        private IEnumerator<float> _RefreshUpgradesCoroutine()
        {
            foreach (var visualizationElement in SpawnedUpgradesCollection)
            {
                visualizationElement.Value.RefreshState();
                yield return MEC.Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> _RefreshUpgradeWithNeighborsCoroutine(int unlockedUpgradeId)
        {
            UpgradeNode upgradeDef = CurrentModel.GetUpgradeDefinitionById(unlockedUpgradeId);
            UpgradeVisualizationElement visualization;
            SpawnedUpgradesCollection.TryGetValue(unlockedUpgradeId, out visualization);
            visualization?.RefreshState();
            visualization.RefreshInfoBox();
            yield return MEC.Timing.WaitForOneFrame;

            foreach (int unlockedId in upgradeDef.UnlockedUpgradesIds)
            {
                SpawnedUpgradesCollection.TryGetValue(unlockedUpgradeId, out visualization);
                visualization?.RefreshState();
            }
        }

        private IEnumerator<float> _BuildTechnologyTreeCoroutine()
        {
            HashSet<UpgradeNode> nextUpgrades = new HashSet<UpgradeNode>();
            List<UpgradeNode> upgrades = CurrentModel.GetRootUpgradesDefinitions();
            List<UpgradeVisualizationElement> visualizations = new List<UpgradeVisualizationElement>();
            UpgradeVisualizationElement visualization, newVisualization;

            while (upgrades.Count > 0)
            {
                visualizations.Clear();
                nextUpgrades.Clear();

                for (int i = 0; i < upgrades.Count; i++)
                {
                    if (upgrades[i].CurrentType == UpgradeType.NORMAL)
                    {
                        newVisualization = SpawnNormalUpgradeVisualization(upgrades[i]);
                    }
                    else
                    {
                        newVisualization = SpawnSpecialUpgradeVisualization(upgrades[i]);
                    }

                    visualizations.Add(newVisualization);

                    nextUpgrades.AddRange(CurrentModel.GetUpgradesDefinitionByIds(upgrades[i].UnlockedUpgradesIds).ToHashSet());
                    yield return MEC.Timing.WaitForOneFrame;
                }

                SpawnedContainers.Add(SpawnVisualizationContainer(visualizations));
                upgrades = new List<UpgradeNode>(nextUpgrades);
                yield return MEC.Timing.WaitForOneFrame;
            }

            // Przeliczenie UI.
            yield return MEC.Timing.WaitForOneFrame;

            ConnecionsContent.SetAsFirstSibling();

            for (int i =0; i < SpawnedUpgradesCollection.Values.Count; i++)
            {
                UpgradeNode u = SpawnedUpgradesCollection.Values[i].CurrentUpgradeNode;
                UILineRenderer renderer;
                SpawnedUpgradesCollection.TryGetValue(u.ID, out newVisualization);

                for (int j = 0; j < u.RequiredUpgradesIds.Count; j++)
                {
                    SpawnedUpgradesCollection.TryGetValue(u.RequiredUpgradesIds[j], out visualization);
                    if (visualization != null)
                    {
                        renderer = SpawnConnectionRenderer();
                        renderer.SetPoint(0, visualization.transform.parent.localPosition + visualization.transform.localPosition);
                        renderer.SetPoint(1, newVisualization.transform.parent.localPosition + newVisualization.transform.localPosition);
                        renderer.transform.position = Vector3.zero;
                        renderer.transform.localPosition = Vector3.zero;
                    }
                }

                yield return MEC.Timing.WaitForOneFrame;
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}

