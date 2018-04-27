using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControll : MonoBehaviour {

	// 消滅位置
	private float deadLine = -10;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {

		// 回転開始の角度
//		this.transform.Rotate(0,Random.Range(0,360),0);

	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

		// 回転
//		this.transform.Rotate(0,3,0);

		// 左へ移動
		//		transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
		transform.Translate(-0.15f, 0, 0);

		// 画面外に出たら廃棄
		if(transform.position.x < deadLine){
			Destroy(gameObject);
		} 

	}

	//####################################  other  ####################################
	//#################################################################################

}
// End