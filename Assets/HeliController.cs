using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour {

	// はしごのPrefab
	public GameObject HashigoSetPrefab;

	// 時間計測用の変数
	private float delta = 0;

	// はしごの生成間隔
	private float span = 0.2f;
	private int TotalH = 0;

	// はしごの生成位置：X座標
	private float genPosX = -4;

	// はしごの生成位置オフセット（Y）
	private float offsetY = 15.3f;
	// はしごの縦方向の間隔
	private float spaceY = 2.7f;

	//はしごの生成位置オフセット（X）
	private float offsetX = -4.5f;
	// はしごの横方向の間隔
	private float spaceX = 0.4f;

	// はしごの生成個数の上限
	private int maxBlockNum = 4;

	private float startLine = -7f;
	public enum FLY_KIND{	//飛行状態の種類を定義
		HOVERING, //停止
		ADVANCE,  //前進
		RETREAT,  //後退
	}

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {


	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {
		//時間経過に応じて上下
		float speed = 0.5f;
		transform.position = new Vector2(transform.position.x,4f + Mathf.Sin(Time.time * speed)*0.5f);

		// ヘリがAボタン押下時に前進する
		if(Input.GetKey(KeyCode.A)){
			transform.position += new Vector3(1 * Time.deltaTime,0,0);
		}

//		if(Input.GetKey(KeyCode.S)){
			// 指定した数だけはしごセットを生成する

		// 強制的にヘリコプターを後ろに下げる（Aボタンが押されていない時＆＆スタートラインより前進している場合）
//		if (Input.GetKeyUp (KeyCode.A) && startLine < transform.position.x) {
		if (startLine < transform.position.x) {
			transform.position += new Vector3(-0.7f * Time.deltaTime,0,0);
		}

		Debug.Log ("TotalH :" + TotalH);

		this.delta += Time.deltaTime;

		if(TotalH < 4){

		// span秒以上の時間が経過したかを調べる
		if (this.delta > this.span) {
			this.delta = 0;

			// 指定した数だけはしごセットを生成する
			for (int i = 0; i < 1; i++) {
				// はしごの生成
				GameObject go = Instantiate (HashigoSetPrefab) as GameObject;
				go.transform.position = new Vector3 (this.genPosX, this.offsetY + i * this.spaceY, 0);
					TotalH++;
			}
			// 次のはしごまでの生成時間を決める
			this.span = this.offsetX + this.spaceX * 20.5f;
		}
		}
	}

	//####################################  other  ####################################
	//#################################################################################

}
// End