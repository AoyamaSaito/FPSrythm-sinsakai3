using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAnimation : MonoBehaviour
{
    [SerializeField] float moveTime = 1f;
    [SerializeField] int upAngle = 290;
    [SerializeField] int leftAngle = 70;
    [SerializeField] Transform camera;
     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Up()
    {
        //iTween.RotateTo(gameObject, iTween.Hash("x", upAngle + camera.localRotation.x, "time", moveTime));
    }

    void Down()
    {
        //iTween.RotateTo(gameObject, iTween.Hash("x", camera.localRotation.x, "time", moveTime));
    }

    public void Left()
    {
        //iTween.RotateTo(gameObject, iTween.Hash("y", leftAngle + camera.localRotation.y, "time", moveTime));
    }

    public void Right()
    {
        //iTween.RotateTo(gameObject, iTween.Hash("y", camera.localRotation.y, "time", moveTime));
    }

    public void PistolRecoil()
    {
        StartCoroutine(Recoil());
    }

    IEnumerator Recoil()
    {
        Up();
        yield return new WaitForSeconds(moveTime);
        Down();
    }
}
