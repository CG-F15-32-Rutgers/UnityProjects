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

    private int vote_count;
    private bool accused_curse;
    private bool accused_ritual;

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
                         char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 2000),
                         char2.GetComponent<BehaviorMecanim>().ST_PlayGesture("Think", AnimationLayer.Face, 2000),
                         char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("Think", AnimationLayer.Face, 2000),
                         char2.GetComponent<BehaviorMecanim>().ST_PlayGesture("HeadNod", AnimationLayer.Face, 2000)));
	}

    protected Node speech(GameObject char1)
    {
        return new Sequence(
                         char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 2000),
                         char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("Think", AnimationLayer.Face, 2000),
                         char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("HeadNod", AnimationLayer.Face, 2000)
                         );
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
		return
			new Sequence(
            char1.GetComponent<BehaviorMecanim>().Node_GoTo(position1),
            char1.GetComponent<BehaviorMecanim>().Node_OrientTowards(position2)
            );
	}

    protected Node pre_trial()
    {
        Val<Vector3> position1 = Val.V(() => extras[7].transform.position);
        return new SequenceParallel(
            new Sequence(extras[9].GetComponent<BehaviorMecanim>().Node_OrientTowards(position1), new LeafWait(1000)),
            ST_Orient(mayor, extras[0]),
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
            new DecoratorLoop(2, Converse(mayor, extras[15])),
            new DecoratorLoop(2, Converse(extras[0], extras[1])),
            new DecoratorLoop(2, Converse(extras[3], extras[4])),
            new DecoratorLoop(2, Converse(extras[5], extras[6])),
            new DecoratorLoop(2, Converse(extras[7], extras[8])),
            new DecoratorLoop(2, Converse(extras[10], extras[11])),
            new DecoratorLoop(2, Converse(extras[12], extras[13])),
            new Sequence(ST_ApproachAndWait(extras[15], roaming[1]))
            );
    }

    protected Node goto_trial()
    {
        return new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(
            ST_ApproachAndOrient(mayor, mayorPoints[0], audienceSpots[2]),
            ST_ApproachAndOrient(extras[0], audienceSpots[0], mayorPoints[0]),
            ST_ApproachAndOrient(extras[1], audienceSpots[1], mayorPoints[0]),
            ST_ApproachAndOrient(extras[2], audienceSpots[2], mayorPoints[0]),
            ST_ApproachAndOrient(extras[3], audienceSpots[18], mayorPoints[0]),
            ST_ApproachAndOrient(extras[4], audienceSpots[19], mayorPoints[0]),
            ST_ApproachAndOrient(extras[5], audienceSpots[3], mayorPoints[0]),
            ST_ApproachAndOrient(extras[6], audienceSpots[6], mayorPoints[0]),
            ST_ApproachAndOrient(extras[7], audienceSpots[7], mayorPoints[0]),
            ST_ApproachAndOrient(extras[8], audienceSpots[16], mayorPoints[0]),
            ST_ApproachAndOrient(extras[9], audienceSpots[17], mayorPoints[0]),
            ST_ApproachAndOrient(extras[10], audienceSpots[10], mayorPoints[0]),
            ST_ApproachAndOrient(extras[11], audienceSpots[11], mayorPoints[0]),
            ST_ApproachAndOrient(extras[12], audienceSpots[12], mayorPoints[0]),
            ST_ApproachAndOrient(extras[13], audienceSpots[13], mayorPoints[0]),
            ST_ApproachAndOrient(extras[14], audienceSpots[14], mayorPoints[0]),
            ST_ApproachAndOrient(extras[15], audienceSpots[15], mayorPoints[0]),
            ST_ApproachAndOrient(accused, accusedPoints[0], audienceSpots[2]),
            ST_ApproachAndOrient(executioner, executionerPoints[0], accusedPoints[0])
            ));
    }

    protected Node trial()
    {
        return new Sequence(
            new DecoratorLoop(3, speech(mayor))
            );
    }
    
    protected Node accused_plee()
    {
        return new Sequence(
            new DecoratorLoop(2, speech(accused))
            );
    }

    protected Node voting()
    {
        return new Sequence(
            vote(extras[0]),
            vote(extras[1]),
            vote(extras[2]),
            vote(extras[3]),
            vote(extras[4]),
            vote(extras[5]),
            vote(extras[6]),
            vote(extras[7]),
            vote(extras[8]),
            vote(extras[9]),
            vote(extras[10]),
            vote(extras[11]),
            vote(extras[12]),
            vote(extras[13]),
            vote(extras[14]),
            vote(extras[15]),
            mayor_vote_true()
            );
    }

    protected Node vote(GameObject char1)
    {
        return new SelectorShuffle(
             vote_true(char1),
             vote_false(char1),
             vote_nuetral(char1)
            );
    }

    protected Node vote_true(GameObject char1)
    {
        System.Action vote = () => { vote_count += 1; };

        return
            new Sequence( new LeafInvoke(vote),
            char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("Wave", AnimationLayer.Hand, 3000));
    }

    protected Node vote_false(GameObject char1)
    {
        System.Action vote = () => { vote_count -= 1; };

        return
            new Sequence( new LeafInvoke(vote),            
            char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("HeadShake", AnimationLayer.Face, 1000));
    }
    
    protected Node mayor_vote_true()
    {
        System.Action vote = ()=> { vote_count += 7; };
        return new Sequence(new LeafInvoke(vote), mayor.GetComponent<BehaviorMecanim>().ST_PlayGesture("Wave", AnimationLayer.Hand, 3000));
    }

    protected Node mayor_vote_false()
    {
        System.Action vote = () => { vote_count -= 7; };
        return new Sequence(new LeafInvoke(vote), mayor.GetComponent<BehaviorMecanim>().ST_PlayGesture("HeadShake", AnimationLayer.Face, 1000));
    }

    protected Node vote_nuetral(GameObject char1)
    {
        return new Sequence(char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("Yawn", AnimationLayer.Hand, 1000));
    }

    protected Node decide_execution()
    {
        System.Func<bool> voted = () => (vote_count > 8);

        return new Selector(
            new Sequence(new LeafAssert(voted), commit_execution()),
            new Sequence(freedom())
            );    
    }

    protected Node commit_execution()
    {
        accused_curse = true;
        return new Sequence(
            ST_ApproachAndOrient(executioner, executionerPoints[1], accusedPoints[0]),
            speech(accused),
            executioner.GetComponent<BehaviorMecanim>().ST_PlayGesture("PISTOLAIM", AnimationLayer.Hand, 3000)
            );
    }

    protected Node freedom()
    {
        System.Action night = () => { this.GetComponent<NightTime>().changeTime(); };
        accused_curse = false;
        return new Sequence(
            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("Wonderful", AnimationLayer.Hand, 2000),
            ST_ApproachAndWait(accused, accusedPoints[2]),
            ST_ApproachAndOrient(accused, accusedPoints[2], accusedPoints[3]),
            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("SATNIGHTFEVER", AnimationLayer.Hand, 3000),
            new LeafInvoke(night)
            );
    }
    protected Node post_execution()
    {
        System.Action night = () => { this.GetComponent<NightTime>().changeTime(); };
        return new Sequence(
            ST_ApproachAndOrient(accused, accusedPoints[2], accusedPoints[3]),
            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("SATNIGHTFEVER", AnimationLayer.Hand, 3000),
            new LeafInvoke(night)
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
        vote_count = 0;
        //this.GetComponent<NightTime>().changeTime();
        return new DecoratorLoop(
            new Sequence(pre_trial(), pre_trial_converse(), goto_trial(), trial(), accused_plee(), voting(), decide_execution())
                    
            );
	}
}
