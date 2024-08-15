using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LP.TurnBasedStrategyTutorial;
using DG.Tweening;

public class DamagePopUp : MonoBehaviour
{
    //This code is to control the damage and heal bar popUps for the enemy and player


    private TextMeshProUGUI textMesh;
    private const float disappearTimer = 1f;
    private const float disappearDelay = 2f;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color healColor = Color.green;
    [SerializeField] private Color PlayerHealthColor = Color.black;
    [SerializeField] private Color EnemyHealthColor = Color.blue;


    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>(); 
    }

    public void Setup(int damageAmount, bool isDamage = true) 
    {
        textMesh.text=damageAmount.ToString();
        textMesh.alpha = 1;
        textMesh.color = isDamage ? damageColor : healColor;
        textMesh.DOKill();
        textMesh.DOFade(0, disappearTimer).SetDelay(disappearDelay).SetEase(Ease.InSine); 
    }

 /*   public void setup(int scoreAmount, bool isScore = true)
    {
        textMesh.text = scoreAmount.ToString();
        textMesh.alpha = 1;
        textMesh.color = isScore ? PlayerHealthColor : EnemyHealthColor;
        textMesh.DOKill();
        textMesh.DOFade(0, disappearTimer).SetDelay(disappearDelay).SetEase(Ease.InSine);
    }
 */
}
 