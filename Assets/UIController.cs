using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	// ゲームオーバーテキスト
	private GameObject gameOverText;

	// 走行距離テキスト
	private GameObject runLengthText;

	// はしごボタン
	private GameObject HashigoB;

	// 走った距離
	private float len = 0;

	// 走る速度
	private float speed = 0.03f;

	// ゲームオーバーの判定
	private bool isGameOver = false;

	GameObject unitychan; // Unityちゃんそのものが入る変数  
	UnityChanController Uscript; // UnityChanControllerが入る変数

	GameObject HashigoMaker;
	RopeLadder2DController Rscript;

	// ################################################################################
	void Awake(){
		this.HashigoB = GameObject.Find ("BtnCreateLadderStep");		
	}

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		// シーンビューからオブジェクトの実体を検索する
		this.gameOverText = GameObject.Find("GameOver");
		this.runLengthText = GameObject.Find ("RunLength");

		// 始めは、はしごボタンを非表示にする
		HashigoB.SetActive (false);

		unitychan = GameObject.Find ("UnityChan2D"); //Unityちゃんをオブジェクトの名前から取得して変数に格納する
		Uscript = unitychan.GetComponent<UnityChanController>(); //unitychanの中にあるUnityChanControllerを取得して変数に格納する

		HashigoMaker = GameObject.Find ("Ladder Root");
		Rscript = HashigoMaker.GetComponent<RopeLadder2DController> ();
	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {
		if (this.isGameOver == false) {
			// 走った距離を更新する
			this.len += this.speed;

			// 走った距離を表示する
			this.runLengthText.GetComponent<Text> ().text = "Distance: " + len.ToString ("F2") + "m";
		}

		// ゲームオーバーになった場合
		if(this.isGameOver){
			// クリックされたらシーンをロードする
			if(Input.GetMouseButtonDown(0)){
				//GameSceneを読み込む
				SceneManager.LoadScene("GameScene");
			}
		}

		bool setHB = Uscript.callHashigoB;
		if(setHB == true){
			// 条件を満たしたら、はしごボタンを再表示する
			HashigoB.SetActive (true);

			// コインポイントを減算
			Uscript.CoinPoint -= 10;

			// コインポイントを表示
			Uscript.pointText.GetComponent<Text> ().text =  Uscript.CoinPoint + "pt";
		}


		// Hボタン押下時にはしごを垂らす
		if (Input.GetKey (KeyCode.H)) {
			Rscript.CreateLadder ();
			// はしごボタンを非表示する
			HashigoB.SetActive (false);
		}
	}

	//####################################  other  ####################################
		public void GameOver() {
			// ゲームオーバーになったときに、画面上にゲームオーバーを表示する
			this.gameOverText.GetComponent<Text>().text = "GameOver";
			this.isGameOver = true;
		}
	//#################################################################################

}
// End