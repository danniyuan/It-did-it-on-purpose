using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteMarkCollider : MonoBehaviour
{
   /* public SpriteRenderer BiteMarkPrefab; // todo: random prefabs
    */
   // public Transform BiteMarkCalibration;
    //public int c = 1;
    public float Density = 0.5f;
    public List<GameObject> BiteMarkPrefabs = new List<GameObject>();
    public List<GameObject> BiteMarks = new List<GameObject>();
    public AudioClip Clip;
    public void Bite(Vector3 position, Quaternion rotation)
    {
        //Debug.Log("Bit " + position);

        GameObject nearbyMark = BiteMarks.Find(e => Vector3.Distance(e.transform.position, position) < Density);
        if (nearbyMark != null) return;

        int markIndex = Random.Range(0, BiteMarkPrefabs.Count);
        if (Clip != null)
            AudioSource.PlayClipAtPoint(Clip, transform.position);
        GameObject mark = Instantiate(BiteMarkPrefabs[markIndex], transform);

        mark.transform.position = position;
        mark.transform.rotation = rotation;
        Vector3 newRot = mark.transform.localEulerAngles;

        newRot.z = Random.Range(1, 180);
        mark.transform.localEulerAngles = newRot;
        // todo: random angle z
        mark.transform.position -= mark.transform.forward * 0.01f;


        GetComponentInParent<Prop>().AddScore();
        BiteMarks.Add(mark);
    }
}
