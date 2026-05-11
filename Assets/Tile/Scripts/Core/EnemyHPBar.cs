using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public CommonAttribute stats;

    public EnemyData data;

    public Image fillImage;

    public Text hpText;

    void Update()
    {
        // درصد HP
        float hpPercent = (float)stats.Hp / data.Hp;

        // مقدار HP Bar
        fillImage.fillAmount = hpPercent;

        // متن HP
        hpText.text = stats.Hp + " / " + data.Hp;

        // 🎨 تغییر رنگ
        if (hpPercent > 0.6f)
        {
            fillImage.color = Color.green;
        }
        else if (hpPercent > 0.3f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }

        // نگاه به دوربین
        transform.LookAt(Camera.main.transform);
    }
}