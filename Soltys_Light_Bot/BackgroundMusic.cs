using System.Collections.Generic;
using IrrKlang;
namespace Soltys.ProgrammerBot
{
    class BackgroundMusic
    {
        readonly List<string> playList = new List<string>();
        readonly ISoundEngine soundEngine = new ISoundEngine();
        int numberTrack;
        public bool PlayMusic { get; set; }
        public BackgroundMusic()
        {

            playList.Add(@"Music/Biomythos.mp3");
            playList.Add(@"Music/Goof.mp3");
            playList.Add(@"Music/WeMayThink.mp3");
            numberTrack = 0;
        }

        public void StartMusic()
        {
            soundEngine.SoundVolume = 0.5f;
            soundEngine.Play2D(playList[numberTrack]);
            PlayMusic = true;
        }

        public void ToggleMusic()
        {
            if (PlayMusic)
            {
                soundEngine.SetAllSoundsPaused(true);
                PlayMusic = false;
            }
            else
            {
                soundEngine.SetAllSoundsPaused(false);
                PlayMusic = true;
            }
        }

        public void Update()
        {
            if (PlayMusic)
            {
                if (!soundEngine.IsCurrentlyPlaying(playList[numberTrack]))
                {
                    nextTrack();
                    StartMusic();
                }
            }
        }

        private void nextTrack()
        {
            numberTrack++;
            if (numberTrack >= playList.Count)
            {
                numberTrack = 0;
            }
        }
    }
}
