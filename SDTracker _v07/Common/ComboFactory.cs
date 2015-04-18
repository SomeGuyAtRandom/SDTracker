using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDTracker.Common
{
    public class ComboFactory<T>
    {
        private class ItemForCombo
        {
            public ItemForCombo(T obj)
            {
 
            }

        }

        public List<T> GetList(T objIn)
        {
            List<T> items = new List<T>();

            return items;
        }

    }
}