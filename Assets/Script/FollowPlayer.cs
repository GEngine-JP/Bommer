using UnityEngine;

namespace Script
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// </summary>
    public class FollowPlayer : MonoBehaviour
    {
        public static Transform player;
        public static int xitm = 0;

        public static int yitm = 0;

        // Update is called once per frame
        void Update()
        {
            if (player != null)
            {
                var transformPosition = transform.position;
                var playerPosition = player.position;
                var x = Mathf.Lerp(transformPosition.x, playerPosition.x, 0.2f);
                var y = Mathf.Lerp(transformPosition.y, playerPosition.y, 0.2f);
                transformPosition = new Vector3(x, y, transformPosition.z);
                transform.position = transformPosition;
            }

            Border();
        }

        private void Border()
        {
            if ((xitm - 6) <= 0 || (yitm - 3) <= 0)
            {
                transform.position = new Vector3(0, 0, transform.position.z);
            }
            else
            {
                if (transform.position.x > (xitm - 6))
                {
                    transform.position = new Vector3(xitm - 6, transform.position.y, transform.position.z);
                }

                if (transform.position.x < -(xitm - 6))
                {
                    transform.position = new Vector3(-(xitm - 6), transform.position.y, transform.position.z);
                }

                if (transform.position.y > (yitm - 3))
                {
                    transform.position = new Vector3(transform.position.x, yitm - 3, transform.position.z);
                }

                if (transform.position.y < -(yitm - 3))
                {
                    transform.position = new Vector3(transform.position.x, -(yitm - 3), transform.position.z);
                }
            }
        }
    }
}