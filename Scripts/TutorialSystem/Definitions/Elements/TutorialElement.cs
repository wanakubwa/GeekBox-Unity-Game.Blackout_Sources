using System;
using UnityEngine;

namespace TutorialSystem
{
    [Serializable]
    public class TutorialElement
    {
        #region Fields

        [SerializeField]
        private string titleKey;
        [SerializeField]
        private string dscKey;
        [SerializeField]
        private Texture2D imgTexture;

        #endregion

        #region Propeties

        public string TitleKey { get => titleKey; }
        public string DscKey { get => dscKey; }
        public Texture2D ImgTexture { get => imgTexture; }

        #endregion

        #region Methods



        #endregion

        #region Enums



        #endregion
    }
}
