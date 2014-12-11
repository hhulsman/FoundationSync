﻿using System;
using System.Collections.Generic;
using Microsoft.SharePoint.Administration;

namespace Nauplius.SP.UserSync
{
    public class FoudationSync : SPDiagnosticsServiceBase
    {
        public static string NaupliusDiagnosticArea = "Nauplius";
        public FoudationSync()
            : base(DefaultName, SPFarm.Local)
        {
        }

        public static FoudationSync Local
        {
            get
            {
                return SPFarm.Local.Services.GetValue<FoudationSync>(DefaultName);
            }
        }

        public static class LogCategories
        {
            public static string FoundationSync = "FoundationSync";
        }

        public static string DefaultName
        {
            get
            {
                return NaupliusDiagnosticArea;
            }
        }

        public static string AreaName
        {
            get
            {
                return NaupliusDiagnosticArea;
            }
        }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {

            var areas = new List<SPDiagnosticsArea>
            {
                new SPDiagnosticsArea(NaupliusDiagnosticArea, 0, 0, false, new List<SPDiagnosticsCategory>
                    {
                        new SPDiagnosticsCategory(LogCategories.FoundationSync, null, TraceSeverity.Medium, EventSeverity.Information, 0, 0, false, true),
                    })
            };
            return areas;
        }

        public static void LogMessage(ushort id, string LogCategory, TraceSeverity traceSeverity, string message, object[] data)
        {
            try
            {
                var log = Local;

                if (log == null) return;
                var category = log.Areas[NaupliusDiagnosticArea].Categories[LogCategory];
                log.WriteTrace(id, category, traceSeverity, message, data);
            }
            catch (Exception)
            {
            }
        }
    }
}
