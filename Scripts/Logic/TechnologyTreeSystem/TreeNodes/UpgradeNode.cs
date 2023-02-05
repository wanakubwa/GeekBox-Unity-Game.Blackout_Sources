using GeekBox.TechnologyTree;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeNode : IIDEquatable
{
    #region Fields
    [SerializeField]
    private string nameKey;
    [SerializeField]
    private string iconName;
    [SerializeField]
    private int upgradeId = Constants.DEFAULT_ID;
    [SerializeField]
    private List<int> requiredUpgradesIds = new List<int>();
    [SerializeField]
    private List<int> unlockedUpgradesIds = new List<int>();
    [SerializeField]
    private UpgradeType currentType = UpgradeType.NORMAL;

    [SerializeField]
    private Vector3 editorPosition;

    // Serializacja za pomoca odina aby zachowac typy dynamiczne klas pochodnych.
    [OdinSerialize]
    private List<UpgradeEffectBase> effects = new List<UpgradeEffectBase>();

    #endregion

    #region Propeties

    public int UpgradeId { 
        get => upgradeId; 
        private set => upgradeId = value; 
    }

    public List<int> RequiredUpgradesIds { 
        get => requiredUpgradesIds; 
        private set => requiredUpgradesIds = value; 
    }

    public List<int> UnlockedUpgradesIds {
        get => unlockedUpgradesIds;
        private set => unlockedUpgradesIds = value; 
    }

    public List<UpgradeEffectBase> Effects { 
        get => effects; 
        private set => effects = value; 
    }

    public int ID => UpgradeId;

    public string NameKey { 
        get => nameKey; 
        private set => nameKey = value; 
    }

    public string IconName { 
        get => iconName; 
        private set => iconName = value; 
    }

    public Vector3 EditorPosition { 
        get => editorPosition; 
        private set => editorPosition = value; 
    }

    public UpgradeType CurrentType { 
        get => currentType; 
        private set => currentType = value; 
    }

    #endregion

    #region Methods

    public UpgradeNode()
    {
        UpgradeId = Guid.NewGuid().GetHashCode();
    }

    public string GetEffectsInfo()
    {
        string formattedInfo = string.Empty;
        for(int i =0; i < Effects.Count; i++)
        {
            if(i == Effects.Count - 1)
            {
                formattedInfo += Effects[i].GetInfo();
            }
            else
            {
                formattedInfo += (Effects[i].GetInfo() + "\n");
            }
        }

        return formattedInfo;
    }

    public Sprite GetIcon()
    {
        return TechnologyTreeSettings.Instance.Icons.GetIconByName(IconName);
    }

    public void SetNameKey(string key)
    {
        NameKey = key;
    }

    public void SetIconName(string iconName)
    {
        IconName = iconName;
    }

    public void SetEditorPosition(Vector3 position)
    {
        EditorPosition = position;
    }

    public void SetUpgradeType(UpgradeType type)
    {
        CurrentType = type;
    }

    public void AddRequiredUpgradeId(int id)
    {
        RequiredUpgradesIds.Add(id);
    }

    public void AddEffect(UpgradeEffectBase effect)
    {
        Effects.Add(effect);
    }

    public void RemoveEffect(UpgradeEffectBase effect)
    {
        Effects.Remove(effect);
    }

    public void RemoveRequiredUpgradeId(int id)
    {
        RequiredUpgradesIds.Remove(id);
    }

    public void AddUnlockedUpgradeId(int id)
    {
        UnlockedUpgradesIds.Add(id);
    }

    public void RemoveUnlockedUpgradeId(int id)
    {
        UnlockedUpgradesIds.Remove(id);
    }

    public void ApplyEffects(ParentValues target)
    {
        for(int i = 0; i < Effects.Count; i++)
        {
            Effects[i].Apply(target);
        }
    }

    public bool IDEqual(int otherId)
    {
        return UpgradeId == otherId;
    }

    #endregion

    #region Enums


    #endregion
}
