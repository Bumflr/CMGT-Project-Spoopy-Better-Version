using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SnakeController : MonoBehaviour
{
    // Start is called before the first frame update

    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 70;

    public GameObject BodyPrefab;

    public float value;

    private List<GameObject> BodyParts = new List<GameObject> ();
    private List<Vector3> PositionsHistory = new List<Vector3> ();
    private List<LineRenderer> LineRenderers = new List<LineRenderer> ();
    private List<Animator> Animators = new List<Animator>();

    void Start()
    {
        StartCoroutine(GrowSnakeDelay());
    }

    // Update is called once per frame
    void Update()
    {
        //store position history
        PositionsHistory.Insert(0, transform.position);

        //move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count-1)];
            Vector3 moveDirection = point - body.transform.position - (body.transform.forward.normalized * index);
            
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);

            /* Line Shit */
            Vector3 previouspartPosition = (index - 1 > 0) ? BodyParts[index - 1].transform.position : BodyParts[0].transform.position;
            Vector3 previouspartPositionOffset = new Vector3(previouspartPosition.x, previouspartPosition.y + 0.5f, previouspartPosition.z);

            LineRenderers[index].SetPosition(0, previouspartPositionOffset);
            LineRenderers[index].SetPosition(LineRenderers[index].positionCount -1 , new Vector3(body.transform.position.x, body.transform.position.y + +0.5f, body.transform.position.z));


            /* Animator Shit*/
  
            var currentDistance = Vector3.Distance(body.transform.position, (index > 0) ? BodyParts[index - 1].transform.position : point);

            if (index == 0)
            {
                currentDistance = 1 - currentDistance;
            }

            Animators[index].speed = Mathf.Clamp01(Mathf.Abs(1 - currentDistance));

            index++;
        }
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab);

        body.transform.localScale = this.transform.localScale;

        BodyParts.Add(body);
        LineRenderers.Add(body.GetComponentInChildren<LineRenderer>());
        Animators.Add(body.GetComponent<Animator>());

        body.GetComponent<Animator>().enabled = true;
    }

    private IEnumerator GrowSnakeDelay()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GrowSnake();
        }
    }
}
