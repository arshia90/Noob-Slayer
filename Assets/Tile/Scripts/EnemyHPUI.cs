using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUI : MonoBehaviour
{
    public static EnemyHPUI instance;

    public CommonAttribute enemyStats;

    public Text hpText;

    void Awake()
    {
        instance = this;
    }

    public void SetTarget(CommonAttribute target)
    {
        enemyStats = target;
    }

    void Update()
    {
        if (enemyStats != null)
        {
            hpText.text = "HP : " + enemyStats.Hp;
        }
        else
        {
            hpText.text = "No Enemy";
        }
    }
}