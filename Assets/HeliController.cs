using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour {

	// はしごのPrefab
	public GameObject HashigoSetPrefab;

	// はしごロープのPrefab
	public GameObject RopePrefab;

	private GameObject Ladder0;
	private GameObject Ladder1;
	private GameObject Ladder2;
	private GameObject Ladder3;

	public GameObject Heli1;
	private GameObject Obj;

	private bool setRope = false;

	// 時間計測用の変数
	private float delta = 0;

	// はしごの生成位置オフセット（Y）
	private float offsetY = 15.3f;
	// はしごの縦方向の間隔
	private float spaceY = -2.7f;


	private float startLine = -7f;
	public enum FLY_KIND{	//飛行状態の種類を定義
		HOVERING, //停止
		ADVANCE,  //前進
		RETREAT,  //後退
	}

	GameObject unitychan; // Unityちゃんそのものが入る変数  
	UnityChanController Uscript; // UnityChanControllerが入る変数

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {

		unitychan = GameObject.Find ("UnityChan2D"); //Unityちゃんをオブジェクトの名前から取得して変数に格納する
		Uscript = unitychan.GetComponent<UnityChanController>(); //unitychanの中にあるUnityChanControllerを取得して変数に格納する

	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {
		//時間経過に応じて上下
		float speed = 0.5f;
		transform.position = new Vector2 (transform.position.x, 4f + Mathf.Sin (Time.time * speed) * 0.5f);

		// ヘリがAボタン押下時に前進する
//		if (Input.GetKey (KeyCode.A)) {
		if (Uscript.UniMode == 1) {
			transform.position += new Vector3 (1.5f * Time.deltaTime, 0, 0);
		}


		// 強制的にヘリコプターを後ろに下げる（Aボタンが押されていない時＆＆スタートラインより前進している場合）
		if (startLine < transform.position.x) {
			transform.position += new Vector3 (-0.7f * Time.deltaTime, 0, 0);
		}

		this.delta += Time.deltaTime;

		Ladder0 = GameObject.Find ("Ladder Part 0");
		Ladder1 = GameObject.Find ("Ladder Part 1");
		Ladder2 = GameObject.Find ("Ladder Part 2");
		Ladder3 = GameObject.Find ("Ladder Part 3");


		// はしごロープを生成する

		if (Ladder0 != null) {
			// exist
			Debug.Log ("存在している");
			if (setRope == false) {

				GameObject childObject0 = Instantiate (RopePrefab) as GameObject;
				childObject0.transform.parent = Ladder0.transform;

				GameObject childObject1 = Instantiate (RopePrefab) as GameObject;
				childObject1.transform.parent = Ladder1.transform;

				GameObject childObject2 = Instantiate (RopePrefab) as GameObject;
				childObject2.transform.parent = Ladder2.transform;

				GameObject childObject3 = Instantiate (RopePrefab) as GameObject;
				childObject3.transform.parent = Ladder3.transform;

				setRope = true;

			}
		} else {
			setRope = false;
		}
		}

	//####################################  other  ####################################
	//#################################################################################

}
// End