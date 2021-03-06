﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tac
{
    class SettingsWindow : Window<SettingsWindow>
    {
        private readonly Settings settings;
        private readonly string version;
        private bool showAdvanced = false;

        private GUIStyle labelStyle;
        private GUIStyle editStyle;
        private GUIStyle buttonStyle;
        private GUIStyle versionStyle;

        public SettingsWindow(Settings settings)
            : base("TAC Clock Settings", 180, 230)
        {
            this.settings = settings;
            version = Utilities.GetDllVersion(this);
        }

        protected override void ConfigureStyles()
        {
            base.ConfigureStyles();

            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.wordWrap = false;
                labelStyle.fontStyle = FontStyle.Normal;
                labelStyle.normal.textColor = Color.white;

                editStyle = new GUIStyle(GUI.skin.textField);

                buttonStyle = new GUIStyle(GUI.skin.button);

                versionStyle = Utilities.GetVersionStyle();
            }
        }

        protected override void DrawWindowContents(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Universal Time", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.showingUniversalTime = GUILayout.Toggle(settings.showingUniversalTime, "");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Earth Time", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.showingEarthTime = GUILayout.Toggle(settings.showingEarthTime, "");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Kerbin Time", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.showingKerbinTime = GUILayout.Toggle(settings.showingKerbinTime, "");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Kerbin Mission Time", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.showingKerbinMissionTime = GUILayout.Toggle(settings.showingKerbinMissionTime, "");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Real Time", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.showingRealTime = GUILayout.Toggle(settings.showingRealTime, "");
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            settings.showMonth = GUILayout.Toggle(settings.showMonth, "Show Month");

            GUILayout.Space(20);

            if (!showAdvanced)
            {
                if (GUILayout.Button("Advanced", buttonStyle, GUILayout.ExpandWidth(true)))
                {
                    showAdvanced = true;
                }
            }
            else
            {
                if (GUILayout.Button("Simple", buttonStyle, GUILayout.ExpandWidth(true)))
                {
                    showAdvanced = false;
                    SetSize(10, 10);
                }

                GUILayout.BeginHorizontal();
                GUILayout.Label("Initial offset in Earth seconds", labelStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                settings.initialOffsetInEarthSeconds = Utilities.ShowTextField(settings.initialOffsetInEarthSeconds, 10, editStyle, GUILayout.MinWidth(50));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Kerbin seconds per minute", labelStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                settings.kerbinSecondsPerMinute = Utilities.ShowTextField(settings.kerbinSecondsPerMinute, 10, editStyle, GUILayout.MinWidth(50));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Kerbin minutes per hour", labelStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                settings.kerbinMinutesPerHour = Utilities.ShowTextField(settings.kerbinMinutesPerHour, 10, editStyle, GUILayout.MinWidth(50));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Kerbin hours per day", labelStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                settings.kerbinHoursPerDay = Utilities.ShowTextField(settings.kerbinHoursPerDay, 10, editStyle, GUILayout.MinWidth(50));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Kerbin days per month", labelStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                settings.kerbinDaysPerMonth = Utilities.ShowTextField(settings.kerbinDaysPerMonth, 10, editStyle, GUILayout.MinWidth(50));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Kerbin days per year", labelStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                settings.kerbinDaysPerYear = Utilities.ShowTextField(settings.kerbinDaysPerYear, 10, editStyle, GUILayout.MinWidth(50));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Kerbin months per year", labelStyle, GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                settings.kerbinMonthsPerYear = Utilities.ShowTextField(settings.kerbinMonthsPerYear, 10, editStyle, GUILayout.MinWidth(50));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
                settings.debug = GUILayout.Toggle(settings.debug, "Debug");
            }

            GUILayout.EndVertical();

            GUILayout.Space(8);
            GUI.Label(new Rect(4, windowPos.height - 14, windowPos.width - 20, 12), "TAC Atomic Clock v" + version, versionStyle);
        }
    }
}
