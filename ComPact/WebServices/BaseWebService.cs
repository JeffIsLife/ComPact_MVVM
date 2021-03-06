using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComPact.WebServices;
using ModernHttpClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using Refit;
using ComPact.Exceptions;
using System.Net;
using ComPact.Models;

namespace ComPact
{
	public class BaseWebService<T>: IBaseWebService<T>
	{
		static HttpClient _instance;
		static string _baseUri = "http://192.168.56.1:8080/"; //"http://10.99.9.93:8080/"; 

		/**
		 * 
		 */
		void CreateHttpClient()
		{
			_instance = new HttpClient(new NativeMessageHandler());
			_instance.BaseAddress = new Uri(_baseUri);
			_instance.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		}
		/**
		 * 
		 */
		protected HttpClient GetHttpClient()
		{
			if (_instance == null)
				CreateHttpClient();
			return _instance;
		}

		/**
		 * Reference for HttpClient.PostAsJsonAsync 'System.Net.Http.Formatting.dll'
		 */
		public virtual async Task<T> Create(string urlExtend, T obj)
		{
			var client = GetHttpClient();

			var data = JsonConvert.SerializeObject(obj);
			var postContent = new StringContent(data, Encoding.UTF8, "application/json");
			try
			{
				HttpResponseMessage response = await client.PostAsync(urlExtend, postContent);
				if (response.StatusCode == HttpStatusCode.Conflict)
				{
					throw new ArgumentException();
				}
				response.EnsureSuccessStatusCode();
				var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
				return result;
			}
			catch (ArgumentException)
			{
				throw new ArgumentException();
			}
			catch (Exception ex)
			{
				throw new Exceptions.WebException("Create failed", ex);
			}
		}
		public async Task<T> Create(string urlExtend, IEnumerable<T> obj)
		{
			var client = GetHttpClient();

			var data = JsonConvert.SerializeObject(obj);
			new MultipartFormDataContent();
			var postContent = new StringContent(data, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(urlExtend, postContent);
			if (response.StatusCode == HttpStatusCode.Conflict)
			{
				throw new ArgumentException();
			}
			response.EnsureSuccessStatusCode();
			var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

			return result;
		}

		public virtual async Task<T> Read(string urlExtend)
		{
			HttpClient client = GetHttpClient();
			var response = await client.GetAsync(urlExtend);
			if (response.IsSuccessStatusCode)
			{
				var getResponseString = await response.Content.ReadAsStringAsync();
				var result = await Task.Run(() => JsonConvert.DeserializeObject<T>(getResponseString));
				return result;
			}
			return default(T);
		}
		public virtual async Task<IEnumerable<T>> ReadAll(string urlExtend)
		{
			HttpClient client = GetHttpClient();
			var response = await client.GetAsync(urlExtend);
			if (response.IsSuccessStatusCode)
			{
				var getResponseString = await response.Content.ReadAsStringAsync();
				var result = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(getResponseString));
				return result;
			}
			return default(IEnumerable<T>);
		}
		public virtual async Task<T> Update(string urlExtend, T obj)
		{
			HttpClient client = GetHttpClient();
			var data = JsonConvert.SerializeObject(obj);
			var putContent = new StringContent(data, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync(urlExtend, putContent);
			response.EnsureSuccessStatusCode();
			var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

			return result;
		}
		public virtual async Task<CallResult> Delete(string url)
		{
			HttpClient client = GetHttpClient();
			HttpResponseMessage response = await client.DeleteAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var getResponseString = await response.Content.ReadAsStringAsync();
				var result = await Task.Run(() => JsonConvert.DeserializeObject<CallResult>(getResponseString));
				return result;
			}
			return default(CallResult);
		}

	}
}