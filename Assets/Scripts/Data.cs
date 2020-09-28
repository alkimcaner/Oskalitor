using UnityEngine;

namespace Data
{
    public class Score
    {
        private string highscore="High_Score";
        public float HighScore
        {
            get{ return PlayerPrefs.GetFloat(highscore); }
            set{ PlayerPrefs.SetFloat(highscore, value); }
        }

    }

    public class Settings
    {
        private string performance="performance";
        public string Performance
        {
            get{ return PlayerPrefs.GetString(performance); }
            set{ PlayerPrefs.SetString(performance, value); }
        }
        private string quality="quality";
        public string Quality
        {
            get{ return PlayerPrefs.GetString(quality); }
            set{ PlayerPrefs.SetString(quality, value); }
        }

        private string thicc="thicc";
        public string Thicc
        {
            get{ return PlayerPrefs.GetString(thicc); }
            set{ PlayerPrefs.SetString(thicc, value); }
        }


    }




}
