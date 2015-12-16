using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using RootMotion.FinalIK;

public class MyBehaviorTree : MonoBehaviour
{
    public GameObject mayor;
    public GameObject executioner;
    public GameObject accused;
    public GameObject[] extras;
    public GameObject[] zombies;

    public Transform[] audienceSpots;
    public Transform[] extraPanic;
    public Transform[] mayorPoints;
    public Transform[] executionerPoints;
    public Transform[] accusedPoints;
    public Transform[] roaming;

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
	}*/

	protected Node Converse(GameObject char1,  GameObject char2)
	{
		return
			new DecoratorPrintResult(
				new Sequence(
                         char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 1000),
                         char2.GetComponent<BehaviorMecanim>().ST_PlayGesture("Think", AnimationLayer.Face, 1000),
                         char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("Think", AnimationLayer.Face, 1000),
                         char2.GetComponent<BehaviorMecanim>().ST_PlayGesture("HeadNod", AnimationLayer.Face, 1000)));
	}

	protected Node ST_Orient(GameObject char1 , GameObject char2)
	{
		Val <Vector3> p1pos = Val.V(() => char1.transform.position);
		Val<Vector3> p2pos = Val.V(() => char2.transform.position);
		return 
			new Sequence( 
            char1.GetComponent<BehaviorMecanim>().Node_OrientTowards(p2pos),         
            char2.GetComponent<BehaviorMecanim>().Node_OrientTowards(p1pos)           
            );
	}

	/*public Node ST_ApproachAndOrient(Transform location1, int player1Num , Transform location2, int player2Num)
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

    protected Node pre_trial()
    {
        Val<Vector3> position1 = Val.V(() => extras[7].transform.position);
        return new SelectorParallel(
            new Sequence(extras[9].GetComponent<BehaviorMecanim>().Node_OrientTowards(position1), new LeafWait(1000)),
            ST_Orient(mayor, extras[16]),
            ST_Orient(extras[0], extras[1]),
            ST_Orient(extras[3], extras[4]),
            ST_Orient(extras[5], extras[6]),
            ST_Orient(extras[7], extras[8]),
            ST_Orient(extras[10], extras[11]),
            ST_Orient(extras[12], extras[13])
            );
    }

    protected Node pre_trial_converse()
    {
        return new SelectorParallel(
            new DecoratorLoop(2, Converse(mayor, extras[16])),
            new DecoratorLoop(2, Converse(extras[0], extras[1])),
            new DecoratorLoop(2, Converse(extras[3], extras[4])),
            new DecoratorLoop(2, Converse(extras[5], extras[6])),
            new DecoratorLoop(2, Converse(extras[7], extras[8])),
            new DecoratorLoop(2, Converse(extras[10], extras[11])),
            new DecoratorLoop(2, Converse(extras[12], extras[13]))
            );
    }


	protected Node ST_ApproachAndWait(GameObject character, Transform target)
	{
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    /*protected Node ST_Approach(int playerNum, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participants[playerNum].GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }*/

    protected Node BuildTreeRoot()
	{
        //this.GetComponent<NightTime>().changeTime();
        return new DecoratorLoop(
            new Sequence(pre_trial(), pre_trial_converse())
                    
            );
	}
}
