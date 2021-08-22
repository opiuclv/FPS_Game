using UnityEngine ; 
using System.Collections ;
using UnityEngine.SceneManagement ;

public class Manager : MonoBehaviour {
	// Manager單例
	public static Manager Instance ;
	// 目前玩家 HP
	public int CurrentHP = 10 ;
	// 增加傳送門
	public GameObject Portals ;

	// 用於初始化
	void Awake () {
		// 實現單例
		if ( Instance == null )
		{
			Instance = this ;
		}
		else
		{
			Debug.LogError("僅有一個Manager") ;
		}
	}
	// 受到怪物攻擊
	public void UnderAttack() {
		CurrentHP -- ;
		// 如果血量小於0 遊戲gameover
		if ( CurrentHP < 0 )
		{
			EndGame() ;
		}
	}
	void EndGame()
	{
		Destroy(Portals) ;
	}

}







