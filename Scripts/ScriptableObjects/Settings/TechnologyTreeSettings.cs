using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TechnologyTree.Attributes;

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

[Serializable]
[CreateAssetMenu(fileName = "TechnologyTreeSettings.asset", menuName = "Settings/TechnologyTreeSettings")]
public class TechnologyTreeSettings : SerializedScriptableObject
{
    #region Fields

    private static TechnologyTreeSettings instance;

    [ReadOnly, OdinSerialize]
    private List<UpgradeNode> technlogyUpgradesDefinitions = new List<UpgradeNode>();

    [Space]
    [SerializeField]
    private Settings settingsData = new Settings();
    [SerializeField]
    private IconsSettings icons = new IconsSettings();
    [OdinSerialize]
    private EffectsSettings effects = new EffectsSettings();

    #endregion

    #region Propeties

    public static TechnologyTreeSettings Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<TechnologyTreeSettings>("Settings/TechnologyTreeSettings");
            }

            return instance;
        }
        set {
            instance = value;
        }
    }

    public List<UpgradeNode> TechnlogyUpgradesDefinitions { 
        get => technlogyUpgradesDefinitions; 
        private set => technlogyUpgradesDefinitions = value; 
    }

    public IconsSettings Icons { get => icons; }
    public Settings SettingsData { get => settingsData; }
    public EffectsSettings Effects { get => effects; }

    #endregion

    #region Methods

    public void AddTechnologyUpgrade(UpgradeNode upgradeNode)
    {
        TechnlogyUpgradesDefinitions.Add(upgradeNode);
        HandleUpgradesCollectionChanged();
    }

    public void RemoveTechnologyUpgrade(UpgradeNode target)
    {
        List<UpgradeNode> requiredUpgrades = GetUpgradesNodesByIds(target.RequiredUpgradesIds);
        for (int i = 0; i < requiredUpgrades.Count; i++)
        {
            requiredUpgrades[i].UnlockedUpgradesIds.Remove(target.ID);
        }

        List<UpgradeNode> unlocingUpgrades = GetUpgradesNodesByIds(target.UnlockedUpgradesIds);
        for (int i = 0; i < unlocingUpgrades.Count; i++)
        {
            unlocingUpgrades[i].RequiredUpgradesIds.Remove(target.ID);
        }

        TechnlogyUpgradesDefinitions.RemoveElementByID(target.ID);
        HandleUpgradesCollectionChanged();
    }

    public List<UpgradeNode> GetUpgradesNodesByIds(List<int> ids)
    {
        List<UpgradeNode> upgrades = new List<UpgradeNode>();
        for (int i = 0; i < ids.Count; i++)
        {
            UpgradeNode targetUpgrade = GetUpgradeNodeById(ids[i]);
            if (targetUpgrade != null)
            {
                upgrades.Add(targetUpgrade);
            }
        }

        return upgrades;
    }

    public UpgradeNode GetUpgradeNodeById(int id)
    {
        return TechnlogyUpgradesDefinitions.BinarySearchByID(id);
    }

    public void HandleUpgradesCollectionChanged()
    {
        SortUpgradesCollection();
        SettingsData.SetRootIds(GetRootUpgradesIds());
    }

    private List<int> GetRootUpgradesIds()
    {
        List<int> output = new List<int>();

        for (int i =0; i < TechnlogyUpgradesDefinitions.Count; i++)
        {
            if(TechnlogyUpgradesDefinitions[i].RequiredUpgradesIds.Count < 1)
            {
                output.Add(TechnlogyUpgradesDefinitions[i].UpgradeId);
            }
        }

        return output;
    }

    private void SortUpgradesCollection()
    {
        // Sortowanie id projektow rosnaco aby ulatwic pozniejsze wyszukiwanie.
        TechnlogyUpgradesDefinitions = TechnlogyUpgradesDefinitions.OrderBy(x => x.ID).ToList();
    }

#if UNITY_EDITOR

    [Button(ButtonSizes.Large)]
    public void SaveThisAsset()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [Button(ButtonSizes.Medium)]
    public void RefreshUpgrades()
    {
        HandleUpgradesCollectionChanged();
    }

    [Button]
    public void ResetUpgrades()
    {
        if(EditorUtility.DisplayDialog("UWAGA!!!1", "Ta operacja spowoduje usunięcie wszystkich wprowadzonych upgradów w tym obiekcie! \n \n Czy kontunuować?",
            "Wiem co robie", "Zabierz mnie stąd!") == true)
        {
            TechnlogyUpgradesDefinitions.Clear();
        }
    }

    private void OnValidate()
    {
        Icons.SortIcons();
        Effects.ValidateCollection();
    }

#endif

    #endregion

    #region Enums

    [Serializable]
    public class Settings
    {
        #region Fields

        [SerializeField, ReadOnly]
        private List<int> rootIds = new List<int>();

        #endregion

        #region Propeties

        public List<int> RootIds { 
            get => rootIds;
            private set => rootIds = value;
        }

        #endregion

        #region Methods

        public void SetRootIds(List<int> ids)
        {
            RootIds = ids;
        }

        #endregion

        #region Enums



        #endregion
    }

    [Serializable]
    public class IconsSettings
    {
        #region Fields

        [SerializeField]
        private List<Sprite> iconsCollections = new List<Sprite>();

        #endregion

        #region Propeties

        public List<Sprite> IconsCollections { 
            get => iconsCollections; 
            private set => iconsCollections = value; 
        }

        #endregion

        #region Methods
#if UNITY_EDITOR
        public void SortIcons()
        {
            IconsCollections = IconsCollections.OrderBy(x => x.name).ToList();
        }
#endif

        public string[] GetAllIconsNames()
        {
            string[] names = new string[IconsCollections.Count];

            for(int i =0; i < IconsCollections.Count; i++)
            {
                names[i] = IconsCollections[i].name;
            }

            return names;
        }

        public Sprite GetIconByName(string iconName)
        {
            int index = 0;
            int maxIndex = IconsCollections.Count - 1;

            while (index <= maxIndex)
            {
                int middleIndex = index + (maxIndex - index) / 2;

                // Check if x is present at mid
                if (IconsCollections[middleIndex].name == iconName)
                {
                    return IconsCollections[middleIndex];
                }

                // If x greater, ignore left half
                // If right compared is greater return -1
                // If righ compared is smaller return 1
                if (string.Compare(IconsCollections[middleIndex].name, iconName) == -1)
                {
                    index = middleIndex + 1;
                }
                else // If x is smaller, ignore right half
                {
                    maxIndex = middleIndex - 1;
                }
            }

            Debug.LogFormat("Can't find icon for name: {0}", iconName);
            return null;
        }

        #endregion

        #region Enums



        #endregion
    }

    [Serializable]
    public class EffectsSettings
    {
        #region Fields

        [OdinSerialize]
        private GeekBox.Collections.SortedList<EffectInfo, string> effectsCollection = new GeekBox.Collections.SortedList<EffectInfo, string>();

        #endregion

        #region Propeties

        public GeekBox.Collections.SortedList<EffectInfo, string> EffectsCollection
        {
            get => effectsCollection;
            private set => effectsCollection = value;
        }

        #endregion

        #region Methods

        public string GetEffectKeyByTypeName(string effectType)
        {
            EffectInfo info = EffectsCollection.BinarySearch(effectType);
            return info != null ? info.InfoKey : string.Empty;
        }

#if UNITY_EDITOR
        public void ValidateCollection()
        {
            Assembly effectsAssembly = typeof(UpgradeEffectAttribute).Assembly;
            List<Type> effectsTypes = effectsAssembly.GetTypesWithAttribute(typeof(UpgradeEffectAttribute)).ToList();

            foreach (Type effectType in effectsTypes)
            {
                if(EffectsCollection.BinarySearch(effectType.Name) == null)
                {
                    EffectsCollection.Add(new EffectInfo(effectType.Name, string.Empty), effectType.Name);
                }
            }

            foreach(EffectInfo info in EffectsCollection.Collection)
            {
                bool exists = false;
                foreach (Type type in effectsTypes)
                {
                    if(type.Name == info.EffectTypeName)
                    {
                        exists = true;
                        break;
                    }
                }

                if(exists == false)
                {
                    EffectsCollection.Remove(info);
                }
            }
        }
#endif

        #endregion

        #region Enums

        #endregion
    }

    [Serializable]
    public class EffectInfo : IEquatable<string>, IComparable<string>
    {
        #region Fields

        [SerializeField, ReadOnly]
        private string effectTypeName;
        [SerializeField]
        private string infoKey;

        #endregion

        #region Propeties

        public string EffectTypeName { get => effectTypeName; }
        public string InfoKey { get => infoKey;}

        #endregion

        #region Methods

        public EffectInfo() { }

        public EffectInfo(string effectTypeName, string infoKey)
        {
            this.effectTypeName = effectTypeName;
            this.infoKey = infoKey;
        }

        public bool Equals(string other)
        {
            return EffectTypeName == other;
        }

        public int CompareTo(string other)
        {
            // If other is not a valid object reference, this instance is greater.
            if (other == null) return 1;

            return EffectTypeName.CompareTo(other);
        }

        #endregion

        #region Enums



        #endregion
    }

    #endregion
}
