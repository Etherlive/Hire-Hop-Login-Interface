﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HireHop_Login_Interface.Services
{
    public static class RequestInterface
    {
        #region Methods

        public static async Task<ClientConnection> SendRequest(ClientConnection client, string urlPath, string method = "POST", List<string> contentList = null)
        {
            var httpClient = client.httpClient;

            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var request = new HttpRequestMessage(new HttpMethod(method), $"{ClientConnection.url}{urlPath}"))
            {
                if (contentList != null)
                {
                    request.Content = new StringContent(string.Join("&", contentList));
                }
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                try
                {
                    var response = await httpClient.SendAsync(request);

                    client.__lastResponse = response;
                }
                catch (Exception e)
                {
                    e = e;
                }

                return client;
            }
        }

        #endregion Methods
    }

    public class ClientConnection
    {
        #region Fields

        public static readonly string url = "https://myhirehop.com/";

        public string __id;
        public HttpResponseMessage __lastResponse;
        private HttpClient __httpClient;
        private HttpClientHandler __httpClientHandler;

        #endregion Fields

        #region Properties

        public CookieCollection cookies
        {
            get
            {
                return __httpClientHandler.CookieContainer.GetCookies(new Uri(url));
            }
        }

        public HttpClient httpClient
        {
            get
            {
                __httpClient = __httpClient == null ? new HttpClient(httpClientHandler) : __httpClient;
                return __httpClient;
            }
        }

        public HttpClientHandler httpClientHandler
        {
            get
            {
                __httpClientHandler = __httpClientHandler == null ? constructHttpClient() : __httpClientHandler;
                return __httpClientHandler;
            }
        }

        #endregion Properties

        #region Methods

        private HttpClientHandler constructHttpClient()
        {
            HttpClientHandler http = new HttpClientHandler();
            http.UseCookies = true;
            return http;
        }

        #endregion Methods
    }
}