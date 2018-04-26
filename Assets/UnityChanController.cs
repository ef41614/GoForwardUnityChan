using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
	//アニメーションするためのコンポーネントを入れる
	Animator animator;

	//Unityちゃんを移動させるコンポーネントを入れる
	Rigidbody2D rigid2D;

	// 地面の位置
	private float groundLevel = -3.0f; 

	// ジャンプの速度の減衰
	private float dump = 0.8f;

	// ジャンプの速度
	float jumpVelocity = 20;

	// ゲームオーバーになる位置
	private float deadLine = -9;

	//スコアを表示するテキスト
	public GameObject pointText;
	//コインのポイント
	public int CoinPoint = 0;

	public GameObject Heli1;
	public bool callHashigoB = false;
	public bool holdHashigo = false;

	private GameObject Ladder2;
	private float startLine = -2.9f;

	GameObject HashigoMaker;
	RopeLadder2DController Rscript;

	private float delta = 0;

	public enum UNI_KIND{	//Unityちゃんの状態の種類を定義
		RUN,           //走っている
		HOLD_HASHIGO,  //はしごをつかんでいる
	}
	private UNI_KIND uniKind;

	//0:走る、1:空中制止
	public int UniMode = 0;

	private Vector2 player_pos;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		// アニメ―タのコンポーネントを取得する
		this.animator = GetComponent<Animator>();
		// rigidbody2Dのコンポーネントを取得する
		this.rigid2D = GetComponent<Rigidbody2D> (); 

		//シーン中のCoinPointオブジェクトを取得
		this.pointText = GameObject.Find("CoinPoint");

		this.Heli1 = GameObject.Find ("Heli1");

		Ladder2 = GameObject.Find ("Ladder Part 2");

		HashigoMaker = GameObject.Find ("Ladder Root");
		Rscript = HashigoMaker.GetComponent<RopeLadder2DController> ();
	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

		// 走るアニメーションを再生するために、Animatorのパラメータを調整する
		this.animator.SetFloat("Horizontal", 1);

		// ------MODE:0 走る  ここから-------------------------------------------------------------------------

		if (UniMode == 0) {
			Debug.Log ("UniMode 0: 走り（通常）モード" + UniMode);
			//着地しているかどうかを調べる
			bool isGround = (transform.position.y > this.groundLevel) ? false : true;
			this.animator.SetBool ("isGround", isGround);

			// ジャンプ状態のときにはボリュームを０にする
			GetComponent<AudioSource> ().volume = (isGround) ? 1 : 0;

			// 着地状態でクリックされた場合
			//		if (Input.GetMouseButtonDown (0) && isGround) {
			if (((Input.GetMouseButtonDown (0)) || (Input.GetButtonDown ("Jump"))) && isGround) {
				// 上方向の力をかける
				this.rigid2D.velocity = new Vector2 (0, this.jumpVelocity);
			}

			// クリックをやめたら上方向への速度を減速する
			//		if (Input.GetMouseButton (0) == false) {
			if (((Input.GetMouseButton (0)) || (Input.GetButton ("Jump"))) == false) {
				if (this.rigid2D.velocity.y > 0) {
					this.rigid2D.velocity *= this.dump;
				}
			}
		}

		// はしごをつかんでいたら → 空中に制止させる
		if (holdHashigo == true) {
			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			//			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePosition;
			Vector2 g2 = new Vector2(0, 0) ;
			Physics.gravity = g2;
			this.transform.position = Ladder2.transform.position;
			UniMode = 1;
		}

		//---------------MODE:0 走る ここまで----------------------------------------------------------

		// ------MODE:1 空中制止  ここから-------------------------------------------------------------------------

		if (UniMode == 1) {
			Debug.Log ("UniMode 1: 空中制止" + UniMode);
			// Unityちゃんが画面中央まで来たら or ジャンプボタン押下されたら → はしごから離れて降りる
			if ((startLine +3 <= Ladder2.transform.position.x)||((Input.GetMouseButtonDown (0)) || (Input.GetButtonDown ("Jump")))) {
				holdHashigo = false;
				this.transform.parent = null;
				GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
				//			this.transform.position = new Vector2(startLine, 2) ;
				GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
				rigid2D.AddForce (new Vector2 (0, 130));
				//			rigidbody.AddForce(new Vector3(0, 5,0));

				// はしごを消去する
					Rscript.DestroyLadder ();
				// 上方向の力をかける
//				this.rigid2D.velocity = new Vector2 (0, 1);
//				if (this.transform.position.y <= -3.6f + 0.1f) {
					UniMode = 2;
//				}
			}
		}
		//---------------MODE:1 空中制止 ここまで----------------------------------------------------------

		// ------MODE:2 はしごから落下-着地  ここから-------------------------------------------------------------------------

		if (UniMode == 2) {
			holdHashigo = false;
			this.transform.parent = null;
			//着地しているかどうかを調べる
			bool isGround = (transform.position.y > this.groundLevel) ? false : true;
			this.animator.SetBool ("isGround", isGround);
//			this.transform.position.y -= 0.01f;
//			transform.Translate (new Vector2(rigid2D.velocity.x, -0.01f));
			transform.Translate (new Vector2(0, -0.01f));

			if (isGround == true) {
				UniMode = 0;
			}
			Debug.Log ("isGround・・・" + isGround);
//			if (this.transform.position.y <= -3.6f + 0.1f) {
//				UniMode = 0;
//			}
		}

		// デッドラインを超えた場合ゲームオーバーにする
		if (transform.position.x < this.deadLine) {
			// UIControllerのGameover関数を呼び出して画面上に「Gameover」を表示する
			GameObject.Find ("Canvas").GetComponent<UIController> ().GameOver ();

			// ユニティちゃんを破棄する
			Destroy(gameObject);
		}

		// はしごボタン表示の条件 
		Ladder2 = GameObject.Find ("Ladder Part 2");
		if (CoinPoint >= 10 && Heli1.transform.position.x <= -7F && callHashigoB == false && Ladder2 == false) {
			callHashigoB = true;
		} else {
			callHashigoB = false;
		}



		Debug.Log ("holdHashigo" + holdHashigo);
		Debug.Log ("UniMode" + UniMode);

//		Debug.Log ("Uni-Y" + transform.position.y);

		player_pos = transform.position; //プレイヤーの位置を取得

		player_pos.y = Mathf.Clamp(player_pos.y, -3.6f, 10f); //y位置が常に範囲内か監視
		transform.position = new Vector2(player_pos.x, player_pos.y); //範囲内であれば常にその位置がそのまま入る

	}

	//####################################  other  ####################################
	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter2D(Collider2D other) {               

		//コインに衝突した場合
		if (other.gameObject.tag == "Coin") {

			// コインポイントを加算
			this.CoinPoint += 1;

			//ScoreText獲得した点数を表示
			this.pointText.GetComponent<Text> ().text =  this.CoinPoint + "pt";

			//接触したコインのオブジェクトを破棄
			Destroy (other.gameObject);
		}

		//はしごに衝突した場合
		if (other.gameObject.tag == "Hashigo" && (this.transform.position.x < -6)) {
			Debug.Log("はしごに触ったよ");
			// 重力指定方向 (右方向)
			Vector3 g = new Vector3(0, 0, 0) ;
			Vector2 g2 = new Vector2(0, 0) ;
			Physics.gravity = g2;
			// 位置 固定
			//			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePosition;
			//★			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			//★			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY;

			//★			this.transform.position = Ladder2.transform.position;

			//座標移動固定
			//			gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePosition;

			holdHashigo = true;
			this.transform.parent = Ladder2.transform;

		}
			
	}

//	void Clamp()
//	{
//		player_pos = transform.position; //プレイヤーの位置を取得

//		player_pos.y = Mathf.Clamp(player_pos.y, -5.4f, 15f); //y位置が常に範囲内か監視
//		transform.position = new Vector2(player_pos.x, player_pos.y); //範囲内であれば常にその位置がそのまま入る
//	}
	//#################################################################################

}
// End