using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code_Singleton<T> : MonoBehaviour where T :MonoBehaviour //Ràng buộc để T chỉ có thể là lớp con của MonoBehaviour
{
    private static T _instance;


    // ✅ Biến cho phép lớp con quyết định có gọi DontDestroyOnLoad hay không
    protected virtual bool UseDontDestroyOnLoad => false;


    public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;   
            if (UseDontDestroyOnLoad)
            {
                gameObject.transform.parent = null;
                DontDestroyOnLoad(gameObject);
            }        // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
