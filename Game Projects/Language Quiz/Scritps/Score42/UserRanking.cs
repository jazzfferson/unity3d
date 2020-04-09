using UnityEngine;
using System.Collections;

public class UserRanking : MonoBehaviour {

    public UILabel userNameLabel, scoreLabel, rankingPositionLabel;

    public void SetValues(string name,string score,string position)
    {
        userNameLabel.text = name;
        scoreLabel.text = score;
        rankingPositionLabel.text = position;
    }
}
