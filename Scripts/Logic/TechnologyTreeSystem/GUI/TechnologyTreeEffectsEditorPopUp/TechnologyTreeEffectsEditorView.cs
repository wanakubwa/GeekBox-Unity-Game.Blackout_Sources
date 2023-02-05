using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace GeekBox.TechnologyTree.UI
{
    public class TechnologyTreeEffectsEditorView : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TechnologyTreeEffectsEditorModel model;
        [SerializeField]
        private TechnologyTreeEffectsEditorController controller;

        [Space, Header("Effects")]
        [SerializeField]
        private RectTransform effectsContent;
        [SerializeField]
        private UpgradeEffectEditorElement effectButtonPrefab;

        [Space, Header("Fields")]
        [SerializeField]
        private RectTransform fieldsContent;
        [SerializeField]
        private List<FieldDrawer> fieldDrawers = new List<FieldDrawer>();

        [SerializeField]
        private Dropdown effectsDropdown;

        #endregion

        #region Propeties

        public TechnologyTreeEffectsEditorModel Model { get => model; }
        public TechnologyTreeEffectsEditorController Controller { get => controller; }

        public UpgradeEffectEditorElement EffectButtonPrefab { get => effectButtonPrefab; }
        public Dropdown EffectsDropdown { get => effectsDropdown; }
        public List<FieldDrawer> FieldDrawers { get => fieldDrawers; }
        public RectTransform EffectsContent { get => effectsContent; }
        public RectTransform FieldsContent { get => fieldsContent; }
        private List<UpgradeEffectEditorElement> SpawnedEffectsCollection {
            get;
            set;
        } = new List<UpgradeEffectEditorElement>();

        public List<FieldDrawer> SpawnedFieldsCollection {
            get;
            private set;
        } = new List<FieldDrawer>();

        #endregion

        #region Methods

        public string GetSelectedDropdownEffectName()
        {
            return EffectsDropdown.options[EffectsDropdown.value].text;
        }

        public void Initialize()
        {
            EffectsDropdown.ClearOptions();
            EffectsDropdown.AddOptions(Model.GetAllAvaibleEffectsNames());

            RefreshEffectsList();
        }

        public void RefreshSelectedEffectInfo()
        {
            SpawnedFieldsCollection.ClearDestroy();
            List<FieldInfo> fieldsToDraw = Model.GetDrawableFieldsFromEffect();
            for(int i =0; i < fieldsToDraw.Count; i++)
            {
                FieldDrawer drawerPrefab = GetFieldForType(fieldsToDraw[i].FieldType);
                if(drawerPrefab != null)
                {
                    FieldDrawer spawnedDrawer = Instantiate(drawerPrefab);
                    spawnedDrawer.transform.ResetParent(FieldsContent);
                    spawnedDrawer.Draw(fieldsToDraw[i].FieldType, fieldsToDraw[i].Name);
                    spawnedDrawer.SetValue(fieldsToDraw[i].GetValue(Model.CurrentEffect).ToString());

                    SpawnedFieldsCollection.Add(spawnedDrawer);
                }
            }
        }

        public void RefreshEffectsList()
        {
            SpawnedEffectsCollection.ClearDestroy();

            UpgradeNode upgrade = Model.CurrentUpgrade;
            foreach  (UpgradeEffectBase effect in upgrade.Effects)
            {
                UpgradeEffectEditorElement uiUpgrade = Instantiate(EffectButtonPrefab);
                uiUpgrade.transform.ResetParent(EffectsContent);
                uiUpgrade.Draw(effect, Controller);

                SpawnedEffectsCollection.Add(uiUpgrade);
            }
        }

        private FieldDrawer GetFieldForType(Type fieldType)
        {
            if(fieldType == typeof(int))
            {
                return GetFieldByEnum(FieldDrawer.InputFieldType.INT);
            }
            else if(fieldType == typeof(float))
            {
                return GetFieldByEnum(FieldDrawer.InputFieldType.FLOAT);
            }
            else if(fieldType.IsEnum == true)
            {
                return GetFieldByEnum(FieldDrawer.InputFieldType.ENUM);
            }

            return null;
        }

        private FieldDrawer GetFieldByEnum(FieldDrawer.InputFieldType fieldType)
        {
            for(int i =0; i < FieldDrawers.Count; i++)
            {
                if(FieldDrawers[i].FieldType == fieldType)
                {
                    return FieldDrawers[i];
                }
            }

            return null;
        }

        #endregion

        #region Enums



        #endregion
    }
}
