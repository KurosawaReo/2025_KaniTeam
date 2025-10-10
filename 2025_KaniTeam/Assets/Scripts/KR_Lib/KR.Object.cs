/*
   - KR_Lib.Object -
   ver.2025/10/03
*/
using UnityEngine;
using System;

using KR_Lib.Position;
using KR_Lib.Variable;
using KR_Lib.Inspector;

/// <summary>
/// オブジェクト管理用の追加機能.
/// </summary>
namespace KR_Lib.Object
{
    /// <summary>
    /// コンポーネント集.
    /// </summary>
    public class Components
    {
        public SpriteRenderer sr;
        public Rigidbody2D    rb2d;
        public Animator       animr;
    }

    /// <summary>
    /// オブジェクトデータ(2D用)
    /// このMyObjectを継承することでMonoBehaviourの機能も使える.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObjectKR : MonoBehaviour
    {
    //▼コンポーネント.
        protected Components cmp;

    //▼private変数.
        private Vector2 facing;   //向いてる方向.
        private bool    isActive; //有効かどうか.
        private bool    isFlip;   //反転するかどうか.
        private bool    isGround; //着地しているか.

    //▼private変数.[入力可]
        [Header("- ObjectKR -")] 
        [Space(4)]
        [SerializeField] 
            private bool    isAutoInit = true;                      //自動で値を初期化するか.
        [InspectorDisable("isAutoInit"), SerializeField] 
            private Vector2 initPos;                                //初期座標.
        [InspectorDisable("isAutoInit"), SerializeField] 
            private Vector2 initFacing;                             //初期向き.
        [InspectorDisable("isAutoInit"), SerializeField] 
            private bool    initActive = true;                      //有効かどうか.
        [InspectorDisable("isAutoInit"), SerializeField] 
            private bool    initFlip   = false;                     //反転するかどうか.

        [SerializeField] private Vector2 size = new Vector2(1, 1);  //当たり判定サイズ.
        [SerializeField] private IntR    hp;                        //体力.

        //��public.
        //set, get.
        public Vector2 Pos { 
            get => transform.position;
            protected set => transform.position = value;
        }
        public float PosX {
            get => transform.position.x;
            protected set => transform.position = new Vector2(value, PosY);
        }
        public float PosY {
            get => transform.position.y;
            protected set => transform.position = new Vector2(PosX, value);
        }
        public Vector2 Vel {
            get => cmp.rb2d.linearVelocity;
            protected set => cmp.rb2d.linearVelocity = value;
        }
        public float VelX {
            get => cmp.rb2d.linearVelocity.x;
            protected set => cmp.rb2d.linearVelocity = new Vector2(value, VelY);
        }
        public float VelY {
            get => cmp.rb2d.linearVelocity.y;
            protected set => cmp.rb2d.linearVelocity = new Vector2(VelX, value);
        }
        public float Gravity {
            get => cmp.rb2d.gravityScale;
            protected set => cmp.rb2d.gravityScale = value;
        }
        public Vector2 Size {
            get => size;
        }
        public Vector2 Facing {
            get => facing;
        }
        public Color Color {
            get => cmp.sr.color;
            protected set => cmp.sr.color = value;
        }
        public int Hp {
            get => hp.Now;
            protected set => hp.Now = value;
        }
        public bool IsActive {
            get => isActive; 
            protected set {
                isActive = value;
                gameObject.SetActive(value); //設定.
            }
        }
        public bool IsFlip {
            get => isFlip;
            protected set {
                isFlip = value;
                cmp.sr.flipX = value; //設定.
            }
        }
        public bool IsGround
        {
            get => isGround;
            protected set => isGround = value;
        }

        /// <summary>
        /// ObjectKR初期化.
        /// </summary>
        public void InitObjKR()
        {
            //実体生成.
            cmp = new Components();
            //コンポーネント取得.
            cmp.sr    = GetComponent<SpriteRenderer>();
            cmp.rb2d  = GetComponent<Rigidbody2D>();    
            cmp.animr = GetComponent<Animator>();
            //サイズ取得.
            size = new Vector2(cmp.sr.bounds.size.x * size.x, cmp.sr.bounds.size.x * size.y);
            //自動初期化モードなら.
            if (isAutoInit)
            {
                initPos    = transform.position;     //初期座標登録.
                initFacing = new Vector2(1, 0);      //右.
                initActive = gameObject.activeSelf;  //アクティブ状態取得.
                initFlip   = cmp.sr.flipX;           //反転状態取得.
            }
            else
            {
                ResetObjKR(); //リセット処理.
            }
        }

        /// <summary>
        /// ObjectKRリセット.
        /// </summary>
        public void ResetObjKR()
        {
            IsActive = initActive;
            isGround = false;
            ResetPos();
            hp.Reset();
        }
        /// <summary>
        /// 位置だけリセット.
        /// </summary>
        public void ResetPos()
        {
            Pos    = initPos;
            facing = initFacing;
            IsFlip = initFlip;
        }

        /// <summary>
        /// 移動処理.
        /// </summary>
        public void Move(Vector2 vec, float speed)
        {
            //移動.
            Pos += vec * speed * Time.deltaTime;
            //方向の保存.
            facing = vec;
        }
        /// <summary>
        /// 移動処理.
        /// </summary>
        /// <param name="lim">限界座標</param>
        public void Move(Vector2 vec, float speed, LBRT lim)
        {
            //移動.
            Pos += vec * speed * Time.deltaTime;
            Pos = PS_Func.FixPosInArea(Pos, size, lim); //移動限界.
            //方向の保存.
            facing = vec;
        }

        /// <summary>
        /// 移動速度を与える.
        /// </summary>
        public void AddForce(Vector2 vec, float pow)
        {
            cmp.rb2d.AddForce(vec * pow);
        }
        /// <summary>
        /// 移動速度を与える.
        /// </summary>
        /// <param name="mode">力を与えるモード</param>
        public void AddForce(Vector2 vec, float pow, ForceMode2D mode)
        {
            cmp.rb2d.AddForce(vec * pow, mode);
        }

        /// <summary>
        /// ジャンプ可能ならジャンプする.
        /// </summary>
        public bool TryJump(float pow)
        {
            //着地しているなら.
            if (isGround)
            {
                //瞬間的に上に力を加える.
                AddForce(Vector2.up, pow, ForceMode2D.Impulse);

                isGround = false; //ジャンプしている.
                return true;      //ジャンプ成功.
            }
            return false; //ジャンプ失敗.
        }

        /// <summary>
        /// Animatorのパラメーターをセット(Trigger)
        /// </summary>
        public void SetAnimParam(string name)
        {
            cmp.animr.SetTrigger(name);
        }
        /// <summary>
        /// Animatorのパラメーターをセット(Bool)
        /// </summary>
        public void SetAnimParam(string name, bool value)
        {
            cmp.animr.SetBool(name, value);
        }
        /// <summary>
        /// Animatorのパラメーターをセット(Int)
        /// </summary>
        public void SetAnimParam(string name, int value)
        {
            cmp.animr.SetInteger(name, value);
        }
        /// <summary>
        /// Animatorのパラメーターをセット(Float)
        /// </summary>
        public void SetAnimParam(string name, float value)
        {
            cmp.animr.SetFloat(name, value);
        }
    }

    /// <summary>
    /// prefabを扱う用.
    /// </summary>
    [Serializable] public class PrefabKR
    {
        [SerializeField] GameObject prefab; //prefabデータ.
        [SerializeField] GameObject inObj;  //prefabを入れる所.

        /// <summary>
        /// prefab新規作成.
        /// </summary>
        /// <returns>作成したprefab</returns>
        public GameObject NewPrefab()
        {
            var obj = UnityEngine.Object.Instantiate(prefab); //生成.
            obj.transform.SetParent(inObj.transform);         //親オブジェクトを設定.
            return obj;
        }
    }
}