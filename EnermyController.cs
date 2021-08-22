using UnityEngine ; 
using System.Collections ;

public class EnermyContoller : MonoBehaviour {

	// 目標位數
	public Transform TargetTransform ;
	// 總血量
	int HP = 2 ;
	// 導覽元件
	NavMeshAgent NavMeshAgent ;
	// 動畫元件
	Animator Animator ;

	// 用於初始化
	void Start()
	{
		NavMeshAgent = GetComponent<NavMeshAgent>() ;
		NavMeshAgent.SetDestination(TargetTransform.transform.position) ;
		Animator = GetComponent<Animator>() ;
	}
	void Update() 
	{
		if ( HP <= 0 ) // 死亡 
		{
			return ;
		}
		// 取得目前動畫資訊
		AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0) ;

		// 判斷目前的動畫狀態是什麼，並進行對應的處理
		if ( stateInfo.fullPath == Animator.StringToHash("Base Layer.Run") && 
			 !Animator.IsInTransition(0) ) 
		{
			Animator.SetBool("Run", false) ;
		}
		// 玩家有移動則重新檢測，目前不會移動
		if ( Vector3.Distance(TargetTransform.transform.position, NavMeshAgent.destination) > 1)
		{
			NavMeshAgent.SetDestination(TargetTransform.transform.position) ;
		}
		// 進入攻擊距離則跳耀到攻擊動畫，否則繼續跑動
		if ( NavMeshAgent.remainingDistance < 3 ) 
		{
			Animator.SetBool("Attack", true ) ;
		}
		else 
		{
			Animator.SetBool("Run", true) ;
		}
		if ( stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Attack") && 
			 !Animator.IsInTransition(0))
		{
			Animator.SetBool("Attack", false) ;
			// 玩家有移動則重新檢測，目前不會移動
			if ( Vector3.Distance(TargetTransform.transform.position, NavMeshAgent.destination) > 1)
			{
				NavMeshAgent.SetDestination(TargetTransform.transform.position) ;
			}
			// 進入攻擊距離則跳耀到攻擊動畫，否則繼續跑動
			if ( NavMeshAgent.remainingDistance < 3 ) 
			{
				Animator.SetBool("Attack", true ) ;
			}
			else 
			{
				Animator.SetBool("Run", true) ;
			}
			/*
			if ( stateInfo.normalizeTime >= 1.0f )
			{
				Manager.Instance,UnderAttack() ;
			}
			*/
		}
		if ( stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Attack") && 
			 !Animator.IsInTransition(0))
		{
			Animator.SetBool("Attack", true) ;
			Animator.SetBool("Attack", false) ;
			// 玩家有移動則重新檢測，目前不會移動
			if ( Vector3.Distance(TargetTransform.transform.position, NavMeshAgent.destination) > 1)
			{
				NavMeshAgent.SetDestination(TargetTransform.transform.position) ;
			}
			// 進入攻擊距離則跳耀到攻擊動畫，否則繼續跑動
			if ( NavMeshAgent.remainingDistance < 3 ) 
			{
				Animator.SetBool("Attack", true ) ;
				NavMeshAgent.Stop() ;
			}
			else 
			{
				Animator.SetBool("Run", true) ;
			}
		}
	}
	public void UnderAttack()
	{
		// 扣血並判斷是否死亡，如果死了就直接跳到死亡動畫 else受傷動畫
		HP -- ;
		if ( HP <= 0 )
		{
			Animator.Play("Death") ;
			Destroy(GetComponent<Collider>()) ;
			Destroy(GetComponent<NavMeshAgent>()) ;
		}
		else
		{
			Animator.Play("Damage") ;
		}
		// 怪物攻擊由動畫事件呼叫
		public void Attack() ;
	}

}







