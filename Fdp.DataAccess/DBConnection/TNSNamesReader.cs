using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Fdp.DataAccess.DBConnection
{
    public class TNSNamesReader
    {
        public List<string> GetOracleHomes()
        {
            List<string> oracleHomes = new List<string>();
            RegistryKey rgkLM = Registry.LocalMachine;
            RegistryKey rgkAllHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE");
            if (rgkAllHome != null)
            {
                foreach (string subkey in rgkAllHome.GetSubKeyNames())
                {
                    if (subkey.StartsWith("KEY_", StringComparison.CurrentCulture))
                        oracleHomes.Add(subkey);
                }
            }
            return oracleHomes;
        }

        private string GetOracleHomePath(String OracleHomeRegistryKey)
        {
            RegistryKey rgkLM = Registry.LocalMachine;
            RegistryKey rgkOracleHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE\" +
                OracleHomeRegistryKey);

            if (!rgkOracleHome.Equals(""))
                return rgkOracleHome.GetValue("ORACLE_HOME").ToString();
            return "";
        }

        private string GetTNSNAMESORAFilePath(String OracleHomeRegistryKey)
        {
            string oracleHomePath = GetOracleHomePath(OracleHomeRegistryKey);
            string tnsNamesOraFilePath = "";
            if (!oracleHomePath.Equals(""))
            {
                tnsNamesOraFilePath = oracleHomePath + @"\NETWORK\ADMIN\TNSNAMES.ORA";
                if (!(File.Exists(tnsNamesOraFilePath)))
                {
                    tnsNamesOraFilePath = oracleHomePath + @"\NET80\ADMIN\TNSNAMES.ORA";
                }
            }
            return tnsNamesOraFilePath;
        }

        public void SetTnsFileText(string OracleHomeRegistryKey, bool GetFilePathFromRegistery)
        {
            string strTNSNAMESORAFilePath = GetTNSNAMESORAFilePath(OracleHomeRegistryKey);
            SetTnsFileText(strTNSNAMESORAFilePath);

        }

        public void SetTnsFileText(string FilePath)
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                FileInfo fiTNS = new FileInfo(FilePath);
                if (fiTNS.Exists && fiTNS.Length > 0)
                {
                    TNSfileTxt = File.ReadAllText(fiTNS.FullName);

                }
            }
        }

        private string TNSfileTxt;
        public List<string> LoadTnsNames()
        {
            List<string> DBNamesCollection = new List<string>();
            string RegExPattern = @"[\n][\s]*[^\(][a-zA-Z0-9_.]+[\s]";

            if (!string.IsNullOrEmpty(TNSfileTxt))
            {
                var matchCollection = Regex.Matches(TNSfileTxt, RegExPattern);

                int i;
                for (i = 0; i < matchCollection.Count; i++)
                {
                    DBNamesCollection.Add(matchCollection[i].Value.Trim());
                }
            }

            return DBNamesCollection;
        }

        public string GetDataSource(string SelectedTNSName)
        {
            var RegExPattern = $@"(?<=\b{SelectedTNSName}\s?=\s+?)\([^\(\)]*(((?<Open>\()[^\(\)]*)+((?<Settings-Open>\))[^\(\)]*)+)*(?(Open)(?!))\)";
            var matchCollection = Regex.Matches(TNSfileTxt, RegExPattern);
            if (matchCollection.Count > 0)
                return matchCollection[0].ToString();
            return null;
        }
    }
}
