using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isChosen = false;
    public float moveSpeed = 5f;
    public float searchRadius = 5f;
    public float attackCooldown=0.7f;
    public float highPos = -4.5f; // Giá trị Y khi prefab đạt kích thước lớn nhất
    public float lowPos = -2.5f;  // Giá trị Y khi prefab đạt kích thước nhỏ nhất
    public float minScale = 0.75f; // Kích thước nhỏ nhất
    public float maxScale = 1.5f; // Kích thước lớn nhất
    private int minOrderLayer=0;
    private int maxOrderLayer=20;


    public bool isAtk_Order = false;
    public bool isDef_Order = true;
    private GameObject target;
    private Animator amt;

    private GameObject joystickContainer;
    private Joystick joystick;
    private Vector2 movement;
    private Vector3 currentDirection;
    private float currentScale;
    float currentY ;// vị trí y hiện tại
    float scale=1;
    float truePossitionY;
    int orderLayer=0;
    private Renderer rend;
    

    void Start()
    {
         rend = GetComponent<Renderer>();
        amt = GetComponent<Animator>();
        //scale for Player
       currentScale = transform.localScale.x; // scale mặc định
        minScale = currentScale *minScale;
        maxScale=currentScale* maxScale;
            

        GameObject battleCanva = GameObject.Find("BattleCanva");
        if (battleCanva != null)
        {
            joystickContainer = battleCanva.transform.Find("JoyStickCanva").gameObject;
            joystick = joystickContainer.transform.Find("Fixed Joystick").GetComponent<Joystick>();
        }
    }

    void Update()
    {

        if (isChosen)
        {
            MoveByJoystick();
        }
        else
        {
            if (isAtk_Order)
            {
                FindAndMoveToClosestEnemy(searchRadius);
            }
            else if (isDef_Order)
            {
                // Logic phòng thủ
            }
        }
        currentY = transform.position.y;
        UpdateCharacterDirection();// chỉnh hướng char
        ScaleMovement();// chỉnh scale char
        SetOrderLayer();// chỉnh Layer char
    }
    
void FixedUpdate()
    {
      
    }

    void UpdateCharacterDirection()
    {
        if (isChosen)
        {
            currentDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        } else{

         if (target != null
         )
     {
        // Cập nhật currentDirection dựa trên hướng di chuyển về phía địch
        currentDirection = (target.transform.position - transform.position).normalized;
      }
        else if(target==null && isAtk_Order )
     {// 
        currentDirection=new Vector3(1, 0, joystick.Vertical);
        
        }
      else
     {
        currentDirection = Vector3.zero; // Nếu không có mục tiêu và không phải là isChosen, giữ currentDirection là zero
     }}


     
        
        isRunning(currentDirection);
      
    }

    void isRunning(Vector3 currentDirection)
    {
        amt.SetBool("isRunning", currentDirection.magnitude > 0.1f);
        Flip();
    }

    void Flip()
    {
        //isFacingRight = !isFacingRight;
        //transform.Rotate(0f, 180f, 0f);
         if (currentDirection.x > 0) // Di chuyển sang phải
    {  
        Debug.Log("Xoay Phải");

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

    }
    else if (currentDirection.x < 0) // Di chuyển sang trái
    {
                Debug.Log("Xoay Trái");

        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    }

    void MoveByJoystick()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void FindAndMoveToClosestEnemy(float searchRadius)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < searchRadius && distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy;
            currentDirection = (target.transform.position - transform.position).normalized;
            MoveTowardTarget();
        }
        else
        {
            movement = Vector3.right * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    void MoveTowardTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            movement = direction * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
            //isRunning(movement);
           
        }
    }
    void ScaleMovement(){
        // Tính toán tỉ lệ scale dựa trên tọa độ Y
       
         scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(lowPos, highPos, currentY));
         transform.localScale = new Vector3(scale, scale, scale);
}
    void SetOrderLayer(){

        orderLayer = (int) Mathf.Lerp(minOrderLayer,maxOrderLayer, Mathf.InverseLerp(lowPos, highPos, currentY));
        rend.sortingOrder = orderLayer;
    }
}
