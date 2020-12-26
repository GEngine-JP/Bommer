using UnityEngine;

namespace Script
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// </summary>
    public class EnemyAI : MonoBehaviour
    {
        private Vector2 moveVector;
        private Rigidbody2D rod;
        private int moveingID;
        public float speed;

        public float weakTime;

        // Use this for initialization
        void Start()
        {
            rod = GetComponent<Rigidbody2D>();
            RandomMove();
            InvokeRepeating(nameof(RandomMove), 3, weakTime);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rod.MovePosition((Vector2) transform.position + moveVector * (speed * Time.deltaTime));
        }

        private void RandomMove(int[] randoms)
        {
            moveingID = Random.Range(0, randoms.Length);
            if (randoms[moveingID] == 0)
            {
                moveVector = Vector2.up;
            }
            else if (randoms[moveingID] == 1)
            {
                moveVector = Vector2.down;
            }
            else if (randoms[moveingID] == 2)
            {
                moveVector = Vector2.left;
            }
            else
            {
                moveVector = Vector2.right;
            }
        }

        void RandomMove()
        {
            moveingID = Random.Range(0, 4);
            if (moveingID == 0)
            {
                moveVector = Vector2.up;
            }
            else if (moveingID == 1)
            {
                moveVector = Vector2.down;
            }
            else if (moveingID == 2)
            {
                moveVector = Vector2.left;
            }
            else
            {
                moveVector = Vector2.right;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Boom")) return;
            //复位
            var position = transform.position;
            position = new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
            transform.position = position;
            if (moveingID == 0)
            {
                RandomMove(new[] {1, 2, 3});
            }
            else if (moveingID == 1)
            {
                RandomMove(new[] {0, 2, 3});
            }
            else if (moveingID == 2)
            {
                RandomMove(new[] {1, 0, 3});
            }
            else if (moveingID == 3)
            {
                RandomMove(new[] {1, 2, 0});
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Boom")) return;
            UIController.enemyCount -= 1;
            if (UIController.enemyCount < 0)
                UIController.enemyCount = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameContorller>().enemyCount -= 1;
            Destroy(gameObject);
        }
    }
}