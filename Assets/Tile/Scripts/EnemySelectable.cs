using UnityEngine;

public class EnemySelectable : MonoBehaviour
{
    public CommonAttribute stats;

    public EnemyData data;

    void OnMouseDown()
    {
        stats.Hp -= 10;

        stats.Hp = Mathf.Clamp(stats.Hp, 0, data.Hp);

        Debug.Log(stats.Hp);
    }
}