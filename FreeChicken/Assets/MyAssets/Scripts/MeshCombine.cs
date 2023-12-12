using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombine : MonoBehaviour
{

    void Start()
    {
        CombineInstance[] combine = new CombineInstance[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            // �� �ڽ� ������Ʈ�� MeshFilter ������ ��������
            MeshFilter childMeshFilter = transform.GetChild(i).GetComponent<MeshFilter>();

            // ������ Mesh�� ���� CombineInstance�� �Ҵ�
            combine[i].mesh = Instantiate(childMeshFilter.sharedMesh);

            // �� �ڽ� ������Ʈ�� ���� Ʈ�������� �����Ͽ� ������ ��ġ�� ����
            combine[i].transform = childMeshFilter.transform.localToWorldMatrix;

            // ���� �ڽ� ������Ʈ ��Ȱ��ȭ (�ʿ信 ���� Ȱ��ȭ ���¸� ������ �� ����)
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // ���� ������Ʈ�� MeshFilter �� MeshRenderer �߰�
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        // �������� Mesh�� ����
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine, true, true);

        meshFilter.mesh = combinedMesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // ���� ������Ʈ�� �ִ� ���� ����
        meshRenderer.sharedMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

        // MeshCollider Combine�� ���� ���յ� �޽ÿ� �°� �ڵ����� ����
        MeshCollider meshColliderOnCombinedObject = gameObject.AddComponent<MeshCollider>();
        meshColliderOnCombinedObject.sharedMesh = combinedMesh;
    }
}





