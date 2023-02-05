using System.Collections.Generic;
using UnityEngine;

namespace UpgradesGUI
{
    public class UpgradeVisualizationContainer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private RectTransform content;

        #endregion

        #region Propeties

        public RectTransform Content { get => content; }

        #endregion

        #region Methods

        public void InsertUpgradesVisualizations(List<UpgradeVisualizationElement> visualizationElements)
        {
            foreach (UpgradeVisualizationElement visualizationElement in visualizationElements)
            {
                visualizationElement.transform.ResetParent(Content);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
