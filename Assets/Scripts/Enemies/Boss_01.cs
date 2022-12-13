using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_01 : MonoBehaviour
{
    [SerializeField] private float life_head_01;
    [SerializeField] private float life_head_02;

    [SerializeField] private GameObject playerReference;
    [SerializeField] private GameObject head_01;
    [SerializeField] private GameObject head_02;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private bool  isArrive;
    [SerializeField] private Transform pointToFire;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject fireWall;
    
    
    private bool isAttacking = false;
    void Start()
    {
        life_head_01 = 80;
        life_head_02 = 80;
        head_01.SetActive(true);
        head_02.SetActive(false);
        speed = 2f;
        distance = 3f;
        AudioManager.Instance.PlaySong("grito_boss");

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }
    void FixedUpdate()
    {
        Vector2 dir = new Vector2(transform.position.x, playerReference.transform.position.y) - new Vector2(transform.position.x, transform.position.y);

       

    }

    public void TakeDamage(float damage)
    {
        if (life_head_01 > 0)
        {
            life_head_01 -= damage;
        }

        if (life_head_01 <= life_head_01 / 2)
        {
            head_02.SetActive(true);
        }

        if (life_head_01 <= 0 && life_head_02 > 0)
        {
            life_head_02 -= damage;
        }

        if (life_head_01 <= 0 && life_head_02 <= 0)
        {
            StartCoroutine(ChangeLevel());
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);

        }
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level_02");
    }
    
    IEnumerator Attack()
    {
        isAttacking = true;
        int randomAttack = Random.Range(0, 10);
        if(randomAttack < 7)
        {
            fireAttack();
        }
        else
        {
            fireWallAttack();
        }
        yield return new WaitForSeconds(4.5f);
        isAttacking = false;
    }
    private void fireAttack()
    {
        GameObject fire = Instantiate(fireBall, pointToFire.position, Quaternion.identity);
        //fire.transform.localPosition = Vector2.MoveTowards(transform.localPosition, playerReference.transform.position, speed * Time.deltaTime);

    }
    
    private void fireWallAttack()
    {
        Vector3 positionWall = new Vector3(0.9274117f,64.35f,0f);
        Instantiate(fireWall, positionWall, Quaternion.identity);

    }
}
