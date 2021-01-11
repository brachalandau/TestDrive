using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class FactoryDAL
    {
        static Idal dal = null;
        public static Idal GetDal()
        {
            if (dal == null)
                dal = new Dal_Imp_Xml();
            return dal;
        }
    }
}
