using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace AddressBook_RestSharpAPI
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void setUp()
        {
            client = new RestClient(" http://localhost:3000");
        }
        private IRestResponse getAddressBookList()
        {
            //arrange
            RestRequest request = new RestRequest("/addressBook", Method.GET);
            //act
            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void whileCallingGETAPi_shouldReturnAddressBookList()
        {
            IRestResponse response = getAddressBookList();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<AddressBook> dataResponse = JsonConvert.DeserializeObject<List<AddressBook>>(response.Content);
            Assert.AreEqual(4, dataResponse.Count);
        }
    }
}
