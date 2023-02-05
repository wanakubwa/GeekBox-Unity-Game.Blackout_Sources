using System;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    [Serializable]
    public class AISettings
    {
        #region Fields

        [SerializeField]
        private List<AIParentSettings> parentsActorsSettings = new List<AIParentSettings>();

        #endregion

        #region Propeties

        public List<AIParentSettings> ParentsActorsSettings { 
            get => parentsActorsSettings; 
            private set => parentsActorsSettings = value; 
        }

        #endregion

        #region Methods

        public AISettings()
        {

        }

        public AISettings(AISettings settings)
        {
            ParentsActorsSettings = settings.ParentsActorsSettings.Clone();
        }

        public AIParentSettings GetParentAISettingsByParentId(int parentId)
        {
            AIParentSettings settings = ParentsActorsSettings.GetElementByID(parentId);
            if(settings == null)
            {
                settings = new AIParentSettings(parentId);
                ParentsActorsSettings.Add(settings);
            }

            return settings;
        }

        #endregion

        #region Enums



        #endregion
    }
}
