using UnityEngine;

public class Fabric : Building
{
    [SerializeField] ParticleSystem recycleFx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Box box = other.GetComponent<Box>();
            box.LinkToFabric(this.TargetPoint);
            recycleFx.Play();
        }

        if (other.CompareTag("Robot"))
        {
            Robot robot = other.GetComponent<Robot>();
            robot.GiveBox();
        }
    }

}
