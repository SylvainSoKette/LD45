using UnityEngine;

public class LightsBehavior : MonoBehaviour
{

    private float daySpeed = 4.0f;
    private float orbiterDistance = 24.0f;

    // utility references
    public GameObject sun;
    public GameObject moon;
    public GameObject board;

    void Update()
    {
        sun.transform.Translate(-Vector3.right * Time.deltaTime * daySpeed);
        if (sun.transform.position.x < -orbiterDistance) {
            sun.transform.Translate(Vector3.right * 2 * orbiterDistance);
            board.GetComponent<BoardBehavior>().IncrementDay();
        }
        moon.transform.Translate(-Vector3.right * Time.deltaTime * daySpeed);
        if (moon.transform.position.x < -orbiterDistance) {
            moon.transform.Translate(Vector3.right * 2 * orbiterDistance);
        }
    }
}
