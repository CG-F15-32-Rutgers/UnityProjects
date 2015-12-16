using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using RootMotion.FinalIK;

public class MyBehaviorTree : MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public GameObject participant;

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

	/*protected Node Wave(int player1Num, int player2Num)
	{
		return
			new DecoratorPrintResult(
				new Sequence( 
			             participants[player1Num].GetComponent<BehaviorMecanim>().ST_PlayGesture("Wave", AnimationLayer.Hand, 1000),
			             participants[player2Num].GetComponent<BehaviorMecanim>().ST_PlayGesture("Wave", AnimationLayer.Hand, 1000),
			             participants[player1Num].GetComponent<BehaviorMecanim>().ST_PlayGesture("CallOver", AnimationLayer.Hand, 1000)));
	}

	protected Node Converse(int player1Num, int player2Num)
	{
		return
			new DecoratorPrintResult(
				new Sequence( 
			             participants[player1Num].GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 1000),
			             participants[player2Num].GetComponent<BehaviorMecanim>().ST_PlayGesture("Think", AnimationLayer.Face, 1000),
			             participants[player1Num].GetComponent<BehaviorMecanim>().ST_PlayGesture("Think", AnimationLayer.Face, 1000),
			             participants[player2Num].GetComponent<BehaviorMecanim>().ST_PlayGesture("HeadNod", AnimationLayer.Face, 1000)));
	}*/

	/*public Node ST_Orient(int player1Num ,int player2Num)
	{
		Val <Vector3> p1pos = Val.V(() => participants[player1Num].gameObject.transform.position);
		Val<Vector3> p2pos = Val.V(() => participants[player2Num].gameObject.transform.position);
		return new Sequence(
			new SequenceParallel(
			participants[player1Num].GetComponent<BehaviorMecanim>().Node_OrientTowards(p2pos),
			participants[player2Num].GetComponent<BehaviorMecanim>().Node_OrientTowards(p1pos)));
	}

	public Node ST_ApproachAndOrient(Transform location1, int player1Num , Transform location2, int player2Num)
	{
		Val <Vector3> p1pos = Val.V(() => participants[player1Num].gameObject.transform.position);
		Val<Vector3> p2pos = Val.V(() => participants[player2Num].gameObject.transform.position);
		Val<Vector3> position1 = Val.V(() => location1.position);
		Val<Vector3> position2 = Val.V(() => location2.position);
		return new Sequence(
			new SequenceParallel(
			participants[player1Num].GetComponent<BehaviorMecanim>().Node_GoTo(position1),
			participants[player2Num].GetComponent<BehaviorMecanim>().Node_GoTo(position2)),
			new SequenceParallel(
			participants[player1Num].GetComponent<BehaviorMecanim>().Node_OrientTowards(p2pos),
			participants[player2Num].GetComponent<BehaviorMecanim>().Node_OrientTowards(p1pos)));
	}*/

	protected Node ST_ApproachAndWait(Transform target)
	{
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    /*protected Node ST_Approach(int playerNum, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participants[playerNum].GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }*/

    protected Node BuildTreeRoot()
	{
        //this.GetComponent<NightTime>().changeTime();
        return
            new DecoratorLoop(
                new SequenceShuffle(
                    this.ST_ApproachAndWait(this.wander1),
                    this.ST_ApproachAndWait(this.wander2),
                    this.ST_ApproachAndWait(this.wander3)));
	}
}
