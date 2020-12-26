using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// 负责控制游戏关卡生成，生成主角 ， 判定胜利和失败
    /// </summary>
    public class GameContorller : MonoBehaviour
    {
        // 仅仅用于测试
        public InputField[] test;

        public GameObject player;

        private int gameLevel;

        public int wallCount;

        public int enemyCount;

        public int xitm;

        public int yitm;

        private GameObject gamePlayer;

        public void ExitGame()
        {
            Application.Quit();
        }

        void Start()
        {
            InitGame();
            GetComponent<UIController>().StartViewWithIndex(0);
        }

        //开始下一关
        public void StartGame()
        {
            StartCoroutine(LoadLevel());
        }

        //关卡控制器
        private void LevelContorller(int levelCount)
        {
            xitm = 6 + 2 * (levelCount / 3);
            yitm = 3 + 2 * (levelCount / 3);
            if (xitm > 16)
            {
                xitm = 16;
            }

            if (yitm > 9)
            {
                yitm = 9;
            }

            FollowPlayer.xitm = xitm;
            FollowPlayer.yitm = yitm;

            //x表示地图所有空位的百分比 0-1之间 如：0.2 表示 取整个地图20%的空格子
            float x = (0.3f + (levelCount * 0.07f));
            if (x > 0.7f)
            {
                x = 0.7f;
            }

            wallCount = (int) (xitm * yitm * x) + 5;

            enemyCount = (int) (levelCount * 1.25f + 1);

            //调用地图控制器的初始化地图方法
            GetComponent<MapController>().InitMap(wallCount, enemyCount, xitm, yitm);

            //初始化猪脚
            InitPlayer();

            //关闭计时器
            StopCoroutine($"GameTimeCount");

            //开启计时器
            StartCoroutine(nameof(GameTimeCount), (int) (80 + levelCount * 30f));

            //调出游戏UI
            GetComponent<UIController>().StartViewWithIndex(1);
        }

        //加载关卡
        private IEnumerator LoadLevel()
        {
            gameLevel++;
            UIController.gameLevel = gameLevel;
            GetComponent<UIController>().StartViewWithIndex(2);
            yield return new WaitForSeconds(2.5f);
            LevelContorller(gameLevel);
        }

        public void InitPlayer()
        {
            //生成主角
            if (gamePlayer == null)
            {
                gamePlayer = Instantiate(player, new Vector2(-(xitm + 1), yitm + 1), Quaternion.identity);
                //初始化player属性
                gamePlayer.GetComponent<PlayerController>().InitPlayer(20, 3, 2, 1);
            }
            else
            {
                gamePlayer.transform.position = new Vector2(-(xitm + 1), yitm + 1);
            }

            FollowPlayer.player = gamePlayer.transform;
        }

        //游戏结束
        public void GameOver()
        {
            if (gamePlayer != null)
            {
                Destroy(gamePlayer);
            }

            GetComponent<UIController>().StartViewWithIndex(3);
        }

        //重新开始游戏
        public void GameReverse()
        {
            InitGame();
            StartGame();
        }

        //初始化属性
        public void InitGame()
        {
            UIController.enemyCount = 0;
            gameLevel = 0;
            if (gamePlayer != null)
            {
                Destroy(gamePlayer);
            }
        }

        private IEnumerator GameTimeCount(int count)
        {
            //为游戏时间赋值
            UIController.gameTime = count;
            //倒计时游戏时间
            while (true)
            {
                yield return new WaitForSeconds(1);
                UIController.gameTime--;
                if (UIController.gameTime <= 0)
                {
                    //游戏结束
                    GameOver();
                    break;
                }
            }
        }
    }
}