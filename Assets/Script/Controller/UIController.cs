using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script
{
	/// <summary>
	/// 作者: Foldcc
	/// QQ: 1813547935
	/// </summary>
	public class UIController : MonoBehaviour {
		[FormerlySerializedAs("UIview")] public GameObject[] uIview;

		public AudioClip startBackgroundAudio;

		public AudioClip backgroundAudio;

		public AudioClip loadAudio;

		public AudioClip lossAudio;

		public static int gameLevel = 0;

		public static int enemyCount = 0;

		public static int playerHp = 0;

		public static int gameTime = 0;

		public Text levelText;
		public Text enemyCountText;
		[FormerlySerializedAs("playerHPText")] public Text playerHpText;
		public Text gameTimeText;
		public void StartViewWithIndex(int index){
			foreach (var t in uIview)
			{
				t.SetActive (false);
			}
			uIview [index].SetActive (true);
			if (index == 0)
			{
				GetComponent<AudioSource>().loop = true;
				GetComponent<AudioSource>().clip = startBackgroundAudio;

			}else if (index == 1) {
				GetComponent<AudioSource>().loop = true;
				GetComponent<AudioSource> ().clip = backgroundAudio;
            
			} else if (index == 2) {
				GetComponent<AudioSource> ().clip = loadAudio;
			} else if (index == 3) {
				GetComponent<AudioSource>().loop = false;
				GetComponent<AudioSource> ().clip = lossAudio;
           
			}
			GetComponent<AudioSource> ().Stop ();
			GetComponent<AudioSource> ().Play ();

		}

		void OnGUI(){
			levelText.text = "Level: " + gameLevel;
			enemyCountText.text ="Enemy: " + enemyCount;
			playerHpText.text ="HP: " + playerHp;
			gameTimeText.text = "Time: " + gameTime;
			gameTimeText.color = gameTime < 15 ? Color.red : Color.white;
		}

	}
}
