using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{


    public Text InfoText;
    public Text GoldText;
    public Text CurrentWave;
    [Header("Turrets")]
    public GameObject CannonTurret;
    public GameObject MachineGunTurret;
    public GameObject AreaTurret;
    public GameObject FreezeTurret;
    public GameObject SniperTurret;
    public GameObject ToxicTurret;
    [Header("Obstacles")]
    public GameObject Wall;
    [Header("Floor Panels")]
    public GameObject FirePanel;
    [Header(" ")]
    public GameObject LevelClearedUI;
    public GameObject GameOverUI;
    public GameObject NextWaveType;
    [Header("NextLevel")]
    public bool Automatic = true;
    public string LevelName;

    private bool loadVideo = false;

    private int x = 0;


    // Use this for initialization
    void Start()
    {
        AdjustFPS(GlobalVariables.TargetFPS);
        GlobalVariables.LevelCleared = false;
        GlobalVariables.LevelFailed = false;
        LevelClearedUI.SetActive(false);
        GameOverUI.SetActive(false);
        Time.timeScale = 1.0F;

        NormalSpeed();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NormalSpeed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FastFoward();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SuperFastFoward();
        }

        // this should not be on the update but instead only called once every time is needed thats when gold is earned or wasted as well as when damage is taken

        GoldText.text = LocalizationText.GetText("Gold") + ": " + GlobalVariables.Money.ToString();
        CurrentWave.text = LocalizationText.GetText("Wave") + ": " + GlobalVariables.CurrentWave.ToString();

        if (GlobalVariables.LevelCleared == true)
        {
            LevelClearedUI.SetActive(true);
        }
        if (GlobalVariables.LevelFailed == true)
        {
            GameOverUI.SetActive(true);
            if (loadVideo == false)
            {
                GameOverUI.SendMessage("LoadAd");
                loadVideo = true;
            }


        }
        else
        {
            GameOverUI.SetActive(false);
        }
    }
    void OnGUI()
    {



    }
    public void AdjustFPS(float fps)
    {
        Application.targetFrameRate = (int)fps;
    }
    public void CreepSpawner()
    {
        GameObject myCreepSpawner = GameObject.FindGameObjectWithTag("CreepSpawner");
        myCreepSpawner.SendMessage("startCreeps");



    }
    public void InfiniteGold()
    {
        x++;
        Debug.Log(x);
        if (x == 10)
        {
            GlobalVariables.Money = 99999;
            GlobalVariables.HP = 99999;
        }

    }

    public void Pause()
    {

        Time.timeScale = 0.0F;

    }
    public void FastFoward()
    {


        GlobalVariables.GameSpeed = 2.0f;
        Time.timeScale = GlobalVariables.GameSpeed;
        //AdjustFPS (GlobalVariables.TargetFPS*GlobalVariables.GameSpeed);

    }
    public void SuperFastFoward()
    {


        GlobalVariables.GameSpeed = 16.0f;
        Time.timeScale = GlobalVariables.GameSpeed;
        //AdjustFPS (GlobalVariables.TargetFPS*GlobalVariables.GameSpeed);


    }
    public void NormalSpeed()
    {


        GlobalVariables.GameSpeed = 1.0f;
        Time.timeScale = GlobalVariables.GameSpeed;
        //AdjustFPS (GlobalVariables.TargetFPS*GlobalVariables.GameSpeed);

    }

    public void MainMenuButtton()
    {
        Application.LoadLevel("MainMenu");

    }
    public void CannonTurretButton()
    {

        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = CannonTurret;
        SelectTurret cost = (SelectTurret)CannonTurret.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void MachineGunTurretButton()
    {
        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = MachineGunTurret;
        SelectTurret cost = (SelectTurret)MachineGunTurret.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void AreaTurretButton()
    {

        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = AreaTurret;
        SelectTurret cost = (SelectTurret)AreaTurret.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void FreezeTurretButton()
    {
        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = FreezeTurret;
        SelectTurret cost = (SelectTurret)FreezeTurret.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void SniperTurretButton()
    {

        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = SniperTurret;
        SelectTurret cost = (SelectTurret)SniperTurret.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void ToxicTurretButton()
    {

        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = ToxicTurret;
        SelectTurret cost = (SelectTurret)ToxicTurret.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void WallButton()
    {

        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = Wall;
        SelectTurret cost = (SelectTurret)Wall.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void FirePanelButton()
    {

        GlobalVariables.DestroyTurret = false;
        GlobalVariables.CurrentTurret = FirePanel;
        SelectTurret cost = (SelectTurret)FirePanel.GetComponent(typeof(SelectTurret));
        GlobalVariables.TurretCost = cost.getTurretCost();
        InfoText.text = LocalizationText.GetText("TurretCost") + ": " + GlobalVariables.TurretCost.ToString();

    }
    public void DestroyTurretButton()
    {
        GlobalVariables.DestroyTurret = true;
        InfoText.text = LocalizationText.GetText("DestroyTurret");
        GlobalVariables.CurrentTurret = null;
    }
    public void LoadLevel(string LevelToLoad)
    {
        Application.LoadLevel(LevelToLoad);
    }
    public void LoadNextLevel()
    {

        if (Automatic)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(LevelName);
        }

    }
    public void RetryLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void getNextWave(string NextWaveCreepName, Color CreepColor)
    {

        if (NextWaveCreepName == " " || NextWaveCreepName == null)
        {

            NextWaveType.GetComponent<Image>().color = Color.black;
            NextWaveType.GetComponentInChildren<Text>().text = LocalizationText.GetText("LastWave");
        }
        else
        {
            NextWaveType.GetComponent<Image>().color = CreepColor;
            //			Debug.Log (NextWaveCreepName);
            NextWaveType.GetComponentInChildren<Text>().text = LocalizationText.GetText("NextWave") + ": " + NextWaveCreepName;
        }

    }
    public void CombinedWaves()
    {
        NextWaveType.GetComponent<Image>().color = Color.black;
        NextWaveType.GetComponentInChildren<Text>().text = LocalizationText.GetText("CombinedWaves");
    }
    public void UpgradeCurrentTurret()
    {
        if (CurrentTurret.myCurrentTurret != null)
        {
            SelectTurret myTurret = CurrentTurret.myCurrentTurret.GetComponent<SelectTurret>();
            myTurret.UpgradeTurret();
            myTurret.UpdateMaterial(false);
            //CurrentTurret.myCurrentTurret.SendMessage ("UpgradeTurret");
            //			Debug.Log("el nuevo nivel de la torre es!!!!! : "+CurrentTurret.myCurrentTurret.GetComponent<SelectTurret>().TurretLevel);
        }

    }
    public void DestroyElement(GameObject myObject)
    {
        Destroy(myObject);
    }
    public void ConfirmTurret(bool confirm)
    {

        if (confirm)
        {

            GlobalVariables.CurrentTurretPlace.SendMessage("PlaceTurret");

        }
        else
        {
            GlobalVariables.CurrentTurretPlace.SendMessage("destroyPlaceHolder");

        }


    }

    public void EnableCameraRender()

    {

        Camera.main.GetComponent<Camera>().enabled = true;


    }


}
