using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashigController : MonoBehaviour {

	// はしごの移動速度
	private float speed = -0.2f;

	// 消滅位置
	private float deadLine = -10;

	private float downCount = 0;
	private GameObject gameManager; //ゲームマネージャーを参照する



	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		//		audioSource = GetComponent<AudioSource> ();
		gameManager = GameObject.Find ("GameManager");

		// はしごを移動させる
//		transform.Translate(0, -1, 0);
	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {
		// はしごを移動させる
//		if(downCount < 3){
//		transform.Translate(0, -0.01f, 0);
//			transform.position += new Vector3(0, -0.01f, 0);
//			downCount = downCount + 0.05f;
//		}
		// 画面外に出たら破棄する
		if (transform.position.x < this.deadLine) {
			Destroy (gameObject);
		}
	}

	//####################################  other  ####################################

	// 衝突処理
	//	void OnTriggerEnter2D (Collider2D other) {
	void OnCollisionEnter(Collision other) {
		Debug.Log ("当たったよ");

	}
	//#################################################################################
}
// End