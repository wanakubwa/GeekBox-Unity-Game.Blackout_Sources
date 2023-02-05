using UnityEngine;

namespace GeekBox.TechnologyTree.UI
{
    [RequireComponent(typeof(TechnologyTreeEffectsEditorView), typeof(TechnologyTreeEffectsEditorModel))]
    public class TechnologyTreeEffectsEditorController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TechnologyTreeEffectsEditorModel model;
        [SerializeField]
        private TechnologyTreeEffectsEditorView view;

        #endregion

        #region Propeties

        public TechnologyTreeEffectsEditorModel Model { get => model; }
        public TechnologyTreeEffectsEditorView View { get => view; }

        #endregion

        #region Methods

        public void Setup(UpgradeNode upgrade)
        {
            Model.Setup(upgrade);
            View.Initialize();
        }

        public void AddSelectedEffect()
        {
            Model.AddEffect();
            View.RefreshEffectsList();
        }

        public void OnDeleteEffect(UpgradeEffectBase sender)
        {
            Model.RemoveEffect(sender);
            View.RefreshEffectsList();
            View.RefreshSelectedEffectInfo();
        }

        public void OnSelectEffect(UpgradeEffectEditorElement sender)
        {
            Model.SetCurrentEffect(sender.CachedEffect);
            View.RefreshSelectedEffectInfo();
        }

        public void ClosePopUp()
        {
            Destroy(gameObject);
        }

        public void SaveChanges()
        {
            Model.SaveCurrentEffect();
        }

        #endregion

        #region Enums



        #endregion
    }
}
