﻿// This file is part of Mystery Dungeon eXtended.

// Copyright (C) 2015 Pikablu, MDX Contributors, PMU Staff

// Mystery Dungeon eXtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Mystery Dungeon eXtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Mystery Dungeon eXtended.  If not, see <http://www.gnu.org/licenses/>.


namespace Client.Logic.Missions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PMDCP.Core;

    class MissionHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static MissionCollection mMissions;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static MissionCollection Missions
        {
            get { return mMissions; }
        }

        #endregion Properties

        #region Methods

        public static void InitMissionCollection()
        {
            mMissions = new MissionCollection();
        }

        public static void LoadMissionsFromPacket(string[] parse)
        {
            try {
                int n = 2;
                int max = parse[1].ToInt();
                mMissions.ClearMissions();
                if (max > 0) {
                    for (int i = 0; i < max; i++) {
                        dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, max));
                        mMissions.AddMission(i, new Mission());
                        mMissions[i].Title = parse[n];
                        n += 1;
                        ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Recieving Data... " + DataManager.AverageLoadPercent().ToString() + "%");
                    }
                    dataLoadPercent = 100;
                }
            } catch (Exception ex) {
                Exceptions.ExceptionHandler.OnException(ex);
            }
        }

        #endregion Methods
    }
}