using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Common
{
    public class WebUtil
    {
        public string JavaScriptStringEncode(object str)
        {
            try
            {
                return HttpUtility.JavaScriptStringEncode(str.ToString());
            }
            catch
            {
                return string.Empty;
            }

        }

        public string GetURLSafeGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public string JSONSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public T JSONDeserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }
        public string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public string ObjectToString(object obj)
        {
            return Base64Encode(CompressString(JSONSerialize(obj)));
        }
        public T StringToObject<T>(string json)
        {
            return JSONDeserialize<T>(DecompressString(Base64Decode(json)));
        }

        public string GetClientIP()
        {
            try
            {
                var szXforwarded = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                return szXforwarded;

            }
            catch
            {
                return string.Empty;
            }
        }

        public bool ValideEmailAddress(string email)
        {

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public string RemoveWhiteSpace(object obj)
        {
            try
            {
                string strTemp = obj.ToString();
                Regex whitespaceRegex = new Regex("\\s");
                return whitespaceRegex.Replace(strTemp, "");
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool IsEmptyHTMLContent(string source)
        {
            try
            {
                if (source.IndexOf("<img") >= 0)
                    return false;
                var t = Regex.Replace(source, "<.*?>", string.Empty);
                if (string.IsNullOrEmpty(t))
                    return true;
                else
                    return false;
            }
            catch
            {
            }
            return false;
        }

    }
}
