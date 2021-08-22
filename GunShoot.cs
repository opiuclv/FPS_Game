using UnityEngine ; 
using System.Collections ;


public class GunShoot : MonoBehaviour {

	 public Animator Animator ;
	 public Transform GunPoint ;
	 public GameObject Spark ;
	 public TextMesh textMesh ;
	 public AudioClip Fire ;
	 public AudioClip Reload ;

	 AudioSource AudioSource ;
	 bool isReloading = false ;
	 int maxBullet = 12 ;
	 int currentBullet ;
	 StearmVR_TrackedController StearmVR_TrackedController ;

	 void Start() 
	 {
	 	// 建立監聽
	 	StearmVR_TrackedController = GetComponent<StearmVR_TrackedController>() ;
	 	StearmVR_TrackedController.TriggerClicked += Gripped ;
	 	currentBullet = maxBullet ;
	 	AudioSource = GetComponent<AudioSource>() ;
	 }
	 // 板機扣下時的開槍邏輯
	 void TriggerClicked( object sender, ClickedEventArgs e )
	 {
	 	// 如果在換彈中直接返回
	 	if ( isReloading )
	 	{
	 		return ;
	 	}
	 	// 如果沒有子彈也直接返回
	 	if ( currentBullet > 0 )
	 	{
	 		currentBullet -- ;
	 		textMesh.text = currentBullet.ToString() ;
	 	}
	 	else
	 	{
	 		return ;
	 	}
	 	// 播放開始嗆聲
	 	AudioSource.PlayOneShot(Fire) ;
	 	Animator.Play("PistoAnimation") ;
	 	Debug.DrawRay(GunPoint.position, GunPoint.up*100, Color.red, 0.02f) ;
	 	Ray raycast = newRay(GunPoint.position, GunPoint.up) ;
	 	RaycastHit hit ;
	 	// 根據Layer來判斷是否有物體擊中
	 	LayerMask layer = 1 << ( LayerMask.NameToLayer("Enemy")) ;
	 	bool bHit = Physics.Raycast(raycast, out hit, 10000, layer.value) ;
	 	// 擊中扣血邏輯
	 	if ( bHit )
	 	{
	 		Debug.Log(hit.collider.gameObject) ;
	 		EnemyController ec = hit.collider.gameObject.GetComponent<EnemyController>() ;
	 		if ( ec != null )
	 		{
	 			ec.UnderAttack() ;
	 			GameObject go = GameObject.Instantiate(Spark) ;
	 			go.transform.position = hit.point ;
	 			Destroy(go, 3) ;
	 		}
	 	}
	 }

	 // 搖桿握住的邏輯
	 void Gripped( object sender, ClickedEventArgs e )
	 {
	 	if ( isReloading )
	 	{
	 		return ;
	 	}
	 	isReloading = true ;
	 	Invoke("ReloadFinished", 2 ) ;
	 	AudioSource.PlayOneShot(Reload) ;
	 }

	 // 換彈結束
	 void ReloadFinished() 
	 {
	 	isReloading = false ;
	 	currentBullet = maxBullet ;
	 	textMesh.text = currentBullet.ToString() ;
	 }
}







