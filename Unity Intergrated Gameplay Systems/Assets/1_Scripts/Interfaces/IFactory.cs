using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//T is return Type
//U is dictionary type

public interface IFactory<T, U>
{
    Dictionary<string, U> Dictionary { get; set; }
    void InitializeDictionary();
    T Create(string key);
}
