using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

	//CoinPrefabを入れる
	public GameObject CoinPrefab;

	public GameObject EnemyPrefab;

	// アイテム生成の時間間隔
	float span = 1.0f; 
	float delta = 0;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {

	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

		this.delta += Time.deltaTime;
		if(this.delta > this.span){
			this.delta = 0;

			// 出現率の設定
			int appear = Random.Range (0, 10);
			if(appear <= 6){
				//コイン生成
				GameObject go = Instantiate(CoinPrefab) as GameObject;
				int py = Random.Range(-4,2);
				go.transform.position = new Vector3(10, py, 0);
			}

			// appear = 7 の時は何も作らない

			if (appear >= 8) {
				//敵の生成
				GameObject go = Instantiate(EnemyPrefab) as GameObject;
				int py = Random.Range(-4,0);
				go.transform.position = new Vector3(10, py, 0);
			}
		}

	}

	//####################################  other  ####################################
	//#################################################################################

}
// End