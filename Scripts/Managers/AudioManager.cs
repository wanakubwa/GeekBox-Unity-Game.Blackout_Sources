using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class AudioManager : SingletonSaveableManager<AudioManager, AudioManagerMemento>
{
    #region Fields

    [ShowInInspector, NonSerialized]
    private AudioTrack currentSoundTrack = null;
    [ShowInInspector, NonSerialized]
    private AudioAmbientTrack currentAmbientTrack = null;

    [Space(10)]
    [SerializeField, ReadOnly]
    private float volume = 0;
    [SerializeField]
    private float maxVolume = 1f;
    [SerializeField]
    private bool isAudioMute = false;

    #endregion

    #region Propeties

    public event Action<float> OnVolumeChanged = delegate { };

    public AudioTrack CurrentSoundTrack
    {
        get => currentSoundTrack;
        private set => currentSoundTrack = value;
    }

    public AudioAmbientTrack CurrentAmbientTrack
    {
        get => currentAmbientTrack;
        private set => currentAmbientTrack = value;
    }

    public float Volume
    {
        get => volume;
        private set => volume = value;
    }

    public float MaxVolume { 
        get => maxVolume; 
        private set => maxVolume = value; 
    }

    public bool IsAudioMute { 
        get => isAudioMute; 
        private set => isAudioMute = value; 
    }

    #endregion

    #region Methods

    public void SetAudioMute(bool isMuteAudio)
    {
        IsAudioMute = isMuteAudio;
        if(IsAudioMute == true)
        {
            SetVolume(0f);
        }
        else
        {
            SetVolume(MaxVolume);
        }
    }

    public void SetVolume(float value)
    {
        Volume = Mathf.Clamp(value, 0f, 1f);
        OnVolumeChanged(Volume);
    }

    public void PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel label)
    {
        AudioContainerSettings audioContainer = AudioContainerSettings.Instance;
        if (audioContainer == null)
        {
            return;
        }

        SoundElement audioElement = audioContainer.GetAudioElementByLabel(label);
        if (audioElement != null)
        {
            if (CurrentSoundTrack != null)
            {
                if (CurrentSoundTrack.IsTrackEqual(label) == true)
                {
                    CurrentSoundTrack.ResetAudio();
                }
                else
                {
                    PlayAudioSoundTrack(audioElement, label);
                }
            }
            else
            {
                PlayAudioSoundTrack(audioElement, label);
            }
        }
    }

    public void PlayAmbientSoundBySceneId(int sceneId)
    {
        AudioContainerSettings audioContainer = AudioContainerSettings.Instance;
        if (audioContainer == null)
        {
            return;
        }

        SoundElement audioElement = audioContainer.GetAudioElementBySceneId(sceneId);
        if(audioElement != null)
        {
            PlayAmbientSoundTrack(audioElement, sceneId);
        }
    }

    public override void LoadManager(AudioManagerMemento memento)
    {
        SetAudioMute(memento.IsAudioMutedSave);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SetAudioMute(IsAudioMute);
        PlayAmbientSoundBySceneId(SceneManager.GetActiveScene().buildIndex);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void PlayAudioSoundTrack(SoundElement audio, AudioContainerSettings.AudioLabel label)
    {
        if (CurrentSoundTrack != null)
        {
            CurrentSoundTrack.DestroyAudio();
        }

        SoundElement audioElement = Instantiate(audio);
        audioElement.transform.ResetParent(transform);
        CurrentSoundTrack = new AudioTrack(audioElement, label);
    }

    private void PlayAmbientSoundTrack(SoundElement audio, int sceneId)
    {
        if (CurrentAmbientTrack != null)
        {
            if(CurrentAmbientTrack.IsTrackEqual(sceneId) == true)
            {
                return;
            }

            CurrentAmbientTrack.DestroyAudio();
        }

        SoundElement audioElement = Instantiate(audio);
        audioElement.transform.ResetParent(transform);
        CurrentAmbientTrack = new AudioAmbientTrack(audioElement, sceneId);
    }

    #endregion

    #region Handlers

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        PlayAmbientSoundBySceneId(scene.buildIndex);
    }

    #endregion

    [Serializable]
    public class AudioTrack
    {
        #region Fields

        [SerializeField]
        private AudioContainerSettings.AudioLabel label;
        [SerializeField]
        private SoundElement audioElement;

        #endregion

        #region Propeties

        public AudioContainerSettings.AudioLabel Label
        {
            get => label;
            private set => label = value;
        }

        public SoundElement AudioElement
        {
            get => audioElement;
            private set => audioElement = value;
        }

        #endregion

        #region Methods

        public AudioTrack(SoundElement audio, AudioContainerSettings.AudioLabel label)
        {
            AudioElement = audio;
            Label = label;
        }

        public AudioTrack(SoundElement audio)
        {
            AudioElement = audio;
        }

        public bool IsTrackEqual(AudioContainerSettings.AudioLabel Label)
        {
            if (Label == label)
            {
                return true;
            }

            return false;
        }

        public void ResetAudio()
        {
            StopAudio();
            PlayAudio();
        }

        public void StopAudio()
        {
            AudioElement.StopAudio();
        }

        public void PlayAudio()
        {
            AudioElement.PlayOneShotAudio();
        }

        public void DestroyAudio()
        {
            AudioElement.DestroyAudio();
        }

        #endregion

        #region Handlers



        #endregion
    }

    [Serializable]
    public class AudioAmbientTrack
    {
        #region Fields

        [SerializeField]
        private int sceneId;
        [SerializeField]
        private SoundElement audioElement;

        #endregion

        #region Propeties

        public SoundElement AudioElement
        {
            get => audioElement;
            private set => audioElement = value;
        }

        public int SceneId
        {
            get => sceneId;
            private set => sceneId = value;
        }

        #endregion

        #region Methods

        public AudioAmbientTrack(SoundElement audio, int sceneId)
        {
            AudioElement = audio;
            SceneId = sceneId;
        }

        public bool IsTrackEqual(int sceneId)
        {
            if (SceneId == sceneId)
            {
                return true;
            }

            return false;
        }

        public void ResetAudio()
        {
            StopAudio();
            PlayAudio();
        }

        public void StopAudio()
        {
            AudioElement.StopAudio();
        }

        public void PlayAudio()
        {
            AudioElement.PlayOneShotAudio();
        }

        public void DestroyAudio()
        {
            AudioElement.DestroyAudio();
        }

        #endregion

        #region Handlers

        #endregion
    }
}
