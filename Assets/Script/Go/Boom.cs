using Script.Controller;
using UnityEngine;

namespace Script.Go
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// </summary>
    public class Boom : MonoBehaviour
    {
        public GameObject door;

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, 0.5f);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().PlayerBuff(0, -1);
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Door"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MapController>().EnemyGenerator(5);
            }
            else if (other.gameObject.CompareTag("DoorWall"))
            {
                Instantiate(door, other.gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}