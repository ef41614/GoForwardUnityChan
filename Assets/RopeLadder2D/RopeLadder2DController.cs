using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLadder2DController : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_ladderRoot;
    [SerializeField] DistanceJoint2D m_ladderPartPrefab;
    [SerializeField] float m_distanceBetweenSteps = 1.0f;
    [SerializeField] float m_linearDragForLadder = 1.0f;
    List<DistanceJoint2D> m_ladderParts = new List<DistanceJoint2D>();

	private GameObject Ladder3;
	private float startLine = -2.9f;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {

	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {
		Ladder3 = GameObject.Find ("Ladder Part 3");

//		if (startLine + 5.5f <= this.transform.position.x) {
//			DestroyLadder ();
//		}

	}

	//####################################  other  ####################################

    public void CreateLadder()
    {

		if(Ladder3==false){
        Rigidbody2D connected;
        Vector2 position;

		for(int i = 0; i<4;i++){
        if (m_ladderParts.Count == 0)
        {
            connected = m_ladderRoot;
        }
        else
        {
            connected = m_ladderParts[m_ladderParts.Count - 1].gameObject.GetComponent<Rigidbody2D>();
        }
        position = connected.gameObject.transform.position;

        DistanceJoint2D newLadderPart = Instantiate<DistanceJoint2D>(m_ladderPartPrefab);
        newLadderPart.gameObject.transform.localPosition = position;
        newLadderPart.distance = m_distanceBetweenSteps;
        newLadderPart.gameObject.GetComponent<Rigidbody2D>().drag = m_linearDragForLadder;
        newLadderPart.gameObject.name = "Ladder Part " + m_ladderParts.Count.ToString();
        newLadderPart.connectedBody = connected;
        m_ladderParts.Add(newLadderPart);
    }
	}
	}

    public void DestroyLadder()
    {
      StartCoroutine(DestroyLadderImpl());
//		StartCoroutine (DestroyLadderImpl (3.5f, () => 
//		{
//		}));
    }

	IEnumerator DestroyLadderImpl()
    {
		yield return new WaitForSeconds(1);
        while (m_ladderParts.Count > 0)
        {
            DistanceJoint2D ladderPartToBeDestroyed =  m_ladderParts[m_ladderParts.Count - 1];
            m_ladderParts.Remove(ladderPartToBeDestroyed);
            Destroy(ladderPartToBeDestroyed.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }

	//#################################################################################
}
// End