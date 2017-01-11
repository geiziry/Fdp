using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;

namespace Fdp.UITest
{
    public class TestsBase
    {
        const string ApplicationPath = @"F:\KOC Development Projects\Dashboard\Fdp\Fdp.UI\bin\Debug\Fdp.UI.exe";
        public TestsBase()
        {
            Application application = Application.Launch(ApplicationPath);
            Windows.Init(application);

            ClearDataBase();
        }

        private void ClearDataBase()
        {
            //string query = @"DELETE FROM [dbo].[ProjectInv];";

            //using (SqlConnection cnn=new SqlConnection(ConnectionString))
            //{
            //    SqlCommand cmd = new SqlCommand(query, cnn)
            //    {
            //        CommandType = CommandType.Text
            //    };

            //    cnn.Open();
            //    cmd.ExecuteNonQuery();
            //}
        }
    }
}
