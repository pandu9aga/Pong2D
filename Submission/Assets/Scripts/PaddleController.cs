using UnityEngine;
public class PaddleController : MonoBehaviour
{
    public float batasAtas;
    public float batasBawah;
    public float batasKanan;
    public float batasKiri;
    public float kecepatan;
    public string axisV;
    public string axisH;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float gerakV = Input.GetAxis(axisV) * kecepatan * Time.deltaTime;
        float nextPosV = transform.position.y + gerakV;
        if (nextPosV > batasAtas)
        {
            gerakV = 0;
        }
        if (nextPosV < batasBawah)
        {
            gerakV = 0;
        }

        float gerakH = Input.GetAxis(axisH) * kecepatan * Time.deltaTime;
        float nextPosH = transform.position.x + gerakH;
        if (nextPosH > batasKanan)
        {
            gerakH = 0;
        }
        if (nextPosH < batasKiri)
        {
            gerakH = 0;
        }

        transform.Translate(gerakH, gerakV, 0);
    }
}