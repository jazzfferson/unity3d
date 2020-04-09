using UnityEngine;
using System.Collections;

public class Score {

    int _indexFase;
    public Score(int indexFase)
    {
        _indexFase = indexFase;
    }
    public int FaseScore
    {
        #region Get
        get
        {
            if (PlayerPrefs.HasKey("score" + _indexFase))
            {
                return PlayerPrefs.GetInt("score" + _indexFase);
            }
            else
            {
                PlayerPrefs.SetInt("score" + _indexFase, 0);
                return PlayerPrefs.GetInt("score" + _indexFase);
            }
        }
        #endregion

        #region Set

        set
        {

            PlayerPrefs.SetInt("score" + _indexFase, value);
            PlayerPrefs.Save();
        }
        #endregion
    }
}
