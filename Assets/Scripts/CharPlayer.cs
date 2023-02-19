using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharPlayer : MonoBehaviour
{
    public float speed;
    Rigidbody rg;
    public List<GameObject> Collectables = new List<GameObject>();
    public GameObject Collects;
    public GameObject GameOverPanel;
    public GameObject FinishGamePanel;
    public TextMeshProUGUI PointText;
    private int Score;
   

    
    Vector3 offset;
    [SerializeField] private Transform PointofStack;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        offset= new Vector3(0, 0.3f, 0);
        PointText.text = Score.ToString();
    }


    void Update()
    {

    }
    private void FixedUpdate()
    {
        rg.AddForce(Vector3.back * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {


            Score++;
            PointText.text = Score.ToString();
            other.transform.SetParent(transform);
            Collectables.Add(other.gameObject);
            other.transform.position = PointofStack.position;
            PointofStack.position = PointofStack.position + offset;

        }
        if (other.gameObject.CompareTag("FinishLine"))
        {
            FinishGamePanel.SetActive(true);
            Time.timeScale = 0;
        }
     
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObstacleTp2") || collision.gameObject.CompareTag("ObstacleTp1"))
        {
            if(Collectables.Count == 0)
            {
                GameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
           
        }
        if (collision.gameObject.CompareTag("ObstacleTp1"))
        {
            if (Collectables.Count != 0)
            {
                GameObject obj = Collectables;
                Collectables.Remove(obj);
                Destroy(obj);
                PointofStack.position = PointofStack.position - (offset);
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("ObstacleTp2"))
        {
            if (Collectables.Count != 0)
            {
                foreach (var Obj in Collectables)
                {
                    Destroy(Obj.gameObject);
                }
                PointofStack.position = PointofStack.position - (offset * (Collectables.Count));
                Collectables.Clear();
                Destroy(collision.gameObject);

                
              
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.position += Vector3.up * 100f* Time.deltaTime;
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
  
    
}
