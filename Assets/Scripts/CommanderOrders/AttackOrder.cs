using UnityEngine;

public class AttackOrder : MonoBehaviour
{
    public void HandleButtonClick()
{
    // Tìm tất cả các PlayerController
    PlayerController[] playerControllers = GameObject.FindObjectsOfType<PlayerController>();

    // Cập nhật isAtk_Order thành true cho tất cả các PlayerController
    foreach (PlayerController playerController in playerControllers)
    {
        playerController.isAtk_Order = true;
        playerController.isDef_Order = false;
        Debug.Log("Attack Order!!!");
    }
}
}