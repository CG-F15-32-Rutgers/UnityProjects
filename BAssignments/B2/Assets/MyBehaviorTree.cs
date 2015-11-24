using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree : MonoBehaviour
{
	public Transform wander1;
	public Transform wander2;
	public Transform wander3;
	public GameObject participant;
	public GameObject participant2;

	private BehaviorAgent behaviorAgent;
	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	protected Node GunPoint()
	{
		return
			new DecoratorPrintResult(
				new SequenceParallel( 
			             participant.GetComponent<BehaviorMecanim>().ST_PlayGesture("PistolAim", AnimationLayer.Hand, 6000),
			             new Sequence(						 
	                     participant2.GetComponent<BehaviorMecanim>().ST_PlayGesture("Shock", AnimationLayer.Hand, 1000),
						 participant2.GetComponent<BehaviorMecanim>().ST_PlayGesture("HandsUp", AnimationLayer.Hand, 5000))));
	}

	
	public Node ST_ApproachAndOrient()
	{
		Val <Vector3> p1pos = Val.V(() => participant.gameObject.transform.position);
		Val<Vector3> p2pos = Val.V(() => participant2.gameObject.transform.position);
		return new Sequence(
			new SequenceParallel(
			participant.GetComponent<BehaviorMecanim>().Node_OrientTowards(p2pos),
			participant2.GetComponent<BehaviorMecanim>().Node_OrientTowards(p1pos)));
	}



	protected Node ST_ApproachAndWait(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}

	protected Node BuildTreeRoot()
	{
		return
			new DecoratorLoop (
				new SequenceParallel (
					new Sequence (
					this.ST_ApproachAndOrient(),
					this.GunPoint ())
		)
		);
	}
}
