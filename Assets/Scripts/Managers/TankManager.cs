﻿using System;
using UnityEngine;

[Serializable]
public class TankManager : AgentManagerInterface
{
    public Transform m_SpawnPoint;
    [HideInInspector] public Color m_PlayerColor;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Wins;
    [HideInInspector] public int m_TargetsKilled;
    [HideInInspector] public TankMovement m_Movement;

    private Shooting m_Shooting;
    private GameObject m_CanvasGameObject;

    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<TankMovement>();
        Action.tank = m_Movement;

        m_Shooting = m_Instance.GetComponent<Shooting>();
        m_Shooting.m_TankInstance = this;

        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">TANK " + m_PlayerNumber + "</color>";

        ColorIt();
    }

    public void setIntelligence(ActionGame[] ADN, Connaissances connaissances)
    {
        m_Movement.setADN(ADN);
        m_Movement.setConnaissances(connaissances);
    }


    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_TargetsKilled = 0;

        ColorIt();

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

    private void ColorIt()
    {
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }
}