using UnityEngine ; 
using System.Collections ;

public class CreateMonster : MonoBehaviour {

	// 目標位數
	public Transform TargetPositon ;
	// 產生怪物預設物體
	public GameObject Monster ;
	// 建立要播放的音效檔
	public AudioClip CreateClip ;

	AudioSource AudioSource ;
	// 最長刷怪時長
	float MaxColdDownTime = 10 ;
	// 最短刷怪時長
	float MinColdDownTime = 4 ;
	float CurrentColdDownTime ;

	void Start()
	{
		// 用於初始化刷怪時間
		CurrentColdDownTime = MinColdDownTime ;
		// 取得AudioSource的屬性
		AudioSource = GetComponent<AudioSource>() ;
	}
	void Update() 
	{
		// 透過Time.deltaTime來取得每一畫框執行的時間
		CurrentColdDownTime -= Time.deltaTime ;
		// 當冷卻時間完成後建立怪物並減少刷怪冷卻時間
		if ( CurrentColdDownTime <= 0 ) 
		{
			instantiateMonster() ;
			CurrentColdDownTime = MaxColdDownTime ;
			if ( MaxColdDownTime > MinColdDownTime )
			{
				MaxColdDownTime -= 0.5f ;
			}
		}
	}
	void InstantiateMonster()
	{
		// 播放怪物吼叫聲
		AudioSource.PlayOneShot(CreateClip) ;
		// 根據預設物體建立怪物
		GameObject go = Instantiate(Monster) ;
		// 將建立出來的怪物放置到自己下面，方便管理
		go.transform.parent = this.transform ;
		//透過Ramdom類別隨機將怪物放置在門口
		go.transform.position = this.transform.position + new Vector3
		( Random.Range(-5, 5), 0, Random.Range( -2.5f, 2.5f)) ;
	}

}







