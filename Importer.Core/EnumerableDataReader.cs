using System;
using System.Collections.Generic;
using System.Data;

namespace Importer.Core
{
    public class EnumerableDataReader : IDataReader
    {
        private readonly List<Import> _imports;
        private int _currentIndex = -1;

        public EnumerableDataReader(List<Import> import)
        {
            _imports = import;
        }
        /// <summary>
        /// The FieldCount property is the number of data fields in each record.In this case 3
        /// </summary>
        public int FieldCount
        {
            get { return 3; }
        }


        /// <summary>
        /// The Read method returns a boolean value which signifies whether there is more data available for the IDataReader to read
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            if ((_currentIndex + 1) < _imports.Count)
            {
                _currentIndex++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method returns a value of the field for the supplied ordinal. For this method to work properly,
        ///  you must maintain an index to the position in the data you are reading.
        ///  I maintain this index in the _currentIndex field. 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public object GetValue(int i)
        {
            switch (i)
            {
                case 0:
                  return  _imports[_currentIndex].OrderID;
                case 1:
                    return _imports[_currentIndex].CustomerID;
                case 2:
                    return _imports[_currentIndex].ShipAddress;
                default:
                    return null;
            }
        }

        
        /// <summary>
        /// This method is the opposite of GetName. This method returns the ordinal for the specified column name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetOrdinal(string name)
        {
            switch (name)
            {
                case "OrderID":
                    return 0;
                case "CustomerID":
                    return 1;

                case "ShipAddress":
                    return 2;
                default:
                    return -1;

            }

        }

        /// <summary>
        /// This method is provided an ordinal which represents the column number in the database table we will be copying data to.  
        /// The method returns the name of the column for the specified ordinal.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetName(int i)
        {
            switch (i)
            {
                case 0:
                    return "OrderID";
                case 1:
                    return "CustomerID";
                case 2:
                    return "ShipAddress";
                default:
                    return string.Empty;
            }

        }

        

        #region NotUsed
        public void Close()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }



        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }



        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        




        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

    }
}