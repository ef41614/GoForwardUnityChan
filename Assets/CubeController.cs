using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	// キューブの移動速度
	private float speed = -0.2f;

	// 消滅位置
	private float deadLine = -10;

	private GameObject gameManager; //ゲームマネージャーを参照する

//	public AudioClip block;			// 効果音：ブロックが[地面/他のブロック]の上に載った時の接触音
//	private AudioSource audioSource;	// オーディオソース

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
//		audioSource = GetComponent<AudioSource> ();
		gameManager = GameObject.Find ("GameManager");
	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {
		// キューブを移動させる
		transform.Translate(this.speed, 0, 0);

		// 画面外に出たら破棄する
		if (transform.position.x < this.deadLine) {
			Destroy (gameObject);
		}
	}

	//####################################  other  ####################################

	// 衝突処理
//	void OnTriggerEnter2D (Collider2D other) {
	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log ("当たったよ");
		if ( (other.gameObject.tag =="Cube")  || (other.gameObject.tag == "ground") ) {
			gameManager.GetComponent<GameManager> ().PutCube ();
		}
		
	}
	//#################################################################################
}
// End