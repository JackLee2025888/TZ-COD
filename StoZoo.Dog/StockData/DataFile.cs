using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class DataFile
    {
        public DataFile()
        {
        }

        FileStream sm = null;
        public bool Open(string path, int offset = 0, bool append = false)
        {
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                sm = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                if (append)
                {
                    if (sm.Length > 0)
                        sm.Seek(0, SeekOrigin.End);
                }
                else if (offset < 0)
                    sm.Seek(offset, SeekOrigin.End);
                else
                    sm.Seek(offset, SeekOrigin.Begin);
                return true;
            }
            catch(Exception e)
            {
                try
                {
                    sm.Close();
                    sm.Dispose();
                }
                catch { }
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                sm.Close();
                sm.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] Read(int len)
        {
            try
            {
                byte[] data = new byte[len];
                int count = sm.Read(data, 0, len);
                if (count == len)
                    return data;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public bool Write(byte[] data)
        {
            try
            {
                sm.Write(data, 0, data.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Cut()
        {
            try
            {
                sm.SetLength(sm.Position);
                return Close();

            }
            catch
            {
                return false;
            }
        }
    }
}
