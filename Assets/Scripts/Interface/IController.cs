﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tank;

public interface IController
{
    void OnDeath();
    IModel GetModel();
}