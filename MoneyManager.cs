using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public int CurrentMoney { get; private set; }
    private static Text _scoreText;

	// Use this for initialization
	void Start () {

	    _scoreText = GetComponent<Text>();
	    CurrentMoney = 2000;
	    UpdateMoney();
    }

    public void AddMoney(int value)
    {
        CurrentMoney = Mathf.Max(0, CurrentMoney + value);
        UpdateMoney();
    }


    private void UpdateMoney()
    {
        _scoreText.text = "Money: " + string.Format("{0}", CurrentMoney).PadLeft(5, '0');
    }

}