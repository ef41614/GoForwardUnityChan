using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public AudioClip block;			// 効果音：ブロックが[地面/他のブロック]の上に載った時の接触音
	private AudioSource audioSource;	// オーディオソース
//	private GameObject gameManager; //ゲームマネージャーを参照する

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		//オーディソース取得
		audioSource = this.gameObject.GetComponent<AudioSource>();
//		gameManager = GameObject.Find ("GameManager");
		Debug.Log ("スタートするぜ");

	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {


	}

	//####################################  other  ####################################
	public void PutCube(){
		audioSource.PlayOneShot (block);
		Debug.Log ("鳴らしたよ");
	}
	//#################################################################################

}
// End