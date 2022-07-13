using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int type = 0;
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        //����� 10 ������ ���� �������� ���� �� ������ � ����
        Destroy(gameObject, 10);
    }
    /// <summary>
    /// ��������� ���� �� �� ������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.TryGetComponent(out AcceptDamage p))
        {
            
            p.MainPugalo.GetComponent<Pugalo>().GetDamage(damage,type);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
