using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public AudioClip block;			// 効果音：ブロックが[地面/他のブロック]の上に載った時の接触音
	public AudioClip gameoverSE;
	public AudioClip popSE;      // 効果音：踏みつけ
	public AudioClip damage;      // 効果音：ダメージ
	public AudioClip getCoin;      // 効果音：コインを取った時のSE
	private AudioSource audioSource;	// オーディオソース


	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		//オーディソース取得
		audioSource = this.gameObject.GetComponent<AudioSource>();

		Debug.Log ("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ ゲーム スタート ！！■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");

	}
		
	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

	}

	//####################################  other  ####################################
	public void PutCube(){
		audioSource.PlayOneShot (block);
	}

	public void popEnemy(){
		audioSource.PlayOneShot (popSE);
		Debug.Log ("敵を踏んだよ");
	}

	public void hitEnemy(){
		audioSource.PlayOneShot (damage);
		Debug.Log ("アウチいー");
	}

	public void GetCoin(){
		audioSource.PlayOneShot (getCoin);
		Debug.Log ("取ったどー");
	}

	public void GameOver(){
		audioSource.PlayOneShot (gameoverSE);
		Debug.Log ("ゲームオーバー...そんなあ");
	}
	//#################################################################################

}
// End