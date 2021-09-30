using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIHuisdierMonitor.DAL
{
    public static class FoodBowlDAL
    {
        private readonly static string ConnString = "";
        private readonly static SqlConnection conn = new SqlConnection(ConnString);

    }
}
