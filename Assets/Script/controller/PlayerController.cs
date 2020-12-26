using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// 玩家控制器
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        //炸弹
        public GameObject bomb;

        //玩家移动速度
        public float speed;

        //玩家生命值
        [FormerlySerializedAs("playerHP")] public int playerHp;

        //玩家放置炸弹的最大CD时间
        [FormerlySerializedAs("bombCDMax")] public float bombCdMax;

        //爆炸范围
        public int bombScope;

        //引爆时间
        private float bombFireTime;

        //主角动画
        private Animator playerAnimator;

        //玩家当前放置炸弹剩余CD时间
        [FormerlySerializedAs("bombCD")] public float bombCd;

        private Rigidbody2D rb2D;

        private float moveX, moveY;
        private static readonly int ToLeft = Animator.StringToHash("ToLeft");
        private static readonly int ToDown = Animator.StringToHash("ToDown");
        private static readonly int ToRight = Animator.StringToHash("ToRight");
        private static readonly int ToUp = Animator.StringToHash("ToUp");

        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (bombCd > 0)
            {
                bombCd -= Time.fixedDeltaTime;
            }
        }

        public void Update()
        {
            PlayerMove();
            Fire();
        }


        private IEnumerator Invincible(int waitTime)
        {
            gameObject.tag = "Untagged";
            for (var i = waitTime; i > 0; i--)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.25f);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.25f);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.25f);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.25f);
            }

            gameObject.tag = "Player";
        }

        //碰撞检测
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Door")) return;
            if (UIController.enemyCount <= 0)
            {
                //游戏通关
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameContorller>().StartGame();
            }
        }

        //2018-10-9 新增
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("ENEMY"))
            {
                PlayerBuff(0, -1);
            }
        }

        //主角初始化
        public void InitPlayer(float speedValue, int hpValue, int bombCDmaxValue, int bombscope)
        {
            speed = speedValue;
            playerHp = hpValue;
            bombCdMax = bombCDmaxValue;
            bombScope = bombscope;
            bombFireTime = 2;
            UIController.playerHp = playerHp;
        }

        private void Fire()
        {
            if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.J)) return;
            if (!(bombCd <= 0)) return;
            //播放放下炸弹音效
            GetComponent<AudioSource>().Play();

            var position = transform.position;
            var boom = Instantiate(bomb,
                new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)), transform.rotation);
            //激活炸弹
            boom.GetComponent<Bomb>().InitBomb(bombScope, bombFireTime);
            //重置CD
            bombCd = bombCdMax;
        }

        //玩家Buff控制方法 count：0 生命值buff  1 移动速度buff 2 炸弹CDbuff  3 炸弹伤害 4 爆炸范围  5 引爆时间
        public void PlayerBuff(int count, float value)
        {
            switch (count)
            {
                case 0:
                    playerHp += (int) value;
                    UIController.playerHp = playerHp;
                    //玩家死亡
                    if (playerHp <= 0)
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameContorller>().GameOver();
                        Destroy(gameObject);
                    }
                    else
                    {
                        StartCoroutine(Invincible(2));
                    }

                    break;
                case 1:
                    speed += value;
                    if (speed >= 8)
                    {
                        speed = 8;
                    }

                    break;
                case 2:
                    bombCdMax += value;
                    if (bombCdMax < 0.5f)
                    {
                        bombCdMax = 0.5f;
                    }

                    break;
//			case 3:
//				bombATK += value;
//				break;
                case 4:
                    bombScope += (int) value;
                    Debug.Log("bombscope" + bombScope);
                    break;
                case 5:
                    bombFireTime += (int) value;
                    if (bombFireTime < 0.5f)
                    {
                        bombFireTime = 0.5f;
                    }

                    break;
            }
        }


        //玩家移动方法
        private void PlayerMove()
        {
            moveX = 0;
            moveY = 0;
            if (Input.GetKey(KeyCode.W))
            {
                moveY = 1;
                playerAnimator.SetBool(ToLeft, false);
                playerAnimator.SetBool(ToDown, false);
                playerAnimator.SetBool(ToRight, false);
                playerAnimator.SetBool(ToUp, true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveX = -1;
                playerAnimator.SetBool(ToRight, false);
                playerAnimator.SetBool(ToUp, false);
                playerAnimator.SetBool(ToDown, false);
                playerAnimator.SetBool(ToLeft, true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveY = -1;
                playerAnimator.SetBool(ToRight, false);
                playerAnimator.SetBool(ToUp, false);
                playerAnimator.SetBool(ToLeft, false);
                playerAnimator.SetBool(ToDown, true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveX = 1;
                playerAnimator.SetBool(ToUp, false);
                playerAnimator.SetBool(ToLeft, false);
                playerAnimator.SetBool(ToDown, false);
                playerAnimator.SetBool(ToRight, true);
            }
            else
            {
                playerAnimator.SetBool(ToRight, false);
                playerAnimator.SetBool(ToUp, false);
                playerAnimator.SetBool(ToLeft, false);
                playerAnimator.SetBool(ToDown, false);
            }

            rb2D.MovePosition((Vector2) transform.position + new Vector2(moveX, moveY) * (speed * Time.deltaTime));
        }
    }
}