using UnityEngine;
using SRF;
using SRDebugger.Services;

public class GameDebugger : MonoBehaviour
{
  public int userLevel = 1;
    public int totalLevels = 10;
    private bool[] levelsCompleted;

    void Start()
    {
        // Khởi tạo mảng để theo dõi các màn chơi đã hoàn thành
        levelsCompleted = new bool[totalLevels];

        // Thêm các tùy chọn vào bảng Debug của SRDebugger
        SRDebug.Instance.AddOptionContainer(this);
    }


    public void IncreaseUserLevel()
    {
        userLevel++;
        Debug.Log("User Level Increased: " + userLevel);
    }

    public void DecreaseUserLevel()
    {
        if (userLevel > 1)
        {
            userLevel--;
            Debug.Log("User Level Decreased: " + userLevel);
        }
    }

    public void CompleteLevel(int level)
    {
        if (level < 1 || level > totalLevels)
        {
            Debug.LogWarning("Invalid level number!");
            return;
        }

        levelsCompleted[level - 1] = true;
        Debug.Log("Level " + level + " completed!");
    }

}
