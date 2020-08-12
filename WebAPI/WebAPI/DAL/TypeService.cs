using System.Linq;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class TypeService
    {
        internal static object SelTypeListAll()
        {
            using (YoungMusicEntities db = new YoungMusicEntities())
            {
                return db.Type.Select(x => new { Tid = x.T_Id, Tname = x.T_Name }).ToList();
            }
        }
    }
}