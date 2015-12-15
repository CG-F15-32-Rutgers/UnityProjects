using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using RootMotion.FinalIK;

public class MyBehaviorTree : MonoBehaviour
{
	public Transform[] locations;
	public GameObject[] participants;

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

	protected Node Wave(int player1Num, int player2Num)
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
	}

	public Node ST_Orient(int player1Num ,int player2Num)
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
	}

	protected Node ST_ApproachAndWait(int playerNum, Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( participants[playerNum].GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1));
	}
    protected Node ST_Approach(int playerNum, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participants[playerNum].GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }

    protected Node BuildTreeRoot()
	{
		int[] playerIndex = new int[participants.Length];
		//Val<Vector3>[] playerPos = new Val<Vector3>[numberOfParticipants.Length];
		//reshuffle(numberOfParticipants);
		for (int i = 0; i < playerIndex.Length; i++)
		{
			playerIndex[i] = i;
			
		}

		int[] moveto = new int[participants.Length];
        //int[] wanderto = new int[participants.Length * 3];
        print (participants.Length);
		int randomPos = Random.Range(0, participants.Length);
		print (randomPos);
		if (randomPos % 2 != 0) {
			randomPos--;
		}
		for (int i = 0; i < playerIndex.Length; i++)
		{
			moveto[i] = randomPos;
			print(moveto[i]);
			randomPos++;
		}
        //randomPos = Random.Range(0, participants.Length * 3);

        /*for ( int i = 0; i < playerIndex.Length*4; ++i)
        {
            wanderto[i] = randomPos + 8;
            randomPos++;
        }*/

        return
                new DecoratorForceStatus( RunStatus.Success, new Sequence(
                    new SequenceParallel(
                        new Sequence(
                        this.ST_Orient(playerIndex[0], playerIndex[1]),
                        this.Wave(playerIndex[0], playerIndex[1]),
                        this.ST_ApproachAndOrient(locations[moveto[0]], playerIndex[0], locations[moveto[1]], playerIndex[1]),
                        new DecoratorLoop(1, this.Converse(playerIndex[0], playerIndex[1])),
                        this.Wave(playerIndex[0], playerIndex[1])
                        ),
                        new Sequence(
                        this.ST_Orient(playerIndex[2], playerIndex[3]),
                        this.Wave(playerIndex[2], playerIndex[3]),
                        this.ST_ApproachAndOrient(locations[moveto[2]], playerIndex[2], locations[moveto[3]], playerIndex[3]),
                        new DecoratorLoop(1, this.Converse(playerIndex[2], playerIndex[3])),
                        this.Wave(playerIndex[2], playerIndex[3])
                        )
                    ),
                     new DecoratorForceStatus(RunStatus.Success, new DecoratorLoop( new SequenceParallel(
                        this.ST_ApproachAndWait(playerIndex[0], locations[Random.Range(8, 10)]),
                        this.ST_ApproachAndWait(playerIndex[1], locations[Random.Range(11, 13)]),
                        this.ST_ApproachAndWait(playerIndex[2], locations[Random.Range(14, 16)]),
                        this.ST_ApproachAndWait(playerIndex[3], locations[Random.Range(17, 19)])))

                        //new ForEach<GameObject> (new Sequence(participants[].GetComponent<BehaviorMecanim>().Node_GoTo(Val.V(()=>locations[Random.Range(8,19)].transform.position), new LeafWait(1000))/*(locations[Random.Range(8, 19)]*/, participants)
                        )

                )
			);
	}
}
