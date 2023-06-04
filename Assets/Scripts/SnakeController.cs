using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    // Start is called before the first frame update

    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 70;

    public GameObject BodyPrefab;

    private Animator anim;
    private List<GameObject> BodyParts = new List<GameObject> ();
    private List<Vector3> PositionsHistory = new List<Vector3> ();

    void Start()
    {
        anim = GetComponent<Animator> ();

        StartCoroutine(GrowSnakeDelay());

    }

    // Update is called once per frame
    void Update()
    {

        //move forward
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //steer
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        //store position history
        PositionsHistory.Insert (0, transform.position);

        //move body parts
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory [Mathf.Min(index * Gap, PositionsHistory.Count-1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);

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
