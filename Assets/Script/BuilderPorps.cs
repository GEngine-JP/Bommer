using System.Collections;
using UnityEngine;

namespace Script
{
    /// <summary>
    /// 作者: Foldcc
    /// QQ: 1813547935
    /// </summary>
    public class BuilderPorps : MonoBehaviour
    {
        public Sprite[] propsSprite;

        private bool isActive;

        private int propsID; //0 生命值buff  1 移动速度buff 2 炸弹CDbuff  3 爆炸范围  

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Boom") && isActive == false)
            {
                isActive = true;
                //随机此道具功能
                propsID = Random.Range(0, propsSprite.Length);
                //开启isTrigger
                GetComponent<BoxCollider2D>().isTrigger = true;
                //更改对应道具的sprite
                GetComponent<SpriteRenderer>().sprite = propsSprite[propsID];

                //播放道具的动画效果
                StartCoroutine(PorpsAnimation());
            }
            else if (other.CompareTag("Player") && isActive)
            {
                switch (propsID)
                {
                    case 0:
                        other.GetComponent<PlayerController>().PlayerBuff(0, 1);
                        break;
                    case 1:
                        other.GetComponent<PlayerController>().PlayerBuff(1, 1);
                        break;
                    case 2:
                        other.GetComponent<PlayerController>().PlayerBuff(2, -0.7f);
                        break;
                    case 3:
                        other.GetComponent<PlayerController>().PlayerBuff(4, 1);
                        break;
                    case 4:
                        UIController.gameTime += 25;
                        break;
                }

                Destroy(gameObject);
            }
        }

        private IEnumerator PorpsAnimation()
        {
            SpriteRenderer s = GetComponent<SpriteRenderer>();
            for (var i = 0; i <= 5; i++)
            {
                yield return new WaitForSeconds(0.2f);
                s.color = Color.white;
                yield return new WaitForSeconds(0.2f);
            }

            yield return 0;
        }
    }
}