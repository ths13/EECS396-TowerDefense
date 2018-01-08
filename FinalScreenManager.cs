using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScreenManager : MonoBehaviour
{

    public LoseScreen losing;
    public Spawn sp;
    public Base HomeBase;
    public Text winlose;

	// Use this for initialization
	void Start ()
	{
	    sp = FindObjectOfType<Spawn>();
	    losing = FindObjectOfType<LoseScreen>();
        HomeBase = FindObjectOfType<Base>();
	    winlose = losing.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

	    if (sp.GameOver && HomeBase.destroyed)
	    {
	        winlose.text = "You Lose!";
	        losing.gameObject.SetActive(true);
        }

	    else if (sp.GameOver)
	    {
	        winlose.text = "You Win!";
            losing.gameObject.SetActive(true);
        }
	}
}
