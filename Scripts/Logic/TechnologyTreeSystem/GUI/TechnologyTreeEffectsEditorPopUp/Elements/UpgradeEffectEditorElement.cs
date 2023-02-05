using UnityEngine;
using UnityEngine.UI;

namespace GeekBox.TechnologyTree.UI
{
    public class UpgradeEffectEditorElement : ButtonBase
    {
        #region Fields

        [SerializeField]
        private Text label;

        #endregion

        #region Propeties

        private TechnologyTreeEffectsEditorController Listener
        {
            get;
            set;
        }

        private Text Label { 
            get => label;
        }

        public UpgradeEffectBase CachedEffect
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void Draw(UpgradeEffectBase effect, TechnologyTreeEffectsEditorController controller)
        {
            CachedEffect = effect;
            Listener = controller;

            Label.text = effect.GetType().Name;
        }

        public void OnDeleteButtonClick()
        {
            Listener.OnDeleteEffect(CachedEffect);
        }

        #endregion

        #region Enums



        #endregion
    }
}
