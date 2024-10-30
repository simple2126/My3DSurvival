using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }

    void Update()
    {
        if (CharacterManager.Instance.Player.controller.isInvincibility) return;
        health.Substract(health.passiveValue * Time.deltaTime);
        if(health.curValue == 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("ав╬З╢ы.");
    }
}
