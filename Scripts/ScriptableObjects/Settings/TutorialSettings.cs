using System;
using UnityEngine;

namespace TutorialSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "TutorialSettings.asset", menuName = "Settings/TutorialSettings")]
    public class TutorialSettings : ScriptableObject
    {
        #region Fields

        private static TutorialSettings instance;

        [SerializeField]
        private TutorialDefinition[] tutorials;

        #endregion

        #region Propeties

        public static TutorialSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<TutorialSettings>("Settings/TutorialSettings");
                }

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public TutorialDefinition[] Tutorials {
            get => tutorials; 
        }

        #endregion

        #region Methods

        public TutorialDefinition GetTutorialDefinition(TutorialType targetType)
        { 
            for(int i =0; i < Tutorials.Length; i++)
            {
                if(Tutorials[i].TypeOfTutorial == targetType)
                {
                    return Tutorials[i];
                }
            }

            return null;
        }

        #endregion

        #region Enums



        #endregion
    }
}

