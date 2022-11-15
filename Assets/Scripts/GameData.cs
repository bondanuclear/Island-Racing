
[System.Serializable]
public class GameData 
{
    public int totalPoints;
    public float musicLoudness;
    public bool musicIsOff;
    public bool[] levelsPassed;
    public bool[] charactersUnlocked;
    public string carToLoad;
    public int savedCarIndex;
    public float currentCarMotorForce;
    public float currentCarBrakeForce;
    public GameData()
    {
        totalPoints = 0;
        savedCarIndex = 0;
        musicLoudness = 0.3f;
        musicIsOff = false;
        levelsPassed = new bool[14];
        levelsPassed[0] = true;
        charactersUnlocked = new bool[4];
        charactersUnlocked[0] = true;
        carToLoad = "DuneBuggy";
        currentCarMotorForce = 3000;
        currentCarBrakeForce = 8000;
    }
}
