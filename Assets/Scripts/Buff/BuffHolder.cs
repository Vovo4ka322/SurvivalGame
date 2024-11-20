using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffHolder", menuName = "Buff/BuffHolder")]
public class BuffHolder : ScriptableObject
{
    private List<Buff> _baffs = new();

    public IReadOnlyList<Buff> Baffs => _baffs;

    public void Add(Buff baff)//�������� �� ��, ��� ���� ��� �������
    {
        _baffs.Add(baff);
    }
}
