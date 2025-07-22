using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TZ.OperationLibrary
{
    public class WebApiConnection
    {
        WebClient client = null;

        class CookieWebClient : WebClient
        {
            private CookieContainer jar = new CookieContainer();
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                HttpWebRequest webRequest = request as HttpWebRequest;
                if (webRequest != null)
                    webRequest.CookieContainer = jar;
                return request;
            }
        }

        public WebApiConnection()
        {
            client = new CookieWebClient()
            {
                Credentials = CredentialCache.DefaultCredentials,
                UseDefaultCredentials = true
            };

            client.DownloadDataCompleted += Client_DownloadDataCompleted;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.UploadDataCompleted += Client_UploadDataCompleted;
            client.UploadFileCompleted += Client_UploadFileCompleted;

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.UploadProgressChanged += Client_UploadProgressChanged;
        }

        #region event
        private void Client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void Client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            Client_Completed(e.UserState as object[], e.Error == null ? e.Result : null, e.Error);
        }

        private void Client_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            Client_Completed(e.UserState as object[], e.Error == null ? e.Result : null, e.Error);
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Client_Completed(e.UserState as object[], null, e.Error);
        }

        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Client_Completed(e.UserState as object[], e.Error == null ? e.Result : null, e.Error);
        }

        private void Client_Completed(object[] tokens, byte[] bytes, Exception e)
        {
            Action<object, string> callback = tokens[1] as Action<object, string>;
            Type type = tokens[0] as Type;

            if (e != null)
                callback(null, e.Message);
            else
            {
                if (type != null)
                {
                    MethodInfo mi = typeof(WebApiConnection).GetMethod("getResult");
                    if (mi != null)
                    {
                        mi = mi.MakeGenericMethod(type);
                        Tuple<object, string> result = mi.Invoke(null, new object[] { bytes }) as Tuple<object, string>;

                        if (result == null)
                            callback(null, "未知错误");
                        else
                            callback(result.Item1, result.Item2);
                    }
                }
            }
        }
        #endregion

        #region Action

        public void getUrlAsync<T>(string url, Action<object, string> callBack)
        {
            if (client.IsBusy)
            {
                callBack(null, "网络繁忙");
                return;
            }
            client.DownloadDataAsync(new Uri(url), new object[] { typeof(T), callBack });
        }

        public T getUrl<T>(string url)
        {
            if (client.IsBusy)
                return default(T);
            try
            {
                byte[] bytes = client.DownloadData(new Uri(url));
                if (typeof(T) == typeof(string))
                    return (T)Convert.ChangeType(Encoding.UTF8.GetString(bytes), typeof(T));
                T result = getResult<T>(bytes);
                return (T)result;
            }
            catch
            {
                return default(T);
            }
        }


        public void postUrlAsync<T>(string url, object obj, Action<object, string> callBack)
        {
            if (client.IsBusy)
            {
                callBack(null, "网络繁忙");
                return;
            }
            client.Headers.Add("Content-Type", "application/json");
            byte[] data = StoZoo.Dog.Global.SerializeToByte(obj);
            client.UploadDataAsync(new Uri(url), "POST", data, new object[] { typeof(T), callBack });
        }

        public T postUrl<T>(string url, object obj)
        {
            if (client.IsBusy)
                return default(T);

            try
            {
                client.Headers.Add("Content-Type", "application/json");
                byte[] data = StoZoo.Dog.Global.SerializeToByte(obj);
                byte[] bytes = client.UploadData(new Uri(url), "POST", data);
                if (typeof(T) == typeof(string))
                    return (T)Convert.ChangeType(Encoding.UTF8.GetString(bytes), typeof(T));
                T result = getResult<T>(bytes);
                return (T)result;
            }
            catch
            {
                return default(T);
            }
        }


        public void PostFileAsync<T>(string url, string path, Action<object, string> callBack)
        {
            if (client.IsBusy)
            {
                callBack(null, "网络繁忙");
                return;
            }
            client.UploadFileAsync(new Uri(url), "POST", path, new object[] { typeof(T), callBack });
        }

        public T PostFile<T>(string url, string path)
        {
            if (client.IsBusy)
                return default(T);
            byte[] bytes = client.UploadFile(new Uri(url), "POST", path);
            T result = getResult<T>(bytes);
            return (T)result;
        }

        public void GetFileAsync(string url, string path, Action<object, string> callBack)
        {
            if (client.IsBusy)
            {
                callBack(null, "网络繁忙");
                return;
            }
            client.DownloadFileAsync(new Uri(url), path, new object[] { null, callBack });
        }

        public void GetFile(string url, string path)
        {
            if (client.IsBusy) return;
            client.DownloadFile(new Uri(url), path);
        }

        #endregion 

        public static T getResult<T>(byte[] data)
        {
            try
            {
                T returnValue = StoZoo.Dog.Global.Deserialize<T>(data);
                return returnValue;
            }
            catch
            { return default(T); }
        }


    }

}
