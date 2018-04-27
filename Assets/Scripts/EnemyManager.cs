using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	// 消滅位置
	private float deadLine = -10;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {

	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

		// 左へ移動
		transform.Translate(-0.15f, 0, 0);

		// 画面外に出たら廃棄
		if(transform.position.x < deadLine){
			Destroy(gameObject);
		} 

	}

	//####################################  other  ####################################
	public void DestroyEnemy(){
		Destroy (this.gameObject);
	}

	//#################################################################################

}
// End