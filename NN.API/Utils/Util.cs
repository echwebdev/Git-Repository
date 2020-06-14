using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Util
    {
        /// <summary>
        /// Transform a datatable to a list of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {

                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ToObject<T>(DataRow row) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            var item = CreateItemFromRow<T>((DataRow)row, properties);
            return item;
        }

        /// <summary>
        /// Create the object from a DataRow 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            try
            {
                foreach (var property in properties)
                {
                    try
                    {
                        if (property.PropertyType == typeof(System.DayOfWeek))
                        {
                            DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                            property.SetValue(item, day, null);
                        }
                        else
                        {
                            if (row.Table.Columns.Contains(property.Name))
                            {
                                if (row[property.Name].GetType() == typeof(System.DBNull))
                                {
                                    if (property.PropertyType == typeof(string))
                                        property.SetValue(item, string.Empty, null);
                                }
                                else
                                {
                                    property.SetValue(item, row[property.Name], null);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return item;
        }

        public static T CloneObject<T>(this object source)
        {
            T result = Activator.CreateInstance<T>();
            try
            {
                Type firstType = source.GetType();

                foreach (PropertyInfo propertyInfo in firstType.GetProperties())
                {
                    if (propertyInfo.CanRead)
                    {
                        object firstValue = propertyInfo.GetValue(source);
                        object secondValue = propertyInfo.GetValue(result);

                        propertyInfo.SetValue(result, secondValue);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
