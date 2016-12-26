using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Fdp.DataAccess.DBConnection
{
    public  class TNSNamesReader
    {
        public  List<string> GetOracleHomes()
        {
            List<string> oracleHomes = new List<string>();
            RegistryKey rgkLM = Registry.LocalMachine;
            RegistryKey rgkAllHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE");
            if (rgkAllHome != null)
            {
                foreach (string subkey in rgkAllHome.GetSubKeyNames())
                {
                    if (subkey.StartsWith("KEY_"))
                        oracleHomes.Add(subkey);
                }
            }
            return oracleHomes;
        }

        private  string GetOracleHomePath(String OracleHomeRegistryKey)
        {
            RegistryKey rgkLM = Registry.LocalMachine;
            RegistryKey rgkOracleHome = rgkLM.OpenSubKey(@"SOFTWARE\ORACLE\" +
                OracleHomeRegistryKey);

            if (!rgkOracleHome.Equals(""))
                return rgkOracleHome.GetValue("ORACLE_HOME").ToString();
            return "";
        }

        private  string GetTNSNAMESORAFilePath(String OracleHomeRegistryKey)
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

        private  string TNSfileTxt;
        public  List<string> LoadTNSNames(string OracleHomeRegistryKey )
        {
            List<string> DBNamesCollection = new List<string>();
            string RegExPattern = @"[\n][\s]*[^\(][a-zA-Z0-9_.]+[\s]";//*=[\s]*\(
            string strTNSNAMESORAFilePath = GetTNSNAMESORAFilePath(OracleHomeRegistryKey);

            if (!strTNSNAMESORAFilePath.Equals(""))
            {
                //check out that file does physically exists
                FileInfo fiTNS = new FileInfo(strTNSNAMESORAFilePath);
                if (fiTNS.Exists)
                {
                    if (fiTNS.Length > 0)
                    {
                        //read tnsnames.ora file
                        TNSfileTxt = File.ReadAllText(fiTNS.FullName);
                        var matchCollection = Regex.Matches(TNSfileTxt, RegExPattern);

                        int i;
                        for (i = 0; i < matchCollection.Count; i++)
                        {
                            DBNamesCollection.Add(matchCollection[i].Value.Trim());//.Trim().Substring(0, matchCollection[i].Value.Trim().IndexOf(" ")));
                        }
                    }
                }
            }
            return DBNamesCollection;
        }

        public  string GetDataSource(string SelectedTNSName)
        {
            string DataSource = string.Empty;

            var RegExPattern = $@"(?<=\b{SelectedTNSName}\s?=\s+?)\([^\(\)]*(((?<Open>\()[^\(\)]*)+((?<Settings-Open>\))[^\(\)]*)+)*(?(Open)(?!))\)";
            var matchCollection = Regex.Matches(TNSfileTxt, RegExPattern);
            DataSource=matchCollection[0].ToString();
            return DataSource;
        }
    }
}
