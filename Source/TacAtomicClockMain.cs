﻿/**
 * TacAtomicClockMain.cs
 * 
 * Thunder Aerospace Corporation's Atomic Clock for the Kerbal Space Program, by Taranis Elsu
 * 
 * (C) Copyright 2013, Taranis Elsu
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 * 
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 * 
 * Non-commercial - You may not use this work for commercial purposes.
 * 
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using KSP.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tac
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class TacAtomicClockMain : MonoBehaviour
    {
        public static TacAtomicClockMain Instance { get; private set; }

        private string filename;
        private Settings settings;
        private MainWindow mainWindow;
        private SettingsWindow settingsWindow;
        private HelpWindow helpWindow;

        public delegate void Update(bool visible);
        public event Update Observers;

        void Awake()
        {
            this.Log("Awake");
            Instance = this;

            settings = new Settings();

            settingsWindow = new SettingsWindow(settings);
            helpWindow = new HelpWindow();
            mainWindow = new MainWindow(settings, settingsWindow, helpWindow);
            mainWindow.SetVisible(true);

            filename = IOUtils.GetFilePathFor(this.GetType(), "TacAtomicClock.cfg");
        }

        void Start()
        {
            this.Log("Start");
        }

        void FixedUpdate()
        {
            if (FlightGlobals.ready && mainWindow.IsVisible())
            {
                Vessel vessel = FlightGlobals.ActiveVessel;
                if (vessel != null)
                {
                    if (!vessel.parts.Any(p => p.Modules.Contains("TacAtomicClock")))
                    {
                        mainWindow.SetVisible(false);
                    }
                }
            }
        }

        public void Load()
        {
            try
            {
                if (File.Exists<TacAtomicClockMain>(filename))
                {
                    ConfigNode config = ConfigNode.Load(filename);
                    this.Log("loaded from file: " + config);

                    settings.Load(config);

                    mainWindow.Load(config);
                    settingsWindow.Load(config);
                    helpWindow.Load(config);

                    if (Observers != null)
                    {
                        Observers(mainWindow.IsVisible());
                    }
                }
                else
                {
                    this.Log("failed to load file: file does not exist");
                }
            }
            catch (Exception ex)
            {
                this.LogWarning("failed to load config file: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void Save()
        {
            try
            {
                ConfigNode config = new ConfigNode();

                settings.Save(config);

                mainWindow.Save(config);
                settingsWindow.Save(config);
                helpWindow.Save(config);

                config.Save(filename);
                this.Log("saved to file: " + config);
            }
            catch (Exception ex)
            {
                this.LogWarning("failed to save config file: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void SetVisible(bool newValue)
        {
            mainWindow.SetVisible(newValue);

            if (Observers != null)
            {
                Observers(mainWindow.IsVisible());
            }
        }

        public bool IsVisible()
        {
            return mainWindow.IsVisible();
        }
    }
}
