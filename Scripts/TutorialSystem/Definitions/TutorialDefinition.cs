using System;
using UnityEngine;

namespace TutorialSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "TutorialDefinition.asset", menuName = "Custom/Tutorial/TutorialDefinition")]
    public class TutorialDefinition : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private TutorialType typeOfTutorial;

        [SerializeField]
        private TutorialElement[] tutorialElements;

        #endregion

        #region Propeties

        public TutorialElement[] TutorialElements { 
            get => tutorialElements;
        }
        public TutorialType TypeOfTutorial { 
            get => typeOfTutorial; 
        }

        #endregion

        #region Methods



        #endregion

        #region Enums



        #endregion
    }
}
