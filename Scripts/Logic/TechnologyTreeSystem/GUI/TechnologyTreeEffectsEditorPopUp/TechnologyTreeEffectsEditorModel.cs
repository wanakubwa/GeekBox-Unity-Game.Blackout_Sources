using System;
using System.Collections.Generic;
using System.Reflection;
using TechnologyTree.Attributes;
using UnityEngine;

namespace GeekBox.TechnologyTree.UI
{
    public class TechnologyTreeEffectsEditorModel : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TechnologyTreeEffectsEditorView view;

        #endregion

        #region Propeties

        public TechnologyTreeEffectsEditorView View {
            get => view;
        }

        public UpgradeNode CurrentUpgrade {
            get;
            private set;
        }

        public UpgradeEffectBase CurrentEffect {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Setup(UpgradeNode node)
        {
            CurrentUpgrade = node;
        }

        public void SetCurrentEffect(UpgradeEffectBase effect)
        {
            CurrentEffect = effect;
        }

        public void SaveCurrentEffect()
        {
            if(CurrentEffect == null)
            {
                return;
            }

            List<FieldDrawer> spawnedDrawers = View.SpawnedFieldsCollection;
            List<FieldInfo> avaibleFields = GetDrawableFieldsFromEffect();

            for(int i =0; i < avaibleFields.Count; i++)
            {
                FieldDrawer targetDrawer = spawnedDrawers.Find(x => x.DrawedType == avaibleFields[i].FieldType && x.Label.text == avaibleFields[i].Name);
                avaibleFields[i].SetValue(CurrentEffect, targetDrawer.GetValue());
            }
        }

        public void AddEffect()
        {
            CreateNewEffect(View.GetSelectedDropdownEffectName());
        }

        public void RemoveEffect(UpgradeEffectBase effect)
        {
            if(CurrentEffect == effect)
            {
                CurrentEffect = null;
            }

            CurrentUpgrade.RemoveEffect(effect);
        }

        public List<string> GetAllAvaibleEffectsNames()
        {
            List<string> typesNames = new List<string>();
            List<Type> effectClass = AppDomain.CurrentDomain.GetAssemblies().GetTypesWithAttribute<UpgradeEffectAttribute>();
            for(int i =0; i < effectClass.Count; i++)
            {
                typesNames.Add(effectClass[i].Name);
            }

            return typesNames;
        }

        public List<FieldInfo> GetDrawableFieldsFromEffect()
        {
            if(CurrentEffect == null)
            {
                return new List<FieldInfo>();
            }

            return CurrentEffect.GetType().GetFieldsWithAttribute<EffectFieldAttribute>();
        }

        private void CreateNewEffect(string effectTypeName)
        {
            Type effectType = Type.GetType(effectTypeName);
            if (effectType != default(Type))
            {
                UpgradeEffectBase effect = Activator.CreateInstance(effectType) as UpgradeEffectBase;
                if (effect != null)
                {
                    CurrentUpgrade.AddEffect(effect);
                }
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
