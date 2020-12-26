using System.Collections;
using Script.Controller;
using UnityEngine;

namespace Script.Go
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// </summary>
    public class Bomb : MonoBehaviour
    {
        //爆炸特效
        public GameObject boom;

        //爆炸范围
        private int bombScope;

        //引爆时间
        private float bombFireTime;

        public void InitBomb(int scope, float fireTime)
        {
            bombScope = scope;
            bombFireTime = fireTime;
            StartCoroutine(StartBoom());
        }

        void OnTriggerExit2D(Collider2D other)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        private void NewBoom(Vector2 ve)
        {
            for (var i = 1; i <= bombScope; i++)
            {
                if (MapController.IsSuperWallPos((Vector2) transform.position + ve * i))
                {
                    break;
                }

                Instantiate(boom, (Vector2) transform.position + ve * i, Quaternion.identity);
            }
        }

        private IEnumerator StartBoom()
        {
            yield return new WaitForSeconds(bombFireTime);
            Instantiate(boom, transform.position, Quaternion.identity);
            NewBoom(Vector2.up);
            NewBoom(Vector2.down);
            NewBoom(Vector2.left);
            NewBoom(Vector2.right);
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}