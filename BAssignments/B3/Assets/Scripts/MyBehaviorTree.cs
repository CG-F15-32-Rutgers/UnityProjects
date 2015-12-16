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
            char1.GetComponent<BehaviorMecanim>().ST_TurnToFace(p2pos),         
            char2.GetComponent<BehaviorMecanim>().ST_TurnToFace(p1pos),           
            new LeafWait(1000));
	}

	public Node ST_ApproachAndOrient(GameObject char1, Transform location1, Transform location2)
	{
		Val<Vector3> position1 = Val.V(() => location1.position);
		Val<Vector3> position2 = Val.V(() => location2.position);
		return new Sequence(
			new Sequence(
            char1.GetComponent<BehaviorMecanim>().Node_GoTo(position1)
            ),
			new Sequence(
            char1.GetComponent<BehaviorMecanim>().Node_OrientTowards(position2)
            ));
	}

    protected Node pre_trial()
    {
        Val<Vector3> position1 = Val.V(() => extras[7].transform.position);
        return new SequenceParallel(
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
        return new SequenceParallel(
            new DecoratorLoop(2, Converse(mayor, extras[16])),
            new DecoratorLoop(2, Converse(extras[0], extras[1])),
            new DecoratorLoop(2, Converse(extras[3], extras[4])),
            new DecoratorLoop(2, Converse(extras[5], extras[6])),
            new DecoratorLoop(2, Converse(extras[7], extras[8])),
            new DecoratorLoop(2, Converse(extras[10], extras[11])),
            new DecoratorLoop(2, Converse(extras[12], extras[13])),
            new Sequence(ST_ApproachAndWait(extras[2], roaming[1]))
            );
    }

    protected Node goto_trial()
    {
        return new SequenceParallel(
            ST_ApproachAndOrient(mayor, mayorPoints[0], audienceSpots[2]),
            ST_ApproachAndOrient(extras[0], audienceSpots[0], mayorPoints[0]),
            ST_ApproachAndOrient(extras[1], audienceSpots[1], mayorPoints[0]),
            ST_ApproachAndOrient(extras[2], audienceSpots[2], mayorPoints[0]),
            ST_ApproachAndOrient(extras[3], audienceSpots[3], mayorPoints[0]),
            ST_ApproachAndOrient(extras[4], audienceSpots[4], mayorPoints[0]),
            ST_ApproachAndOrient(extras[5], audienceSpots[5], mayorPoints[0]),
            ST_ApproachAndOrient(extras[6], audienceSpots[6], mayorPoints[0]),
            ST_ApproachAndOrient(extras[7], audienceSpots[7], mayorPoints[0]),
            ST_ApproachAndOrient(extras[8], audienceSpots[8], mayorPoints[0]),
            ST_ApproachAndOrient(extras[9], audienceSpots[9], mayorPoints[0]),
            ST_ApproachAndOrient(extras[10], audienceSpots[10], mayorPoints[0]),
            ST_ApproachAndOrient(extras[11], audienceSpots[11], mayorPoints[0]),
            ST_ApproachAndOrient(extras[12], audienceSpots[12], mayorPoints[0]),
            ST_ApproachAndOrient(extras[13], audienceSpots[13], mayorPoints[0]),
            ST_ApproachAndOrient(extras[14], audienceSpots[14], mayorPoints[0]),
            ST_ApproachAndOrient(extras[15], audienceSpots[15], mayorPoints[0]),
            ST_ApproachAndOrient(extras[16], audienceSpots[16], mayorPoints[0]),
            ST_ApproachAndOrient(accused, accusedPoints[0], audienceSpots[2]),
            ST_ApproachAndOrient(executioner, executionerPoints[0], accusedPoints[0])
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
            new Sequence(pre_trial(), pre_trial_converse(), goto_trial())
                    
            );
	}
}
