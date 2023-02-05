
namespace SaveLoadSystem
{
    public interface ISaveable : IResettable
    {
        #region Fields



        #endregion

        #region Propeties

        bool IsLoaded { get; }

        #endregion

        #region Methods

        void Save(string directoryPath);
        void Load(string directoryPath);

        #endregion

        #region Enums



        #endregion
    }
}

