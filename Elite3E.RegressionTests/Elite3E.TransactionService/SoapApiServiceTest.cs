
using Elite3E.SoapServices.Services.Requests.Interface;
using NUnit.Framework;
using System.Reflection;
using System.Xml;
using Elite3E.SoapServices.Services.Requests;
using FluentAssertions;

namespace Elite3E.SoapServices
{
    [TestFixture]
    public class SoapApiServiceTest
    {
        private readonly IEntityPersonRequest _entityPersonRequest = new EntityPersonRequest();
        private readonly IVendorRequest _vendorRequest = new VendorRequest();
        private readonly IMatterRequest _matterRequest = new MatterRequest();
        private readonly IPayorRequest _payorRequest = new PayorRequest();
        private readonly IClientRequest _clientRequest = new ClientRequest();
        private readonly INxBizTalkEntityRequest _nxBizTalkEntityRequest = new NxBizTalkEntityRequest();
        private TransactionServiceFT.ExecuteProcessResponse? _response;
      
        
        [Test]
        public async Task CreatePayorAsync()
        {
            _response = await _payorRequest.CreatePayorAsync(GetXmlString("Payor.xml"));

            Console.WriteLine("Response : " + _response.Body.ExecuteProcessResult);

            _response.Body.ExecuteProcessResult.Should().NotContain("Failure");
        }
        [Test]
        public async Task CreateClientAsync()
        {
            _response = await _clientRequest.CreateClientAsync(GetXmlString("Client_Srv.xml"));

            Console.WriteLine("Response : " + _response.Body.ExecuteProcessResult);

            _response.Body.ExecuteProcessResult.Should().Contain("Success");
        }
        [Test]
        public async Task CreateNxBizTalkEntityAsync()
        {
            _response = await _nxBizTalkEntityRequest.CreateNxBizTalkEntityAsync(GetXmlString("NxBizTalkEntity.xml"));

            Console.WriteLine("Response : " + _response.Body.ExecuteProcessResult);
            _response.Body.ExecuteProcessResult.Should().NotContain("Failure");
            _response.Body.ExecuteProcessResult.Should().Contain("Success");
        }

        [Test]
        public async Task CreateMatterAsync()
        {

            var xmlString = GetXmlString("Matter_Srv.xml");
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            xmlString = xmlString.Replace("auto", Guid.NewGuid().ToString()).Replace("today", date);

            _response = await _matterRequest.CreateMatterAsync(xmlString);

            Console.WriteLine("Response : " + _response.Body.ExecuteProcessResult);
            _response.Body.ExecuteProcessResult.Should().NotContain("Error");
            _response.Body.ExecuteProcessResult.Should().Contain("Success");
        }

        [Test]
        public async Task CreateEntityPerson()
        {
            _response = await _entityPersonRequest.CreateEntityPersonAsync();

            Console.WriteLine("Response : " + _response.Body.ExecuteProcessResult);
        }

        [Test]
        public async Task CreateVendor()
        {
            _response = await _vendorRequest.CreateVendorAsync();

            Console.WriteLine("Response : " + _response.Body.ExecuteProcessResult);

            if (_response.Body.ExecuteProcessResult.Contains("Success"))
            {
                var results = _vendorRequest.GetProcessExecutionResults(_response.Body.ExecuteProcessResult);
                var message = results.MESSAGE;
                Console.WriteLine("Message : " + message);
            }
        }
        
        private static string GetResourceFilePath(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\XmlFiles\\", fileName);
        }

        private static string GetXmlString(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(GetResourceFilePath(fileName));
            return xmlDoc.OuterXml;
        }

    }
    
}
