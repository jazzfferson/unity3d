using UnityEngine;
using System.Collections;

public class DataBase : MonoBehaviour {

    public static DataBase instance;
    UserInfo[] arrayUserInfo;

	void Start () {

        if (instance == null) { instance = this; }
        arrayUserInfo = new UserInfo[5];
        for (int i = 0; i < arrayUserInfo.Length; i++)
        {
            arrayUserInfo[i] = new UserInfo("User", 0,"None",i);
        }
    
	}
	
	void Update () {
	
	}

    /// <summary>
    /// Index vai de 0 á 4.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    /// <param name="index"></param>
    public void SetRecordInfo(string name, int score,string nation,int index)
    {
        arrayUserInfo[index].Name = name;
        arrayUserInfo[index].Score = score;
        arrayUserInfo[index].Nation = nation;

    }

     /// <summary>
    /// Index vai de 0 á 4.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    /// <param name="index"></param>
    public void GetRecordInfo(out string name, out int userScore,out string nation,int index)
    {
        name = arrayUserInfo[index].Name;
        userScore = arrayUserInfo[index].Score;
        nation = arrayUserInfo[index].Nation;
    }

    public class UserInfo
    {

        int _index;

        public UserInfo(string name,int score,string nation,int index)
        {

            _index = index;
            Score = score;
            Name = name;
            Nation = nation;

        }

        public int Score
        {
            #region Get
            get
            {
                if (PlayerPrefs.HasKey("score" + _index))
                {
                    return PlayerPrefs.GetInt("score" + _index);
                }
                else
                {
                    PlayerPrefs.SetInt("score" + _index, 0);
                    return PlayerPrefs.GetInt("score" + _index);
                }
            }
            #endregion

            #region Set

            set
            {
                PlayerPrefs.SetInt("score" + _index, value);
            }
            #endregion
        }

        public string Name
        {
            #region Get
            get
            {
                if (PlayerPrefs.HasKey("name" + _index))
                {
                    return PlayerPrefs.GetString("name" + _index);
                }
                else
                {
                    PlayerPrefs.SetInt("name" + _index, 0);
                    return PlayerPrefs.GetString("name" + _index);
                }
            }
            #endregion

            #region Set

            set
            {

                PlayerPrefs.SetString("name" + _index, value);
            }
            #endregion
        }

        public string Nation
        {
            #region Get
            get
            {
                if (PlayerPrefs.HasKey("nation" + _index))
                {
                    return PlayerPrefs.GetString("nation" + _index);
                }
                else
                {
                    PlayerPrefs.SetInt("nation" + _index, 0);
                    return PlayerPrefs.GetString("nation" + _index);
                }
            }
            #endregion

            #region Set

            set
            {

                PlayerPrefs.SetString("nation" + _index, value);
            }
            #endregion
        }
    }
    
  
}


