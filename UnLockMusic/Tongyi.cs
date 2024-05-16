using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnLockMusic
{       //用于实现api的调用
    class Tongyi
    {
        private readonly  string _requestUri = "https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation";
        private readonly  string _apiKey = "sk-2d5dd0f8dccf49a9a5484316f57f43e2"; // 阿里云密钥信息
        private readonly  string _model = "qwen-max";

        public async Task<string> CallQWen(string question)
        {
            using (var client = new HttpClient())
            {
                // 创建模型类
                var requestObj = new QianWenRequest
                {
                    Model = _model,
                    Input = new Input
                    {
                        Prompt = question
                    }
                };

                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                };

                // 将对象序列化为JSON字符串
                string requestJson = JsonConvert.SerializeObject(requestObj, settings);
                //Console.WriteLine(requestJson);

                var request = new HttpRequestMessage(HttpMethod.Post, _requestUri);
                //定义Body
                var content = new StringContent(requestJson.ToLower(), Encoding.UTF8, "application/json");
                request.Content = content;

                //定义header
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // 正确做法
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{_apiKey}");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine("通义千问的回答：");
                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.WriteLine(responseBody);
                    return responseBody;
                }
                else
                {
                    //Console.ForegroundColor = ConsoleColor.Red;
                    string error = $"请求失败，状态码：{response.StatusCode}";
                    //Console.WriteLine($"请求失败，状态码：{response.StatusCode}");
                    //Console.ForegroundColor = ConsoleColor.White;
                    return error;
                }
            }
        }

       
    }
    
    public class QianWenRequest
    {
        public string Model { get; set; }
        public Input Input { get; set; }
    }

    public class Input
    {
        public string Prompt { get; set; }

    }
}
