using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using RootMotion.FinalIK;

public class MyBehaviorTreePart2 : MonoBehaviour
{
    public GameObject mayor;
    public GameObject executioner;
    public GameObject accused;
    public GameObject[] extras;
    public GameObject[] zombies;
    public GameObject movable_crate;
    public Transform crate_position;

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
    
    protected Node post_trial_converse()
    {
        return new SequenceParallel(
            new DecoratorLoop(2, Converse(mayor, extras[15])),
            new DecoratorLoop(2, Converse(extras[0], extras[1])),
            new DecoratorLoop(2, Converse(extras[3], extras[4])),
            new DecoratorLoop(2, Converse(extras[5], extras[6])),
            new DecoratorLoop(2, Converse(extras[7], extras[8])),
            new DecoratorLoop(2, Converse(extras[10], extras[11])),
            new DecoratorLoop(2, Converse(extras[12], extras[13]))           
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
        System.Action vote = ()=> { vote_count += 9; };
        return new Sequence(new LeafInvoke(vote), mayor.GetComponent<BehaviorMecanim>().ST_PlayGesture("Wave", AnimationLayer.Hand, 3000));
    }

    protected Node mayor_vote_false()
    {
        System.Action vote = () => { vote_count -= 9; };
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
        System.Action night = () => { this.GetComponent<NightTime>().changeTime(); };
        System.Action die = () => { accused.SetActive(false); };
        System.Action curse = () => { accused_curse = true; };

        return new Sequence(
            ST_ApproachAndOrient(executioner, executionerPoints[1], accusedPoints[0]),
            speech(accused),
            executioner.GetComponent<BehaviorMecanim>().ST_PlayGesture("PISTOLAIM", AnimationLayer.Hand, 3000),
            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("Dying", AnimationLayer.Body, 10000),
            new LeafInvoke(die),
            new LeafInvoke(night),
            new LeafInvoke(curse)
            );
    }

    protected Node freedom()
    {
        System.Action night = () => { this.GetComponent<NightTime>().changeTime(); };
        System.Action curse = () => { accused_curse = false; };
        return new Sequence(
            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("Wonderful", AnimationLayer.Hand, 2000),
            ST_ApproachAndWait(accused, accusedPoints[2]),
            ST_ApproachAndOrient(accused, accusedPoints[2], accusedPoints[3]),
            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("SATNIGHTFEVER", AnimationLayer.Hand, 3000),
            new LeafInvoke(night),
            new LeafInvoke(curse)
            );
    }

    protected Node continuous_ritual()
    {
        return new Sequence(
            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("SATNIGHTFEVER", AnimationLayer.Hand, 3000)
            );
    }
    protected Node post_execution()
    {
        System.Action runspeed = () => {
            for (int i = 0; i < extras.Length; ++i)
            {
                extras[i].GetComponent<Animator>().SetFloat("Speed", 10);
            }
            mayor.GetComponent<Animator>().SetFloat("Speed", 10);
            executioner.GetComponent<Animator>().SetFloat("Speed", 10);
        };

        return new Sequence(new LeafInvoke(runspeed), new SequenceParallel(
            new Sequence(ST_ApproachAndWait(extras[0], extraPanic[0]), panic(extras[0])),
            new Sequence(ST_ApproachAndWait(extras[1], extraPanic[1]), panic(extras[1])),
            new Sequence(ST_ApproachAndWait(extras[2], extraPanic[2]), panic(extras[2])),
            new Sequence(ST_ApproachAndWait(extras[3], extraPanic[3]), panic(extras[3])),
            new Sequence(ST_ApproachAndWait(extras[4], extraPanic[4]), panic(extras[4])),
            new Sequence(ST_ApproachAndWait(extras[5], extraPanic[5]), panic(extras[5])),
            new Sequence(ST_ApproachAndWait(extras[6], extraPanic[6]), panic(extras[6])),
            new Sequence(ST_ApproachAndWait(extras[7], extraPanic[7]), panic(extras[7])),
            new Sequence(ST_ApproachAndWait(extras[8], extraPanic[8]), panic(extras[8])),
            new Sequence(ST_ApproachAndWait(extras[9], extraPanic[9]), panic(extras[9])),
            new Sequence(ST_ApproachAndWait(extras[10], extraPanic[10]), panic(extras[10])),
            new Sequence(ST_ApproachAndWait(extras[11], extraPanic[11]), panic(extras[11])),
            new Sequence(ST_ApproachAndWait(extras[12], extraPanic[12]), panic(extras[12])),
            new Sequence(ST_ApproachAndWait(extras[13], extraPanic[13]), panic(extras[13])),
            new Sequence(ST_ApproachAndWait(extras[14], extraPanic[14]), panic(extras[14])),
            new Sequence(ST_ApproachAndWait(extras[15], extraPanic[15]), panic(extras[15])),
            protaginist_actions()

            ));
    }

    protected Node panic(GameObject char1)
    {
        return new DecoratorLoop(new Sequence(char1.GetComponent<BehaviorMecanim>().ST_PlayGesture("Duck", AnimationLayer.Body, 3000)));
    }

    protected Node protaginist_actions()
    {
        System.Action move_crate = () => { movable_crate.transform.Translate(crate_position.position - movable_crate.transform.position, Space.World); };
        System.Func<bool> hope = () => (!accused_curse);
        System.Func<bool> despair = () => (accused_curse);

        return new Sequence(new SequenceParallel(ST_ApproachAndOrient(mayor, mayorPoints[2], executionerPoints[3]),
                                                ST_ApproachAndOrient(executioner, executionerPoints[2], movable_crate.transform)),
        executioner.GetComponent<BehaviorMecanim>().ST_PlayGesture("STAYAWAY", AnimationLayer.Hand, 3000),
            new LeafInvoke(move_crate),
            ST_ApproachAndOrient(executioner, executionerPoints[3], mayorPoints[2]),
            new DecoratorLoop(3, Converse(mayor, executioner)),
            new DecoratorForceStatus(RunStatus.Success, new Sequence(new LeafAssert(hope), save_the_day())),
            new DecoratorForceStatus(RunStatus.Success, new Sequence(new LeafAssert(despair), death_to_all()))

            );
    }

    protected Node death_to_all()
    {
        System.Action zombie_targeting = () => { zombies[0].GetComponent<ZombieAI>().superTarget = movable_crate.transform;
                                                 zombies[1].GetComponent<ZombieAI>().superTarget = movable_crate.transform;};
        return new Sequence(
            new LeafInvoke(zombie_targeting),
            new SequenceParallel (new DecoratorLoop(Converse(mayor, executioner)))

            );
    }

    protected Node save_the_day()
    {
        System.Action escape_mayor = () => { mayor.GetComponent<BodyMecanim>().NavWarp(mayorPoints[4].position); };
        System.Action escape_exec = () => { executioner.GetComponent<BodyMecanim>().NavWarp(executionerPoints[5].position); };
        System.Action kill_zombies = () => { this.GetComponent<NightTime>().kill_zombies(); };
        System.Action die = () => { accused.SetActive(false); };

        return new Sequence(new SequenceParallel(new Sequence(ST_Approach(mayor, mayorPoints[3]), new LeafInvoke(escape_mayor), ST_Approach(mayor, mayorPoints[5]), ST_Approach(mayor, mayorPoints[6]), 
                                                              ST_ApproachAndOrient(mayor, mayorPoints[7], accused.transform) ),
                                                 new Sequence(new LeafWait(2000), ST_Approach(executioner, executionerPoints[4]), new LeafInvoke(escape_exec), ST_Approach(executioner, executionerPoints[6]), 
                                                              ST_Approach(executioner, executionerPoints[7]), ST_ApproachAndOrient(executioner, executionerPoints[8], accused.transform))
                                                ),
                            mayor.GetComponent<BehaviorMecanim>().ST_PlayGesture("pointing", AnimationLayer.Hand, 3000),
                            ST_Orient(accused, mayor),
                            new DecoratorLoop(2, speech(mayor)),
                            new DecoratorLoop(2, speech(accused)),
                            speech(mayor),
                            executioner.GetComponent<BehaviorMecanim>().ST_PlayGesture("PISTOLAIM", AnimationLayer.Hand, 3000),
                            accused.GetComponent<BehaviorMecanim>().ST_PlayGesture("Dying", AnimationLayer.Body, 10000),
                            new LeafInvoke(die),
                            new LeafInvoke(kill_zombies),
                            new SequenceParallel(executioner.GetComponent<BehaviorMecanim>().ST_PlayGesture("Cheer", AnimationLayer.Hand, 3000), 
                                                 mayor.GetComponent<BehaviorMecanim>().ST_PlayGesture("Cheer", AnimationLayer.Hand, 3000))


            );
    }

    protected Node ST_ApproachAndWait(GameObject character, Transform target)
	{
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(character.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    protected Node ST_Approach(GameObject char1, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(char1.GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }

    protected Node BuildTreeRoot()
	{
        System.Func<bool> curse = () => (accused_curse);
        vote_count = 0;
        //this.GetComponent<NightTime>().changeTime();
        return new DecoratorLoop(
            new Sequence(pre_trial(), pre_trial_converse(), goto_trial(), trial(), accused_plee(), voting(), decide_execution(), pre_trial(), post_trial_converse(), post_execution()
                    
            ));
	}
}
