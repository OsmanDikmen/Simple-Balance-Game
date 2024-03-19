using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemenr : MonoBehaviour
{
    public float plSpeed = 3.0f;
    private float powerupStrengh = 15;
    private float plVerticial;
    public bool hasPowerup; 

    public GameObject PoweupIndicator;
    private GameObject focalPoint;
    private Rigidbody playerRb; 
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        plVerticial = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * plVerticial * plSpeed);
        PoweupIndicator.gameObject.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            PoweupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PoweupCountDownTime());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRgb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRgb.AddForce(awayFromPlayer * powerupStrengh,ForceMode.Impulse);
        }
    }

    IEnumerator PoweupCountDownTime()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        PoweupIndicator.SetActive(false);
    }
}
