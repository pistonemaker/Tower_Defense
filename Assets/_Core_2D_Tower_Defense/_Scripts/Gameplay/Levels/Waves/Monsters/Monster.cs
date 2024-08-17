using UnityEngine;

public class Monster : MonoBehaviour
{
    public MiniWave miniWave;
    [SerializeField] private MonsterData monsterData;
    private Rigidbody2D rb;
    [SerializeField] private float curHP;

    public HealthBar healthBar;
    public SpriteRenderer sr;
    public Vector2 target;
    public int pathIndex;
    public int IDInWave;
    private int spiritStoneAmount;
    private int damage;

    private float notTakeDamageTime = 0f;
    private float timeToHideHealthBar = 2f;

    public void InitMonster(MonsterData data)
    {
        monsterData = data;
        curHP = data.maxHP;
        spiritStoneAmount = data.spiritStoneAmount;
        damage = data.damage;
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<HealthBar>();
        sr = GetComponentInChildren<SpriteRenderer>();
        healthBar.SetMaxHP(data.maxHP);
        pathIndex = 0;
        healthBar.gameObject.SetActive(false);
        target = miniWave.pathway.wayPoints[0];
    }

    private void Start()
    {
        RotateTowardsTarget();
    }

    private void Update()
    {
        notTakeDamageTime += Time.deltaTime;

        if (notTakeDamageTime >= timeToHideHealthBar)
        {
            healthBar.gameObject.SetActive(false);
        }
        
        if (Vector2.Distance(target, transform.position) <= 0.1f 
            && pathIndex < miniWave.pathway.wayPoints.Count - 1)
        {
            pathIndex++; 
            target = miniWave.pathway.wayPoints[pathIndex];
            RotateTowardsTarget();
        }
        else if (Vector2.Distance(target, transform.position) <= 0.1f 
                 && pathIndex == miniWave.pathway.wayPoints.Count - 1)
        {
            pathIndex++;
        }

        if (pathIndex == miniWave.pathway.wayPoints.Count)
        {
            this.PostEvent(EventID.On_Monster_Escaped, damage);
            miniWave.listMonsters.Remove(this);
            miniWave.CheckIfAllEnermyDead();
            //PoolingManager.Despawn(gameObject);
            Destroy(gameObject);
        }
        else
        {
            target = miniWave.pathway.wayPoints[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        rb.velocity = direction * monsterData.speed;
    }

    public void TakeDamage(float amount)
    {
        healthBar.gameObject.SetActive(true);
        notTakeDamageTime = 0f;
        curHP -= amount;
        
        if (curHP <= 0f)
        {
            OnMonsterDie();
        }

        healthBar.SetHP(curHP);
    }

    private void OnMonsterDie()
    {
        miniWave.listMonsters.Remove(this);
        miniWave.CheckIfAllEnermyDead();
        var indicator = PoolingManager.Spawn(GameController.Instance.spiritStoneIndicator, 
            transform.position, Quaternion.identity);
        indicator.transform.SetParent(GameController.Instance.indicatorParent);
        indicator.ChangeIndicator(spiritStoneAmount);
        this.PostEvent(EventID.On_Monster_Killed, spiritStoneAmount);
        AudioManager.Instance.PlaySFX("Dead", 0.75f);
        //PoolingManager.Despawn(gameObject); 
        Destroy(gameObject);
    }
    
    private void RotateTowardsTarget()
    {
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 90) * 90; 
        sr.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}