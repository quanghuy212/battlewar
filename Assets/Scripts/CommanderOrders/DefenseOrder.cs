using UnityEngine;

public class DefenseOrder : MonoBehaviour
{
   public void HandleButtonClick()
{
    // Tìm tất cả các PlayerController
    PlayerController[] playerControllers = GameObject.FindObjectsOfType<PlayerController>();

    // Cập nhật isAtk_Order thành true cho tất cả các PlayerController
    foreach (PlayerController playerController in playerControllers)
    {
        playerController.isAtk_Order = false;
        playerController.isDef_Order = true;
        Debug.Log("Defense Order!!!");
    }
}
}