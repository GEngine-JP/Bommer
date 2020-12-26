using Script.Controller;
using UnityEngine;

namespace Script.Go
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// </summary>
    public class DoorWall : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.tag);
            if (!other.gameObject.CompareTag("Player")) return;
            if (UIController.enemyCount == 0)
            {
                //游戏通关
                //GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameContorller> ().loadLevel();
                Debug.Log("游戏通关");
            }
        }
    }
}