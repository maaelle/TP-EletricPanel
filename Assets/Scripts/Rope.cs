using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    struct RopePoint
    {
        public Vector3 position;
        public Vector3 oldPosition;
        public bool isFree;
    }

    [Header("Rope Attach")]
    public Transform ropeStart;
    public Transform ropeEnd;
    RopePoint[] points;

    [Header("Rope Settings")]
    [Min(2)] public int lineSegments = 10;
    [Min(0)] public float lineLength = 1.3f;
    [Min(1)] public int solverIterations = 3;
    public float gravity;

    LineRenderer line;

    public float SegmentLength { get => lineLength / lineSegments; }

    // Start is called before the first frame update
    void Start()
    {
        CreateRope();
        line = GetComponent<LineRenderer>();
        line.positionCount = lineSegments;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineVisuals();
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    void CreateRope()
    {
        points = new RopePoint[lineSegments];

        for (int i = 0; i < lineSegments; i++)
        {
            points[i].position = Vector3.Lerp(ropeStart.position, ropeEnd.position, (float)(i+1)/lineSegments);
            points[i].oldPosition = points[i].position;
            points[i].isFree = true;
        }

        //Les extrémités ne sont pas libres
        points[0].isFree = false;
        points[lineSegments-1].isFree = false;
    }

    void Simulate()
    {
        //Apply movement
        for (int i = 0; i < lineSegments; i++)
        {
            ApplyDisplacement(i);
        }

        //Update old pos
        for (int i = 0; i < lineSegments; i++)
        {
            points[i].oldPosition = points[i].position;
        }

        //Initialisation de la position des extrémités.
        points[0].position = ropeStart.position;
        points[lineSegments - 1].position = ropeEnd.position;

        for (int i = 0; i < solverIterations; i++)
        {
            ApplyConstraints();
        }
    }

    void ApplyConstraints()
    {
        for (int i = 0; i < lineSegments - 1; i++)
        {
            ApplyDistanceConstraint(i, i + 1, SegmentLength);
        }
    }

    void ApplyDisplacement(int i)
    {
        Vector3 deplacement = (points[i].position - points[i].oldPosition);
        Vector3 deplacementCorrige = deplacement * 0.3f;
        points[i].position = (points[i].position + deplacementCorrige);
    }

    void ApplyDistanceConstraint(int pointA, int pointB, float desiredSegmentLength)
    {/* 
      * tentative non réussie
      * logique : trouver le vecteur AB, puis trouver la distance à ajouter
      * normaliser le vectauer AB et multiplier par la distance pour avoir le vecteur de translation 
      * ajouter le vecteur de translation au point
      *        
        Vector3 SegmentDistance = (points[pointB].position - points[pointA].position);
        float difference = SegmentDistance.magnitude - desiredSegmentLength;
        Vector3 shift = SegmentDistance.normalized * difference;

        if (points[pointA].isFree == false)
        {
            points[pointB].position = (points[pointB].position + shift);
        }
        if (points[pointB].isFree == false)
        {
            points[pointA].position = (points[pointA].position + shift*(-1));
        }
        else
        { 
            difference = (SegmentDistance.magnitude - desiredSegmentLength)/2;
            shift = SegmentDistance.normalized * difference;
            points[pointB].position = (points[pointB].position + shift*(-1));
            points[pointA].position = (points[pointA].position + shift);
        } */
    }

    void UpdateLineVisuals()
    {
        for (int i = 0; i < lineSegments; i++)
        {
            line.SetPosition(i, points[i].position);
        }
    }
}
