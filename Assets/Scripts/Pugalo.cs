using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
public class Pugalo : MonoBehaviour
{
    public InputActionReference Restore = null;
    public TextMeshProUGUI HP_text;
    public int State = 0;
    public int HP = 1000;
    public int Shkala = 0;
    public bool CanPress = true;
    public List<Material> Materials;
    public List<GameObject> Things;
    public Canvas canva;
    public Slider HealthBar;
    public List<GameObject> Hide;
    Coroutine Fire;
    public Image ShkalaMokrosi;
    //0 norm
    //1 wet
    //2 fire
    // Start is called before the first frame update

    /// <summary>
    /// Смена от чёрного до голубого
    /// </summary>
    void ChangeShkalaMokrosti()
    {
        ShkalaMokrosi.color = new Color(0, 0, Shkala/ 255f, 255);
      //  Debug.Log(ShkalaMokrosi.color);

    }

  
    void Start()
    {
        HP_text.text = HP.ToString();
        ChangeShkalaMokrosti();
    }
    /// <summary>
    /// Восстановление пугала
    /// </summary>
    void Heal()
    {
        if (Fire != null)
            StopCoroutine(Fire);
        HP = 1000;
        State = 0;
        Shkala = 0;
        foreach (var obj in Hide)
        {
            obj.SetActive(true);

        }
        HP_text.text = HP.ToString();
        ChangeColorSlider();

        foreach (var part_clothe in Things)
            part_clothe.GetComponent<Renderer>().material = Materials[0];
        canva.enabled = true;
    }
    /// <summary>
    /// Корутина для горения
    /// </summary>
    /// <param name="text">Поле для отображения хп</param>
    /// <returns></returns>
    IEnumerator FireOn(TextMeshProUGUI text)
    {
        for (int i = 0; i < 10; i++)
         { HP = HP - 5;
             text.text = HP.ToString();
            ChangeColorSlider();
            yield return new WaitForSeconds(1);
         }
        State = 0;
        foreach (var part_clothe in Things)
            part_clothe.GetComponent<Renderer>().material = Materials[0];
    }
    /// <summary>
    /// Смена материалов и вызов горения
    /// </summary>
    /// <param name="power">сила</param>
    /// <param name="text">текстовое поле для отображения</param>
    void ChangeState(int power, TextMeshProUGUI text)
    {
        if (power == 1)
        {
            if (Fire != null)
                StopCoroutine(Fire);
            if (Shkala + 10 <= 100)
                Shkala = Shkala + 10;
            else
                Shkala = 100;
            if (Things[0].GetComponent<Renderer>().material != Materials[1])
            { 
                foreach(var part_clothe in Things)
                    part_clothe.GetComponent<Renderer>().material = Materials[1];
            }
            State = 1;
            Debug.Log("мкорый");
        }

        if (power == 2)
        {
            
            if(Shkala>0)
            Shkala--;
            if (Shkala == 0)
            {
                if (Things[0].GetComponent<Renderer>().material != Materials[2])
                {
                    foreach (var part_clothe in Things)
                        part_clothe.GetComponent<Renderer>().material = Materials[2]; 
                }
                if(Fire!=null)
                StopCoroutine(Fire);
                Fire=StartCoroutine(FireOn(text));
                State = 2;
            }
           
        }
      
      
       
        


    }
    /// <summary>
    /// Смена цвета слайдера в зависимоти от хп
    /// </summary>
    void ChangeColorSlider()
    {
        if(HP>=500)
            HealthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
        if(HP<500 && HP >=250)
            HealthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
        if(HP<250)
            HealthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.red;

     

    }
    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="damage"> Количество урона</param>
    /// <param name="type">Тип урона</param>
    public void GetDamage(int damage,int type=0)
    {
       
        switch (type)
        {
        case 0://пистолеты
                switch (State)
                {
                    case 2:
                        HP = HP - damage - 10;
                        break;
                    case 1:
                        HP = HP - damage + 10;
                        break;
                    case 0:
                        HP = HP - damage;
                        break;
                }
                break;
        case 2://огонь
                HP = HP - damage;
                break;  
        
        }
        ChangeState(type,HP_text);
        HP_text.text = HP.ToString();
        ChangeColorSlider();
    }
    // Update is called once per frame
    void Update()
    {
        
        ChangeShkalaMokrosti();
        if (Restore.action.ReadValue<float>() == 1 && CanPress)
            Heal(); 
        HealthBar.value = HP;
        if (HP <= 0)
        {
            foreach (var obj in Hide)
            {
                obj.SetActive(false);
            
            }
            canva.enabled = false;
        }
    }
}
